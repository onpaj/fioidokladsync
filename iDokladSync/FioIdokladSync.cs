using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using FioSdkCsharp;
using FioSdkCsharp.Models;
using IdokladSdk;
using IdokladSdk.ApiModels.BankStatements;
using IdokladSdk.Clients.Auth;
using IdokladSdk.Enums;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using ApiExplorer = FioSdkCsharp.ApiExplorer;

namespace iDokladSync
{
    public class FioIdokladSync
    {
        private readonly ILogger _logger;
        private ApiExplorer _fioClient;
        private IdokladSdk.ApiExplorer _iDokladClient;
        private RetryPolicy _retryPolicy;

        public FioIdokladSync(ILogger logger, string fioApiKey, string idokladClientId, string iDokladClientSecret)
        {
            _logger = logger;

            _fioClient = GetFioApi(fioApiKey);
            _iDokladClient = GetIdokladApi(idokladClientId, iDokladClientSecret);

            _retryPolicy = Policy
                .Handle<HttpRequestException>()
                .WaitAndRetry(10, i => TimeSpan.FromSeconds(i * i * i));
        }

        public int Sync(TransactionFilter transactionFilter = null)
        {
            AccountStatement statements;
            if (transactionFilter == null)
                statements = _fioClient.Last();
            else
                statements = _fioClient.Periods(transactionFilter);

            _logger.LogInformation("Loaded {StatementsCount} new statements",
                statements.TransactionList.Transactions.Count);

            var newStatements = new List<BankPairStatement>();

            foreach (var t in statements.TransactionList.Transactions)
            {
                var st = new BankPairStatement()
                {
                    PartnerAccountNumber = t.CounterpartAccount?.Value,
                    PartnerBankCode = t.CounterpartBankCode?.Value,
                    CurrencyCode = t.Currency.Value,
                    Amount = Math.Abs(t.Amount.Value),
                    VariableSymbol = t.VariableSymbol?.Value,
                    DateOfTransaction = t.Date.Value.Date.AddHours(2), // UTC error in iDoklad
                    MessageForPartner = t.Comment?.Value,
                    MovementType = t.Amount.Value > 0 ? MovementTypeEnum.Entry : MovementTypeEnum.Issue,
                    AccountNumber = statements.Info.AccountId,
                    BankCode = statements.Info.BankId,
                    
                };

                newStatements.Add(st);
            }


            foreach (var stm in newStatements)
            {
                _retryPolicy.Execute(() =>
                {
                    var result = _iDokladClient.BankStatements.Pair(stm);
                    _logger.LogInformation(
                        "Submitted new statement with result: {Result}. no. {Id} ({VS} | {Amount}). Reason: {Message}",
                        result.WasPaired, stm.PartnerAccountNumber, stm.VariableSymbol, stm.Amount, result.Message);
                });
            }


            return newStatements.Count();
        }


        public FioSdkCsharp.ApiExplorer GetFioApi(string apiKey)
        {
            var explorer = new FioSdkCsharp.ApiExplorer(apiKey);

            return explorer;
        }


        public IdokladSdk.ApiExplorer GetIdokladApi(string clientId, string clientSecret)
        {
            var credentials = new ClientCredentialAuth(clientId, clientSecret);

            var apiContext = new ApiContext(credentials)
            {
                AppName = "iDoklad",
            };

            var explorer = new IdokladSdk.ApiExplorer(apiContext);

            return explorer;
        }

    }
}
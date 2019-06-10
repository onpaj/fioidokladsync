using System;
using System.Collections.Generic;
using System.Net.Http;
using FioSdkCsharp;
using FioSdkCsharp.Models;
using IdokladSdk.ApiModels.BankStatements;
using IdokladSdk.Enums;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using ApiExplorer = FioSdkCsharp.ApiExplorer;

namespace iDokladSync.Bank
{
    public class FioBankImporter : IBankImporter
    {
        private readonly IdokladSdk.ApiExplorer _idokladClient;
        private readonly ApiExplorer _fioClient;
        private readonly ILogger<FioBankImporter> _logger;
        private RetryPolicy _retryPolicy;

        public FioBankImporter(IdokladSdk.ApiExplorer idokladClient, FioSdkCsharp.ApiExplorer fioClient, ILogger<FioBankImporter> logger)
        {
            _idokladClient = idokladClient;
            _fioClient = fioClient;
            _logger = logger;

            _retryPolicy = Policy
                .Handle<HttpRequestException>()
                .WaitAndRetry(10, i => TimeSpan.FromSeconds(i * i * i));
        }

        public List<BankPairStatement> Statements { get; private set; }


        public List<BankPairStatement> Load(TransactionFilter transactionFilter = null)
        {
            AccountStatement statements;
            if (transactionFilter == null)
                statements = _fioClient.Last();
            else
                statements = _fioClient.Periods(transactionFilter);

            _logger.LogInformation("Loaded {StatementsCount} new statements",
                statements.TransactionList.Transactions.Count);

            Statements = new List<BankPairStatement>();

            foreach (var t in statements.TransactionList.Transactions)
            {
                var st = new BankPairStatement()
                {
                    PartnerAccountNumber = t.CounterpartAccount?.Value,
                    PartnerBankCode = t.CounterpartBankCode?.Value,
                    CurrencyCode = t.Currency.Value,
                    Amount = Math.Abs(t.Amount.Value),
                    VariableSymbol = t.VariableSymbol?.Value ?? "0", // Variable is required
                    DateOfTransaction = t.Date.Value.Date.AddHours(2), // UTC error in iDoklad
                    MessageForPartner = t.Comment?.Value,
                    MovementType = t.Amount.Value > 0 ? MovementTypeEnum.Entry : MovementTypeEnum.Issue,
                    AccountNumber = statements.Info.AccountId,
                    BankCode = statements.Info.BankId,

                };

                Statements.Add(st);
            }

            return Statements;
        }


        public List<BankPairStatement> Submit()
        {
            foreach (var stm in Statements)
            {
                _retryPolicy.Execute(() =>
                {
                    var result = _idokladClient.BankStatements.Pair(stm);
                    _logger.LogInformation(
                        "Submitted new statement with result: {Result}. no. {Id} ({VS} | {Amount}). Reason: {Message}",
                        result.WasPaired, stm.PartnerAccountNumber, stm.VariableSymbol, stm.Amount, result.Message);
                });
            }

            return Statements;
        }
    }
}
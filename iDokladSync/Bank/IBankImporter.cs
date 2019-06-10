using System.Collections.Generic;
using FioSdkCsharp;
using IdokladSdk.ApiModels.BankStatements;

namespace iDokladSync.Bank
{
    public interface IBankImporter
    {
        List<BankPairStatement> Submit();
        List<BankPairStatement> Load(TransactionFilter transactionFilter = null);
        List<BankPairStatement> Statements { get; }
    }
}
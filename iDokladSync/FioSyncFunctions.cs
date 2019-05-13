using System;
using System.Dynamic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using FioSdkCsharp;
using IdokladSdk.ApiModels.ReadOnlyEntites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace iDokladSync
{
    public static class FioSyncFunctions
    {
        [FunctionName("SyncLast")]
        public static void SyncLast([TimerTrigger("0 */10 * * * *")]TimerInfo myTimer, ILogger log)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("cs-CZ");

            var syncService = new FioIdokladSync(
                    log, 
                    Environment.GetEnvironmentVariable("FIO_API_KEY"),
                Environment.GetEnvironmentVariable("IDOKLAD_CLIENTID"),
                Environment.GetEnvironmentVariable("IDOKLAD_CLIENT_SECRET")
                    );
            
            var imported = syncService.Sync();
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}, importing {imported} items");
        }

        [FunctionName("SyncCustom")]
        public static async Task SyncCustom([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("cs-CZ");

            var syncService = new FioIdokladSync(
                log,
                Environment.GetEnvironmentVariable("FIO_API_KEY"),
                Environment.GetEnvironmentVariable("IDOKLAD_CLIENTID"),
                Environment.GetEnvironmentVariable("IDOKLAD_CLIENT_SECRET")
            );

            var body = await req.ReadAsStringAsync();
            var filter = JsonConvert.DeserializeObject<TransactionFilter>(body);

            var imported = syncService.Sync(filter);
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}, importing {imported} items");
        }
    }
}

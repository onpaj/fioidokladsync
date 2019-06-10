using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FioSdkCsharp;
using iDokladSync;
using iDokladSync.Bank;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ImportHebloMVC.Controllers
{
    public class BankController : Controller
    {
        private readonly IBankImporter _bankClient;
        private readonly ILogger<BankController> _logger;

        public BankController(IBankImporter bankClient, ILogger<BankController> logger)
        {
            _bankClient = bankClient;
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult History()
        {
            return View("Index");
        }

        public IActionResult Load(DateTime dateFrom, DateTime dateTo)
        {
            if(dateTo == DateTime.MinValue)
                dateTo = DateTime.MaxValue;

            _bankClient.Load(new TransactionFilter(dateFrom, dateTo));

            return View("List", _bankClient.Statements);
        }

        public IActionResult Import()
        {
            _bankClient.Submit();

            return View("Index");
        }

        public IActionResult LoadRecent()
        {
            _bankClient.Load();

            return View("List", _bankClient.Statements);
        }
    }
}
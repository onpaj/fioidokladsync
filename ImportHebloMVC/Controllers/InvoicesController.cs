using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iDokladSync;
using iDokladSync.Invoices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;

namespace ImportHebloMVC.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IInvoiceImporter _invoiceImporter;
        private readonly IInvoiceSourceFactory _invoiceSourceFactory;
        private readonly ILogger<InvoicesController> _logger;

        public InvoicesController(
            IInvoiceImporter invoiceImporter, 
            IInvoiceSourceFactory invoiceSourceFactory,
            ILogger<InvoicesController> logger)
        {
            _invoiceImporter = invoiceImporter;
            _invoiceSourceFactory = invoiceSourceFactory;
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

        public IActionResult Upload(IFormFile file)
        {
            var sb = new StringBuilder();

            if (file == null)
                return View("Index");

            string contentXml = null;
            using (TextReader tr = new StreamReader(file.OpenReadStream()))
            {
                contentXml = tr.ReadToEnd();
            }

            var content = _invoiceSourceFactory.Create(contentXml);
            _invoiceImporter.Load(content);

            return View("List", _invoiceImporter.InvoiceBatch.Invoices.Select(s => s.Invoice));
        }

        public IActionResult List()
        {
            return View(_invoiceImporter.InvoiceBatch.Invoices.Select(s => s.Invoice));
        }

        public IActionResult Import()
        {
            _invoiceImporter.Submit();

            return View("Index");
        }
    }
}
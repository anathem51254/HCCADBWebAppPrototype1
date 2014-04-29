using DocumentFormat.OpenXml.Packaging;
using HCCADBWebAppPrototype1.DAL;
using HCCADBWebAppPrototype1.Models;
using HCCADBWebAppPrototype1.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HCCADBWebAppPrototype1.Controllers
{
    public class ReportGenerationController : Controller
    {
        //
        // GET: /ReportGeneration/
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GenerateReport()
        {
            MemoryStream mem = new MemoryStream(System.IO.File.ReadAllBytes(@"D:\GitHub\HCCADBWebAppPrototype1\HCCADBWebAppPrototype1\download.docx"));

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(mem, true))
            {

                string docText = null;

                using (StreamReader src = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    docText = src.ReadToEnd();
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    using (StreamWriter sw = new StreamWriter(ms))
                    {
                        sw.Write(docText);
                    }

                    MemoryStream _ms = new MemoryStream(ms.ToArray());
                    _ms.Seek(0, SeekOrigin.Begin);
                    wordDoc.MainDocumentPart.FeedData(_ms);
                }
            }
            mem.Seek(0, SeekOrigin.Begin);
            
            //return View();
            return File(mem, "application/octet-stream", "download.docx");
        }
	}
}
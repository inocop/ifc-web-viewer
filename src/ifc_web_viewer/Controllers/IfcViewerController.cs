using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ifc_web_viewer.Models;
using System.IO;

namespace ifc_web_viewer.Controllers
{
    public class IfcViewerController : Controller
    {
        public IActionResult Index()
        {
            // get data directory files
            string[] files = Directory.GetFiles( @"/opt/dotnet/src/data/", "*", SearchOption.AllDirectories);
            ViewData["IfcFiles"] = files;
         
            return View();
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

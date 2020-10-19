using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcFiles.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace MvcStar.Controllers
{
    public class FilesController : Controller
    {
        private readonly IWebHostEnvironment _env;
        public FilesController(IWebHostEnvironment env)
        {
            _env = env;
        }
        public IActionResult Index()
        {
            string webRootPath = _env.WebRootPath;
            string[] files = Directory.GetFiles(webRootPath + "\\TextFiles");
            string[] filenames = new string[files.Length];
            int i = 0;
            foreach(string file in files)
            {
                 filenames[i++] = (Path.GetFileNameWithoutExtension(file));
            }
            return View(filenames);
        }
       
        public IActionResult Contents(string id)
        {
            string webRootPath = _env.WebRootPath + "\\TextFiles\\" + id + ".txt";
            try 
            {
                    using (StreamReader sr = new StreamReader(webRootPath))
                    {
                        ViewBag.MyString = sr.ReadToEnd();                 
                    }
            }
            catch(IOException e)
            {
                ViewBag.MyString = "Please choose File Name from the List  " + e.Message.ToString();
            }                   
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}
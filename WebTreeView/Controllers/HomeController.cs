using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using WebTreeView.Models;

namespace WebTreeView.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string path = HostingEnvironment.MapPath("~/App_Data/items.json");
            string json = System.IO.File.ReadAllText(path);
            List<ItemModel> items = JsonConvert.DeserializeObject<List<ItemModel>>(json);

            return View(items);
        }       
    }
}
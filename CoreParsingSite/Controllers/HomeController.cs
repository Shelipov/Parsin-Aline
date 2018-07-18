using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreParsingSite.Models;


namespace CoreParsingSite.Controllers
{
    public class HomeController : Controller
    {
        IGoodsRepository repo;
        public HomeController(IGoodsRepository r)
        {
            repo = r;
        }

        public IActionResult Index(int page = 1)
        {
            
            int pageSize = 1;   // количество элементов на странице            
            var source = repo.GetGoods();
            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,_Goods = items
            };
            return View(viewModel);
        }

        public IActionResult Parsing_Page(Parse parse)
        {          
            parse.Parsing();
            return View();

        }
        public IActionResult Price_Check(Price price)
        {
            ViewBag.Names = price.Changes(repo.GetGoods()).Keys;
            return View();
        }
            public IActionResult About()
        {          
            return View(repo.GetGoods().ToList());
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmbeddedStockByPros.Models;
using Microsoft.EntityFrameworkCore;

namespace EmbeddedStockByPros.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;


        public HomeController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var viewlist = new List<ComponentType>();

            var hewoui = _context.CategoryComponenttypebindings.ToList();

            var tmp = _context.ComponentTypes
                .Include(data => data.CategoryComponenttypebindings)
                .ThenInclude(data => data.Category)
                .ToList();

            foreach (var item in tmp)
            {
                foreach (var item2 in item.CategoryComponenttypebindings)
                {
                    if (item2.Category.Name == "Arduino")
                    {
                        viewlist.Add(item);
                    }
                }
            }

            return View(viewlist);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
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

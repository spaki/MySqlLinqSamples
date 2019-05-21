using LinqSamples.Db;
using LinqSamples.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LinqSamples.Controllers
{
    public class HomeController : Controller
    {
        private readonly AvengersDb db;

        public HomeController(AvengersDb db) => this.db = db;

        public IActionResult Index()
        {
            #region Memory
            var avengers = new List<Avenger> {
                new Avenger { Codename = "Iron Man", MainSkill = "Armor", Age = 45 },
                new Avenger { Codename = "Captain America", MainSkill = "Strategy", Age = 90 },
                new Avenger { Codename = "Hulk", MainSkill = "Strength", Age = 40 },
                ////new Avenger { Codename = "Captain Marvel", MainSkill = "Overpower", Age = 36 },
            };

            ViewBag.avengersOrderedByName = avengers.OrderBy(e => e.Codename).ToList();
            ViewBag.avengersOrderedByAge = avengers.OrderByDescending(e => e.Age).ToList();
            ViewBag.avengersWithLetterA = avengers.Where(e => e.Codename.Contains("a")).OrderBy(e => e.Codename).ToList();
            ViewBag.avengersOlders = avengers.Where(e => e.Age > 42).OrderBy(e => e.Age).ToList();
            #endregion

            #region Database

            ////avengers.ForEach(e => db.Avengers.Add(e));
            ////db.SaveChanges();

            //var avengersOrderedByNameQuery = db.Avengers.OrderBy(e => e.Codename);
            //var avengersOrderedByName = avengersOrderedByNameQuery.ToList();
            //ViewBag.avengersOrderedByName = avengersOrderedByName;

            //ViewBag.avengersOrderedByAge = db.Avengers.OrderByDescending(e => e.Age).ToList();
            //ViewBag.avengersWithLetterA = db.Avengers.Where(e => e.Codename.Contains("a")).OrderBy(e => e.Codename).ToList();
            //ViewBag.avengersOlders = db.Avengers.Where(e => e.Age > 42).OrderBy(e => e.Age).ToList();

            #endregion

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

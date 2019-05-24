using LinqSamples.Db;
using LinqSamples.Models;
using Microsoft.AspNetCore.Mvc;
using System;
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
            var avengers = new List<Avenger> {
                new Avenger { Codename = "Iron Man", MainSkill = "Armor", Age = 45 },
                new Avenger { Codename = "Captain America", MainSkill = "Strategy", Age = 90 },
                new Avenger { Codename = "Hulk", MainSkill = "Strength", Age = 40 },
            };

            ViewBag.avengersOrderedByName = avengers.OrderBy(e => e.Codename).ToList();
            ViewBag.avengersOrderedByAge = avengers.OrderByDescending(e => e.Age).ToList();
            ViewBag.avengersWithLetterA = avengers.Where(e => e.Codename.Contains("a")).OrderBy(e => e.Codename).ToList();
            ViewBag.avengersOlders = avengers.Where(e => e.Age > 42).OrderBy(e => e.Age).ToList();

            return View();
        }

        public IActionResult GetAvengersFromDb()
        {
            var avengersOrderedByNameQuery = db.Avengers.OrderBy(e => e.Codename);
            var avengersOrderedByName = avengersOrderedByNameQuery.ToList();
            ViewBag.avengersOrderedByName = avengersOrderedByName;

            ViewBag.avengersOrderedByAge = db.Avengers.OrderByDescending(e => e.Age).ToList();
            ViewBag.avengersWithLetterA = db.Avengers.Where(e => e.Codename.Contains("a")).OrderBy(e => e.Codename).ToList();
            ViewBag.avengersOlders = db.Avengers.Where(e => e.Age > 42).OrderBy(e => e.Age).ToList();

            return View("Index");
        }

        public IActionResult CreateOrders()
        {
            for (int i = 1; i <= 3000; i++)
            {
                var order = new Order { CustomerName = $"Customer {i}", Date = DateTime.Now.AddYears(5).AddDays(i), Items = new List<OrderItem>() };

                for (int j = 1; j <= 10; j++)
                    order.Items.Add(new OrderItem { ProductName = $"Product {j}", Quantity = j, Value = j+i });

                db.Orders.Add(order);
                db.SaveChanges();
            }

            TempData["Message"] = "Orders Created!";
            return RedirectToAction("Index");
        }

        public IActionResult Orders()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var query = db.Orders.OrderBy(e => e.Date);
            var orders = query.ToList();
            var result = new List<OrderDetailDTO>();

            foreach (var order in orders)
            {
                foreach (var item in order.Items)
                {
                    var dto = new OrderDetailDTO();
                    dto.OrderId = order.Id;
                    dto.CustomerName = order.CustomerName;
                    dto.Date = order.Date.ToString();
                    dto.OrderItemId = item.Id;
                    dto.ProductName = item.ProductName;
                    dto.Value = item.Value.ToString("c");

                    result.Add(dto);
                }
            }

            stopwatch.Stop();

            TempData["Message"] = $"Time elapsed: {stopwatch.Elapsed.TotalSeconds} seconds";

            return View(result);
        }

        public IActionResult OptimizedOrders()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var query = db
                .OrderItems
                .OrderBy(e => e.Order.Date)
                .Select(e => new {
                    OrderId = e.Order.Id,
                    e.Order.CustomerName,
                    e.Order.Date,
                    OrderItemId = e.Id,
                    e.ProductName,
                    e.Value
                });

            var queryResult = query.ToList();

            var result = queryResult.Select(e => new OrderDetailDTO {
                OrderId = e.OrderId,
                CustomerName = e.CustomerName,
                Date = e.Date.ToString(),
                OrderItemId = e.OrderItemId,
                ProductName = e.ProductName,
                Value = e.Value.ToString("c")
            }).ToList();

            stopwatch.Stop();

            TempData["Message"] = $"Time elapsed: {stopwatch.Elapsed.TotalMinutes} seconds";

            return View("Orders", result);
        }

        public IActionResult CreateAvengers()
        {
            var avengers = new List<Avenger> {
                new Avenger { Codename = "Iron Man", MainSkill = "Armor", Age = 45 },
                new Avenger { Codename = "Captain America", MainSkill = "Strategy", Age = 90 },
                new Avenger { Codename = "Hulk", MainSkill = "Strength", Age = 40 },
                new Avenger { Codename = "Captain Marvel", MainSkill = "Overpower", Age = 36 },
            };

            
            avengers.ForEach(e => db.Avengers.Add(e));
            db.SaveChanges();

            TempData["Message"] = "Avengers Created!";
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

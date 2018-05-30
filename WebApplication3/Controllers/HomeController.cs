using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        String connString = "Host=message.postgres.database.azure.com;Username=rootr@message;Password=Q123456789!;Database=message";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize]
        public ActionResult Panel()
        {
            ViewBag.Title = "Panel Page";
            List<string> authors = new List<string>();
            List<string> customers = new List<string>();
            string query = " select author_name from author";
            string query1 = " select customer_name from customer";
            authors.Add("--Choose--");
            customers.Add("--Choose--");
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                // Retrieve all rows
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        authors.Add(reader.GetString(0));


                // Retrieve all rows
                using (var cmd = new NpgsqlCommand(query1, conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        customers.Add(reader.GetString(0));
            }
            CustomerAuthor item = new CustomerAuthor();

            item.Author = authors;
            item.Customer = customers;
            return View(item);
        }
        [Authorize]
        public ActionResult Author()
        {
            ViewBag.Title = "Author Page";
            List<string> authors = new List<string>();
            List<string> customers = new List<string>();
            string query = " select author_name from author";
            string query1 = " select customer_name from customer";
            authors.Add("--Choose--");
            customers.Add("--Choose--");
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                // Retrieve all rows
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        authors.Add(reader.GetString(0));


                // Retrieve all rows
                using (var cmd = new NpgsqlCommand(query1, conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        customers.Add(reader.GetString(0));
            }
            CustomerAuthor item = new CustomerAuthor();

            item.Author = authors;
            item.Customer = customers;
            return View(item);

        }
        [Authorize]
        public ActionResult Customer()
        {
            ViewBag.Title = "Customer Page";
            ViewBag.Title = "Home Page";
            List<string> authors = new List<string>();
            List<string> customers = new List<string>();
            string query = " select author_name from author";
            string query1 = " select customer_name from customer";
            authors.Add("--Choose--");
            customers.Add("--Choose--");
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                // Retrieve all rows
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        authors.Add(reader.GetString(0));


                // Retrieve all rows
                using (var cmd = new NpgsqlCommand(query1, conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        customers.Add(reader.GetString(0));
            }
            CustomerAuthor item = new CustomerAuthor();

            item.Author = authors;
            item.Customer = customers;
            return View(item);
        }
    }
}
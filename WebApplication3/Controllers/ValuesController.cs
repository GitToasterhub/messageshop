using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication3.Controllers
{
    
    public class ValuesController : ApiController
    {
        String connString = "Host=message.postgres.database.azure.com;Username=rootr@message;Password=Q123456789!;Database=message";
        // GET: api/Values
        //1
        [Route("~/api/author/{author}/{N:int}/{f}/{t}")]
        public IEnumerable<string> Get(string author, int N, string f, string t)
        {
            List<string> list = new List<string>();

            string query = "select count(selected_table.customer_name) as numb_of_orders,selected_table.customer_name " +
               " from( " +
   " SELECT " +
 " author.author_name," +
" author_order.order_id," +
 " orders.order_date, " +
" customer.customer_name " +
" FROM " +
" author " +
" INNER JOIN author_order ON author_order.author_id = author.author_id " +
 " Inner join orders on orders.order_id = author_order.order_id " +
 " Inner join customer on customer.customer_id = orders.customer_id " +
" where author_name = '" + author + "'  and orders.order_date > '" + f + "' and orders.order_date <'" + t + "') as selected_table " +
" group by selected_table.customer_name " +
 " having count(selected_table.customer_name) > " + N.ToString();

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Insert some data
                using (var cmd = new NpgsqlCommand())
                {
                    //cmd.Connection = conn;
                    //cmd.CommandText = "Insert into \"user\".ttab values(20)";
                    //cmd.ExecuteNonQuery();
                }

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        list.Add(reader.GetString(0) + " " + reader.GetString(1));
            }


            return list;
        }
        //2
        [Route("~/api/author/{customer}/{f}/{t}")]
        public IEnumerable<string> Get(string customer, string f, string t)
        {
            List<string> list = new List<string>();
            string query = " SELECT " +
  " distinct author.author_name " +
" FROM " +
" orders " +
" INNER JOIN customer ON orders.customer_id = customer.customer_id " +
" inner join author_order on orders.order_id = author_order.order_id " +
" inner join author on author_order.author_id = author.author_id " +
" where customer_name = '" + customer + "' and orders.order_date > '" + f + "' and orders.order_date < '" + t + "'";
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                // Retrieve all rows
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        list.Add(reader.GetString(0));
            }
            return list;
        }
        //3
        [Route("~/api/author/{N:int}/{f}/{t}")]
        public IEnumerable<string> Get(int N, string f, string t)
        {
            List<string> list = new List<string>();
            string query = " select count(distinct selected_authors.customer_id) as numb_of_orders,selected_authors.author_name " +
" from " +
" (select " +
" author_name, " +
" orders.order_id, " +
" orders.customer_id " +
" from " +
" author " +
" inner join author_order on author_order.author_id = author.author_id " +
" inner " +
" join orders on author_order.order_id = orders.order_id " +
" where orders.order_date>'" + f + "' and orders.order_date<'" + t + "' " +
" ) as selected_authors " +
" group by selected_authors.author_name " +
" having count(selected_authors.customer_id) > " + N.ToString() + "; ";
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                // Retrieve all rows
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        list.Add(reader.GetString(0)+" " + reader.GetString(1));
            }
            return list;
        }


        //4
        [Route("~/api/customerr/{f}/{N:int}/{t}")]
        public IEnumerable<string> Get(string f, string t, int N)
        {
            List<string> list = new List<string>();
            string query = " select count(selected_customers.order_id) as numb_of_orders,selected_customers.customer_name " +
" from " +
" (select " +
" customer_name, " +
" orders.order_id," +
 " orders.customer_id " +
" from " +
" customer " +
" inner join orders on customer.customer_id = orders.customer_id " +
" where orders.order_date > '" + f + "' and orders.order_date < '" + t + "' " +
" ) as selected_customers " +
" group by selected_customers.customer_name " +
" having  count(selected_customers.order_id) < " + N.ToString();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                // Retrieve all rows
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        list.Add(reader.GetString(0));
            }
            return list;
        }

        //5
        [Route("~/api/customerr/{customer}/{N:int}/{f}/{t}")]
        public IEnumerable<string> Get(string f, string t, int N, string customer)
        {
            List<string> list = new List<string>();
            string query = " select count(selected_table.order_id) as numb_of_orders,selected_table.social_network_name " +
" from( " +
" select " +
" customer_name," +
" social_network.social_network_name, " +
" orders.order_id " +
" from " +
" customer " +
" inner join orders on orders.customer_id = customer.customer_id " +
" inner " +
" join message on message.message_id = orders.message_id " +
" inner " +
" join social_network on social_network.social_network_id = message.social_network_id " +
" where customer_name = '" + customer + "'  and orders.order_date > '" + f + "' and orders.order_date < '" + t + "') as selected_table " +
" group by selected_table.social_network_name " +
" having count(selected_table.order_id) >  " + N.ToString();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                // Retrieve all rows
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        list.Add(reader.GetString(0) + " " + reader.GetString(1));
            }
            return list;
        }

        //6
        [Route("~/api/author/{author}/{f}/{t}/{N:int}")]
        public IEnumerable<string> Get(string f, string t, string author, int N)
        {
            List<string> list = new List<string>();
            string query = " SELECT author.author_name, accesses.profile_id, profile.social_network_id, social_network_name FROM " +
" accesses " +
" INNER JOIN " +
" author ON author.author_id = accesses.author_id " +
" INNER JOIN profile ON accesses.profile_id = profile.profile_id " +
" INNER JOIN social_network ON profile.social_network_id = social_network.social_network_id " +
" WHERE from_date> '" + f + "'AND to_date< '" + t + "' AND author_name = '" + author + "' ";



            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                // Retrieve all rows
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        list.Add(reader.GetString(0) + " " + reader.GetString(1) + " " + reader.GetString(2) + " " + reader.GetString(3));
            }
            return list;
        }

        // GET: api/Values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Values
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Values/5
        public void Delete(int id)
        {
        }
    }
}

using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication3.Controllers
{
    public class APIController : ApiController
    {//7
        String connString = "Host=message.postgres.database.azure.com;Username=rootr@message;Password=Q123456789!;Database=message";
        [Route("~/api/customer/{customer}")]
        public IEnumerable<string> Get(string customer)
        {
            List<string> list = new List<string>();
            string query = " select " +
" author.author_name " +
" from " +
" customer " +
" inner join customer_profile on customer_profile.customer_id = customer.customer_id " +
" inner join accesses on accesses.profile_id = customer_profile.profile_id " +
" inner join author on author.author_id = accesses.author_id " +
" where customer_name = '" + customer + "'and accesses.to_date < date(now())";
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

        //10
        [Route("~/api/customer/{customer}/{N:int}")]
        public IEnumerable<string> Get(string customer, int N)
        {
            List<string> list = new List<string>();
            string query = "select count(selected_table.order_id) as numb_of_orders_with_discount,selected_table.type " +
" from(select " +
" customer_name, " +
" orders.order_id, " +
" message.style_id, " +
" message_style.type, " +
" orders.order_date " +
" from " +
" customer " +
" inner join orders on orders.customer_id = customer.customer_id " +
" inner " +
" join message on message.message_id = orders.message_id " +
" inner " +
" join message_style on message_style.style_id = message.style_id " +
" inner " +
" join discounts on discounts.style_id = message_style.style_id " +
" inner " +
" join author_message on author_message.message_id = message.message_id " +

" where customer_name = '" + customer + "'and discounts.author_id = author_message.author_id and orders.order_date >= discounts.from_date and orders.order_date <= discounts.to_date) as selected_table " +

" group by selected_table.type ";
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


        //11
        [Route("~/api/customer/{f}/{t}")]
        public IEnumerable<string> Get(string f, string t)
        {
            List<string> list = new List<string>();
            string query = "SELECT COUNT(*) " +
" FROM orders " +
" WHERE orders.order_date >= '" + f + "' " +
" AND orders.order_date <= '" + t + "'";
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

        //12
        [Route("~/api/customer/{customer}/{f}/{t}")]
        public IEnumerable<string> Get(string customer, string f, string t)
        {
            List<string> list = new List<string>();
            string query = "select count(all_messages.type) as messages_in_SN, all_messages.social_network_name " +
" from(select author_name, " +
     " social_network.social_network_name, " +
" message_style.type, " +
     " orders.order_date " +
" from author " +

" inner join author_message on author_message.author_id = author.author_id " +
" inner " +
" join message on message.message_id = author_message.message_id " +
" inner " +
" join message_style on message_style.style_id = message.style_id " +
" inner " +
" join social_network on social_network.social_network_id = message.social_network_id " +
" inner " +
" join orders on orders.message_id = message.message_id " +
" where author_name = '" + customer + "'and orders.order_date > '" + f + "' and orders.order_date < '" + t + "') as all_messages " +
" group by all_messages.social_network_name " +
" order by messages_in_SN desc";
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

        //8
        [Route("~/api/customer/{author}/{customer}/{f}/{t}")]
        public IEnumerable<string> Get(string customer, string f, string t, string author)
        {
            List<string> list = new List<string>();
            string query = " select " +
" author.author_name, customer_name, order_date, message_style.type, orders.profile_id, accesses.from_date as gave_access, accesses.to_date as took_access_back " +

" from customer " +
" inner join orders on orders.customer_id = customer.customer_id " +
" inner " +
" join author_order on author_order.order_id = orders.order_id " +
" inner " +
" join author on author.author_id = author_order.author_id " +
" inner " +
" join message on message.message_id = orders.message_id " +
" inner " +
" join message_style on message_style.style_id = message.style_id " +
" inner " +
" join accesses on accesses.profile_id = orders.profile_id " +
" where author_name = '" + author + "' and customer_name = '" + customer + "' and orders.order_date > '" + f + "' and orders.order_date < '" + t + "'";
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

        //9
        [Route("~/api/customer/{N:int}/{author}/{f}/{t}")]
        public IEnumerable<string> Get(int N, string f, string t, string author)
        {
            List<string> list = new List<string>();
            string query = " select count(number_of_times_he_wrote_in_group) " +
" from(select " +
" count(selected_messages.message_id) as number_of_people_in_group, selected_messages.social_network_name " +
"  from " +
" (select " +
"  author_name, " +
" author_message.message_id, " +
" social_network.social_network_name, " +
 " orders.order_date " +
" from " +
" author " +
" inner join author_message on author_message.author_id = author.author_id " +
" inner " +
" join message on author_message.message_id = message.message_id " +
" inner " +
" join social_network on social_network.social_network_id = message.social_network_id " +
" inner " +
" join orders on orders.message_id = message.message_id " +
"where author_name = '" + author + "'and orders.order_date > '" + f + "' and orders.order_date < '" + t + "') as selected_messages " +
" inner join orders on orders.message_id = selected_messages.message_id " +
" inner join author_order on author_order.order_id = orders.order_id " +
" group by selected_messages.social_network_name " +
" having count(selected_messages.message_id) >= " + N.ToString() + ") as number_of_times_he_wrote_in_group ";
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

        // GET: api/API/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/API
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/API/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/API/5
        public void Delete(int id)
        {
        }
    }
}

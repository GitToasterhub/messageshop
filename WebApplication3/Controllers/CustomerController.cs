using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication3.Controllers
{
    public class CustomerController : ApiController
    {
        String connString = "Host=message.postgres.database.azure.com;Username=rootr@message;Password=Q123456789!;Database=message";
        //GET customer
        [Route("~/api/customer")]
        public IEnumerable<string> Get()
        {
            List<string> list = new List<string>();
            string query = " select " +
" customer.customer_name " +
" from " +
" customer ";
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


        // GET: api/Customer/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Customer
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Customer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Customer/5
        public void Delete(int id)
        {
        }
    }
}

using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication3.Controllers
{
    public class AuthorController : ApiController
    {
        String connString = "Host=message.postgres.database.azure.com;Username=rootr@message;Password=Q123456789!;Database=message";
        //GET author
        [Route("~/api/author")]
        public IEnumerable<string> Get()
        {
            List<string> list = new List<string>();
            string query = " select " +
" author.author_name " +
" from " +
" author ";
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

        // GET: api/AC/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/AC
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/AC/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AC/5
        public void Delete(int id)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.Repositories
{
    public class BlogRepository : DatabaseConnector, IRepository<Blog>
    {
        public BlogRepository(string connectionString) : base(connectionString) { }


        // the below mehtod allows a blog to be inserted into the database
        public void Insert(Blog blog)
        {
            
            using(SqlConnection conn = Connection)
            {
                conn.Open();

                using(SqlCommand cmd = conn.CreateCommand())
                {
                    //below is the querry that needs to be excuted
                        
                    cmd.CommandText = @"INSERT INTO Blog (Title, URL) OUTPUT INSERTED.Id VALUES
								(@Title, @URL)";

                    cmd.Parameters.AddWithValue("@Title", blog.Title);
                    cmd.Parameters.AddWithValue("@URL", blog.Url);

                    int id = (int)cmd.ExecuteScalar();

                    blog.Id = id;
                }
            }
        }


        public List<Blog> GetAll()
        {
            throw new NotImplementedException();

        }

        public Blog Get(int id)
        {
            throw new NotImplementedException();

        }


        public void Update(Blog blog)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }


    }
}

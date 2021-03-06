﻿using System;
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
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Title, URL FROM Blog";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Blog> allBlog = new List<Blog>();

                    //reader.Read() will return true as long as there is more data to read
                    while(reader.Read())
                    {
                        //get column positions and values

                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int titlePosition = reader.GetOrdinal("Title");
                        string titleValue = reader.GetString(titlePosition);


                        int urlPosition = reader.GetOrdinal("Url");
                        string urlValue = reader.GetString(urlPosition);

                        Blog blog = new Blog
                        {
                            Id = idValue,
                            Title = titleValue,
                            Url = urlValue
                        };

                        allBlog.Add(blog);
                        
                    }

                    reader.Close();

                    return allBlog;


                }
            }
        }

        public Blog Get(int id)
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id, title, url FROM Blog WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    Blog blog = null;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        if(blog == null)
                        {
                            blog = new Blog
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Url = reader.GetString(reader.GetOrdinal("url"))

                            };
                        }
                    }

                    reader.Close();
                    return blog;

                }
            }

        }


        public void Update(Blog blog)
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();

                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Blog SET Title = @title, Url = @url WHERE id = @id";

                    cmd.Parameters.AddWithValue("@id", blog.Id);
                    cmd.Parameters.AddWithValue("@title", blog.Title);
                    cmd.Parameters.AddWithValue("@url", blog.Url);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {

            using(SqlConnection conn = Connection)
            {
                conn.Open();

                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Blog WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}

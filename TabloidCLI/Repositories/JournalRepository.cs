using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI
{
    //Created by Brett Stoudt
    public class JournalRepository : DatabaseConnector, IRepository<Journal>
    {
        //
        public JournalRepository(string connectionString) : base(connectionString) { }


        //ALL JOURNALS
        public List<Journal> GetAll()
        {
            //SETUP CONNECTION TO SQL USING ADO.NET
            using (SqlConnection conn = Connection)
            {
                //OPEN CONNECTION
                conn.Open();
                //CREATE A SINGLE COMMAND TO SEND TO SQL SERVER, JOURNAL TABLE
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id,
                                               Title,
                                               Content,
                                               CreateDateTime
                                          FROM Journal";

                    //CREATE LIST FOR JOURNALS TO BE RETURNED TO
                    List<Journal> journals = new List<Journal>();

                    //EXECUTES COMMAND ABOVE AND IF RESULTS COME BACK IT READS THE RESPONSE (QUERY) AND CREATES A NEW JOURNAL OBJECT TO PLACE IN THE LIST
                   // MUST CLOSE THIS READER METHOD ONCE RESPONSE TO ADD TO DATABASE HAS BEEN COMPLETED
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Journal journal = new Journal()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                        };
                        journals.Add(journal);
                    }

                    reader.Close();

                    return journals;
                }
            }
        }

        
       
        
        //PLACES DATA IN TABLE
        //DOES NOT REQUIRE A QUERY IN THE CMD EXECUTE SINCE WE DO NOT HAVE ANY DATA RELATIONSHIPS WITH THIS TABLE
        public void Insert(Journal journal)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Journal (Title, Content, CreateDateTime)
                                                     VALUES (@title, @content, @createDateTime)";
                    cmd.Parameters.AddWithValue("@title", journal.Title);
                    cmd.Parameters.AddWithValue("@content", journal.Content);
                    cmd.Parameters.AddWithValue("@createDateTime", journal.CreateDateTime);
                   
                    //NO RESPONSE BACK, NOT A QUERY
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //UPDATE JOURNAL, NON QUERY EXECUTE
        public void Update(Journal journal)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Journal 
                                           SET Title = @title,
                                               Content = @content,
                                               CreateDateTime = @createDateTime
                                         WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@title", journal.Title);
                    cmd.Parameters.AddWithValue("@content", journal.Content);
                    cmd.Parameters.AddWithValue("@createDateTime", journal.CreateDateTime);
                    cmd.Parameters.AddWithValue("@id", journal.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Journal WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        //NOT USED YET, BUT REQUIRED IN IREPOSITORY AS A SHARED METHOD
        public Journal Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}

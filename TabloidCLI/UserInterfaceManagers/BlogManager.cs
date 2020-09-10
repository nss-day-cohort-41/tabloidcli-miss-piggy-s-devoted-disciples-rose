using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    class BlogManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private string _connectionString;


        public BlogManager(IUserInterfaceManager parentUi, string connectionString)
        {
            _connectionString = connectionString;
            _parentUI = parentUi;
            _blogRepository = new BlogRepository(_connectionString);
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Blog Menu");
            Console.WriteLine(" 1) List Blogs");
            Console.WriteLine(" 3) Add a Blog");
            Console.WriteLine(" 0) Go Back");
            Console.Write("> ");

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    List();
                    return this;
                case "3":
                    Insert();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;

            }
        }

        private void Insert()
        {
            Console.WriteLine("New Blog!");

            Blog blog = new Blog();

            Console.Write("Enter Blog Title: ");
            blog.Title = Console.ReadLine();

            Console.Write("Enter Blog URl: ");
            blog.Url = Console.ReadLine();

            _blogRepository.Insert(blog);

        }

        private void List()
        {
            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog blog in blogs)
            {
                Console.WriteLine(blog);
            }
        }



    }
}

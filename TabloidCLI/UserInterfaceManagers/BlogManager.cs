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
            Console.WriteLine();
            Console.WriteLine("Blog Menu");
            Console.WriteLine(" 1) List Blogs");
            Console.WriteLine(" 2) Blog Details");
            Console.WriteLine(" 3) Add a Blog");
            Console.WriteLine(" 4) Edit Blog");
            Console.WriteLine(" 5) Remove Blog");
            Console.WriteLine(" 0) Go Back");
            Console.Write("> ");

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Blog blog = Choose();
                    if (blog == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new BlogDetailManager(this, _connectionString, blog.Id);
                    }
                case "3":
                    Insert();
                    return this;
                case "4":
                    Edit();
                    return this;
                case "5":
                    Remove();
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
            Console.WriteLine();
            Console.WriteLine("All Blog Entries:");
            foreach (Blog blog in blogs)
            {
                Console.WriteLine(blog);
            }
        }

        private Blog Choose(string prompt = null)
        {
            if (prompt == null)
            {
                Console.WriteLine();
                prompt = "Please choose an Author:";
            }
            Console.WriteLine(prompt);

            List<Blog> blogs = _blogRepository.GetAll();

            for (int i = 0; i < blogs.Count; i++)
            {
                Blog blog = blogs[i];
                Console.WriteLine($" {i + 1}) {blog.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            Console.WriteLine();
            try
            {
                int choice = int.Parse(input);
                return blogs[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }


        private void Remove()
        {
            Blog blogToDelete = Choose("Which blog would you like to remove?");

            if(blogToDelete != null)
            {
                _blogRepository.Delete(blogToDelete.Id);
            }

        }



        public void Edit()
        {
            Blog blogToEdit = Choose("Which blog would you like to remove?");

            if (blogToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New title (blank to leave unchanged: ");

            string title = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(title))
            {
                blogToEdit.Title = title;
            }

            Console.Write("New URL (blank to leave unchanged: ");
            string url = Console.ReadLine();

            if(!string.IsNullOrWhiteSpace(url))
            {
                blogToEdit.Url = url;
            }

            _blogRepository.Update(blogToEdit);


        }



    }
}

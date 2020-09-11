using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Post Details");
            Console.WriteLine(" 3) Add Post");
            Console.WriteLine(" 4) Edit Post");
            Console.WriteLine(" 5) Remove Post");
            Console.WriteLine(" 6) Note Management");
            Console.WriteLine(" 0) Return to Main Menu");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Post post = Choose();
                    if (post == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new PostDetailManager(this, _connectionString, post.Id);
                    }
                case "3":
                    Add();
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

        private void List()
        {
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine(post);
            }
        }

        private Post Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Post:";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Add()
        {
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.Write("Title: ");
            post.Title = Console.ReadLine();

            Console.Write("URL: ");
            post.Url = Console.ReadLine();

            post.PublishDateTime = DateTime.Now;

            // Lists All authors with their ID
            // Then parses user input from string to integer
            // Author object is assigned an author using parsed response
            Console.WriteLine("Choose an Author: ");
            List<Author> authors = _authorRepository.GetAll();
            foreach (Author author in authors)
            {
                Console.WriteLine($"{author.Id}. {author.FullName}");
            }
            string authorInput = Console.ReadLine();
            int authorId = Int32.Parse(authorInput);
            
            
            if (authorId == 0  || authorId > authors.Count)
            {
                Console.WriteLine("Invalid selection. Choose an Author: ");
                authors = _authorRepository.GetAll();
                foreach (Author author in authors)
                {
                    Console.WriteLine($"{author.Id}. {author.FullName}");
                }
                authorInput = Console.ReadLine();
                authorId = Int32.Parse(authorInput);
            }
            else
            {
                post.Author = _authorRepository.Get(authorId);
            }

            // Lists All blogs with their ID
            // Then parses user input from string to integer
            // Author object is assigned a blog using parsed response
            
            Console.Write("Choose a Blog: ");
            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog blog in blogs)
            {
                Console.WriteLine($"{blog.Id}. {blog.Title}");
            }
            string blogInput = Console.ReadLine();
            int blogId = Int32.Parse(blogInput);
            
            post.Blog = _blogRepository.Get(blogId);

            _postRepository.Insert(post);
        }

        private void Edit()
        {
            Post postToEdit = Choose("Which post would you like to edit?");
            if (postToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New Title (blank to leave unchanged: ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                postToEdit.Title = title;
            }

            Console.Write("New URL (blank to leave unchanged: ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                postToEdit.Url = url;
            }

            DateTime dateTime = DateTime.Now;
            postToEdit.PublishDateTime = dateTime;

            // EDIT AUTHOR ASSIGMENT
            List<Author> authors = _authorRepository.GetAll();
            foreach (Author author in authors)
            {
                Console.WriteLine($"{author.Id}. {author.FullName}");
            }
            Console.Write("New Author (blank to leave unchanged: ");
            string authorInput = Console.ReadLine();
            int authorId = Int32.Parse(authorInput);
            if (!string.IsNullOrWhiteSpace(authorInput))
            {
                postToEdit.Author = _authorRepository.GetById(authorId);
            }

            // EDIT BLOG ASSIGNMENT
            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog blog in blogs)
            {
                Console.WriteLine($"{blog.Id}. {blog.Title}");
            }
            Console.Write("New Blog (blank to leave unchanged: ");
            string blogInput = Console.ReadLine();
            int blogId = Int32.Parse(blogInput);
            if (!string.IsNullOrWhiteSpace(blogInput))
            {
                postToEdit.Blog = _blogRepository.Get(blogId);
            }

            _postRepository.Update(postToEdit);
        }

        private void Remove()
        {
            Post postToDelete = Choose("Which post would you like to remove?");
            if (postToDelete != null)
            {
                _postRepository.Delete(postToDelete.Id);
            }
        }
    }
}

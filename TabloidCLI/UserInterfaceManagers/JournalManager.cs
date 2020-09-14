using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    //Created by Brett Stoudt

    public class JournalManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;
        private string _connectionString;

        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine();
            Console.WriteLine("Journal Menu");
            Console.WriteLine(" 1) List Entries");
            Console.WriteLine(" 2) Add Entry");
            Console.WriteLine(" 3) Edit Entry");
            Console.WriteLine(" 4) Remove Entry");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                /*
                case "2":
                    Author author = Choose();
                    if (author == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new AuthorDetailManager(this, _connectionString, author.Id);
                    }
                */
                case "2":
                    Add();
                    return this;
                    
                case "3":
                    Edit();
                    return this;
                
                case "4":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;

                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        //GETALL Journal Entries
        private void List()
        {
            List<Journal> journals = _journalRepository.GetAll();
            Console.WriteLine();
            Console.WriteLine("All Journal Entries:");
            foreach (Journal journal in journals)
            {
                Console.WriteLine(journal);
            }
        }
        private Journal Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Journal:";
            }

            Console.WriteLine(prompt);

            List<Journal> journals = _journalRepository.GetAll();

            for (int i = 0; i < journals.Count; i++)
            {
                Journal journal = journals[i];
                Console.WriteLine($" {i + 1}) {journal.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            Console.WriteLine();
            try
            {
                int choice = int.Parse(input);
                return journals[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }
       
        private void Add()
        {
            Console.WriteLine("New Journal Entry");
            Journal journal = new Journal();

            Console.Write("Title: ");
            journal.Title = Console.ReadLine();

            Console.Write("Content: ");
            journal.Content = Console.ReadLine();

            Console.Write($"Current Date & Time : {DateTime.Now} ");
            journal.CreateDateTime = DateTime.Now;
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            _journalRepository.Insert(journal);
        }

        
        private void Edit()
        {
            Journal journalToEdit = Choose("Which Journal Entry would you like to edit?");
            if (journalToEdit == null)
            {
                return;
            }

            
            Console.Write("New Title (blank to leave unchanged): ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                journalToEdit.Title = title;
            }
            Console.Write("New Content (blank to leave unchanged): ");
            string content = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(content))
            {
                journalToEdit.Content = content;
            }

            _journalRepository.Update(journalToEdit);
        }

  
        private void Remove()
        {
            Console.WriteLine();
            Journal journalToDelete = Choose("Which journal entry would you like to remove?");
            if (journalToDelete != null)
            {
                Console.WriteLine($"{journalToDelete.Title} Deleted");
                _journalRepository.Delete(journalToDelete.Id);
            }
        }
    }
}

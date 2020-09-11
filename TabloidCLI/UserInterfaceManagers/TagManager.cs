using System;
using System.Collections.Generic;
using TabloidCLI.Models;
/// <summary>
/// Author of TagManager TLane
/// </summary>
namespace TabloidCLI.UserInterfaceManagers
{
    public class TagManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private TagRepository _tagRepository;
        private string _connectionString;

        public TagManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _tagRepository = new TagRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Tag Menu");
            Console.WriteLine(" 1) List Tags");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Edit Tag");
            Console.WriteLine(" 4) Remove Tag");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
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

        private Tag Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Tag:";
            }

            Console.WriteLine(prompt);

            List<Tag> tags = _tagRepository.GetAll();

            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return tags[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void List()
        {

            List<Tag> allTags = _tagRepository.GetAll();
            Console.WriteLine($"TagID\tTag Name");
            foreach (Tag tag in allTags)
            {
                Console.WriteLine($"{tag.Id}\t{tag.Name}");
            }

        }

        


        private void Add()
        {
            Console.WriteLine("Please add a Tag");
            Console.WriteLine("Tag Name?  (ENTER to cancel)");

            string tagName = Console.ReadLine();
            if (tagName != "")
            {
                Tag tag = new Tag { Name = tagName };
                _tagRepository.Insert(tag);
                Console.WriteLine("-----------------------------");

                Console.WriteLine($"Added the new Tag with id {tag.Id}");
            }

        }

        private void Edit()
        {
            Tag tagToEdit = Choose("Which tag would you like to edit?");
            if (tagToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New name (blank to leave unchanged: ");
            string name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                tagToEdit.Name = name;
            }


            _tagRepository.Update(tagToEdit);

            Console.WriteLine("Updated Tag List: ");
            List();
        }

        private void Remove()
        {
            Tag tagToDelete = Choose("Which tag would you like to remove?");
            if (tagToDelete != null)
            {
                _tagRepository.Delete(tagToDelete.Id);//Delete Id
            }
            Console.WriteLine($"{tagToDelete.Name} has been deleted.");//Delete Name
            List();
        }
    }
}
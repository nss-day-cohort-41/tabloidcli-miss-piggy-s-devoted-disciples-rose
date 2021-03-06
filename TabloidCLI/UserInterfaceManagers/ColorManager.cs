﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TabloidCLI.UserInterfaceManagers
{
    //Created by Brett Stoudt
    class ColorManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;


        public ColorManager(IUserInterfaceManager parentUI)
        {
            _parentUI = parentUI;
        }
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine();
            Console.WriteLine("Color Menu");
            Console.WriteLine(" 1) DarkMagenta / White 'Miss Piggys Favorit`");
            Console.WriteLine(" 2) White Background / Black Text");
            Console.WriteLine(" 3) Grey Background / Black Text");
            Console.WriteLine(" 4) Cyan Background/ Black Text");
            Console.WriteLine(" 5) White Background/ Dark Red Text");
            Console.WriteLine(" 9) Black Background / White Text 'Original'");

            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Clear();
                    return Execute();

                case "2":
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Clear();
                    return Execute();
                case "3":
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Clear();
                    return Execute();
                case "4":
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Clear();
                    return Execute();
                case "5":
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Clear();
                    return Execute();
                case "9":
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Clear();
                    return Execute();

                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine($"Invalid Selection, {choice} is not a number provided here. Please choose from a number listed above");
                    return this;
            }
        }

    }
}

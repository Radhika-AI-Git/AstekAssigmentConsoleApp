using GICBankingApp;
using System;

namespace GICBankingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var bankSystem = new BankSystem();
            string choice;

            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to Awesome GIC Bank! What would you like to do?");
                Console.WriteLine("[T] Input transactions");
                Console.WriteLine("[I] Define interest rules");
                Console.WriteLine("[P] Print statement");
                Console.WriteLine("[Q] Quit");
                Console.Write("Enter your choice: ");
                choice = Console.ReadLine();
               

                switch (choice.ToUpper())
                {
                    case "T":
                        bankSystem.InputTransaction();
                        break;
                    case "I":
                        bankSystem.DefineInterestRule(); ;
                        break;
                    case "P":
                        bankSystem.PrintAccountStatement(); ;
                        break;
                    case "Q":
                        Console.WriteLine("Thank you for banking with Awesome GIC Bank.\r\n Have a nice day!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }

                if (choice != "Q")
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }

            } while (choice != "Q");
        }       
        
    }
}
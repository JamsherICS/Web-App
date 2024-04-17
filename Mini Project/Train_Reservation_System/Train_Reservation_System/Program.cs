using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Train_Reservation_System
{
    class Program
    {
        static void Main(string[] args)
        {

            mainFunction();
            
        }


        public static void mainFunction()
        {
            Console.WriteLine("********************************************************************");
            Console.WriteLine("         ------Welcome to Railway Resevation System------");
            Console.WriteLine("********************************************************************");
            Console.WriteLine("\n  Admin -> 1\n  User  -> 2\n  Exit  -> 0");
            Console.WriteLine();
            Console.Write("Enter your choice: ");
            

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 0 || choice > 2))
            {
                Console.WriteLine("Please enter a valid option (1, 2, or 0).");
                Console.Write("Enter your choice: ");
                Console.WriteLine();
            }

            switch (choice)
            {
                case 0:                    
                    Console.WriteLine("Exiting the program...");
                    return; // Exit the program
                case 1:
                    Console.WriteLine();
                    Console.WriteLine("...............Admin option selected...............");
                    Console.WriteLine();
                    // Call method for admin login
                    AdminClass.adminFunction();
                    break;
                case 2:
                    Console.WriteLine();
                    Console.WriteLine("...............User option selected...............");
                    Console.WriteLine();
                    // Call method for user login
                    UserClass.userFunction();
                    break;
            }
        }
    }
}

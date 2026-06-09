using System;

// This class is placed in the same namespace so it belongs to the same project group.
namespace HealthcareDecisionApp
{
    // internal = accessible only within this project, ideal for console app components.
    internal class AppointmentRouter
    {
        // This is the entry point for this class. To run this file first,
        // change the Startup Object in project properties.
        static void Main(string[] args)
        {
            Console.WriteLine("=== Hospital Appointment Routing System ===");
            Console.WriteLine("Select the type of service needed:");
            Console.WriteLine("1. Cardiology");
            Console.WriteLine("2. Orthopedics");
            Console.WriteLine("3. Pediatrics");
            Console.WriteLine("4. Dermatology");
            Console.WriteLine("5. Emergency Care");

            Console.Write("Enter your choice (1-5): ");

            // Convert.ToInt32 converts the user input (string) into an integer.
            int choice = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            // switch-case is used here because there are fixed, known options.
            // It is cleaner and more readable than multiple else-if blocks.
            switch (choice)
            {
                case 1 or 2:
                    Console.WriteLine("Routing to Cardiology Department...");
                    // Example of deeper logic within a case
                    Console.WriteLine("Please proceed to Block A, Room 104.");
                    break;

                case 2:
                    Console.WriteLine("Routing to Orthopedics Department...");
                    Console.WriteLine("Please proceed to Block B, Room 210.");
                    break;

                case 3:
                    Console.WriteLine("Routing to Pediatrics Department...");
                    Console.WriteLine("Please proceed to Block C, Room 305.");
                    break;

                case 4:
                    Console.WriteLine("Routing to Dermatology Department...");
                    Console.WriteLine("Please proceed to Block D, Room 120.");
                    break;

                case 5:
                    Console.WriteLine("Emergency Care Selected.");
                    // Demonstrates nested logic inside a case.
                    Console.Write("Is the patient experiencing severe symptoms (yes/no)? ");
                    string severity = Console.ReadLine().ToLower();

                    if (severity == "yes")
                    {
                        Console.WriteLine("Immediate attention required. Proceed to Emergency Bay 1.");
                    }
                    else
                    {
                        Console.WriteLine("Proceed to Emergency Triage Desk for assessment.");
                    }
                    break;

                // default executes when none of the above cases match.
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Process completed.");
        }
    }
}


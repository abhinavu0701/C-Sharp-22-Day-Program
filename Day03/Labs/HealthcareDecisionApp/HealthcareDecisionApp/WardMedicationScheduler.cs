using System;
using System.Collections.Generic;

namespace HealthcareDecisionApp
{
    internal class WardMedicationScheduler
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Ward Medication Scheduling System ===");

            // List of patient names; foreach is ideal for iterating collections.
            List<string> patients = new List<string>
            {
                "John Carter",
                "Maria Fernandes",
                "Rakesh Nair"
            };

            // foreach loop is used when the count is unknown or irrelevant
            // and you simply want to process each element.
            foreach (string patient in patients)
            {
                Console.WriteLine("\nScheduling medication for: " + patient);

                // for loop schedules 3 medication times per day.
                // for is chosen because we know the exact number of reminders.
                for (int timeSlot = 1; timeSlot <= 3; timeSlot++)
                {
                    Console.WriteLine("  Medication Reminder Slot " + timeSlot + " scheduled.");
                }

                Console.WriteLine("All medication reminders created for " + patient);
            }

            Console.WriteLine("\nScheduling completed for all patients.");
        }
    }
}


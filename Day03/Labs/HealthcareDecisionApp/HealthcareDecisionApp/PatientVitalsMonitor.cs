using System;

namespace HealthcareDecisionApp
{
    internal class PatientVitalsMonitor
    {
        // To run this first, set this class as Startup Object in project properties.
        static void Main(string[] args)
        {
            Console.WriteLine("=== ICU Patient Vitals Monitoring Simulation ===");

            // Current oxygen level; the while loop will run until the level is safe.
            int oxygenLevel = 88;

            // The while loop continues as long as condition is true.
            // Useful for ongoing monitoring where the end condition is unknown.
            while (oxygenLevel < 95)
            {
                Console.WriteLine("\nMonitoring cycle started...");

                // The for loop simulates multiple sensor readings per cycle.
                // for loops are best when the number of iterations is known.
                for (int i = 1; i <= 3; i++)
                {
                    Console.WriteLine("Reading " + i + ": Oxygen Level = " + oxygenLevel + "%");
                }

                Console.WriteLine("Oxygen low. Administering oxygen support...");

                // Increase level to simulate improvement.
                oxygenLevel += 3;

                // Warning to avoid accidental infinite loops.
                // The while condition will eventually become false.
                Console.WriteLine("Updated Oxygen Level = " + oxygenLevel);
            }

            Console.WriteLine("\nPatient oxygen stabilized. Monitoring stopped.");
        }
    }
}

using System;

// Namespace groups related classes of this project together.
namespace HealthcareDecisionApp
{
    // 'internal' = class accessible only inside this project (default for console apps).
    internal class Program
    {
        // Main method = entry point of the application. Program starts running from here.
        static void Main(string[] args)
        {
            Console.WriteLine("=== TRIAGE DECISION SYSTEM ===");

            // Input Data
            Console.Write("Enter Patient Heart Rate (bpm): ");
            // Convert.ToInt32() → converts the user's text input into an integer.
            int heartRate = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Oxygen Saturation (SpO2 %): ");
            int oxygen = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Temperature (°C): ");
            float temperature = float.Parse(Console.ReadLine());

            Console.Write("Enter Pain Level (1 to 10): ");
            int pain = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nEvaluating triage level...\n");

            string triage;

            // Complex Decision Logic
            if (oxygen < 85 || heartRate > 160)
            {
                triage = "RED – Immediate Emergency Care Required";
            }
            else if ((oxygen >= 85 && oxygen < 92)
                     || (heartRate >= 120 && heartRate <= 160)
                     || temperature > 39
                     || pain >= 8)
            {
                triage = "YELLOW – Urgent Care Needed";
            }
            else if ((oxygen >= 92 && oxygen <= 95)
                     || (temperature >= 37.5 && temperature <= 39)
                     || (pain >= 4 && pain < 8))
            {
                triage = "GREEN – Stable but Needs Observation";
            }
            else
            {
                triage = "BLUE – Non-critical; Basic Consultation";
            }

            Console.WriteLine($"Final Triage Category: {triage}");
        }
    }
}

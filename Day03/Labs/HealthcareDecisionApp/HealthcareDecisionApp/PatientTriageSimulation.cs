using System;
using System.Collections.Generic;

namespace HealthcareDecisionApp
{
    internal class PatientTriageSimulation
    {
        // Entry point for this simulation class.
        static void Main(string[] args)
        {
            Console.WriteLine("=== Emergency Room Triage Simulation ===");

            // A list of simulated patients arriving at the ER.
            List<string> incomingPatients = new List<string>
            {
                "John Carter",
                "Emily Watson",
                "Rajiv Menon",
                "Sophia Martinez"
            };

            // Process each patient using functional decomposition.
            foreach (string patient in incomingPatients)
            {
                Console.WriteLine("\nProcessing new arrival: " + patient);

                // Step 1: Collect vital signs (simulated values).
                var vitals = CollectVitals(patient);

                // Step 2: Classify triage level using the vitals.
                string triageLevel = DetermineTriageLevel(vitals);

                // Step 3: Assign the patient to the correct care unit.
                string assignedUnit = AssignCareUnit(triageLevel);

                // Step 4: Display the summary for training clarity.
                DisplaySummary(patient, vitals, triageLevel, assignedUnit);
            }

            Console.WriteLine("\nSimulation completed.");
        }

        // This method collects simulated vitals rather than asking user input.
        // Functional decomposition allows the logic to be reused in other workflows.
        private static (int heartRate, int oxygen, float temperature) CollectVitals(string patientName)
        {
            Random rand = new Random();

            // Simulating random vitals for the patient.
            int heartRate = rand.Next(60, 180);       // realistic range
            int oxygen = rand.Next(82, 100);
            float temperature = (float)(rand.NextDouble() * (40 - 36) + 36);

            Console.WriteLine("Collected Vitals:");
            Console.WriteLine("  Heart Rate: " + heartRate);
            Console.WriteLine("  Oxygen: " + oxygen);
            Console.WriteLine("  Temperature: " + temperature.ToString("0.0"));

            return (heartRate, oxygen, temperature);
        }

        // This method encapsulates all triage rules.
        // This keeps main flow readable and makes the logic reusable.
        private static string DetermineTriageLevel((int heartRate, int oxygen, float temperature) vitals)
        {
            int hr = vitals.heartRate;
            int ox = vitals.oxygen;
            float temp = vitals.temperature;

            if (ox < 85 || hr > 165)
                return "RED";

            if (ox < 92 || hr > 130 || temp > 39.0)
                return "YELLOW";

            if (ox < 96 || temp > 37.8)
                return "GREEN";

            return "BLUE";
        }

        // Each triage level maps to a different care area.
        // This separation of logic is the core of functional decomposition.
        private static string AssignCareUnit(string triageLevel)
        {
            switch (triageLevel)
            {
                case "RED":
                    return "Critical Care Unit (Immediate Intervention)";

                case "YELLOW":
                    return "Urgent Care Ward";

                case "GREEN":
                    return "Observation Bay";

                case "BLUE":
                    return "Standard Outpatient Area";

                default:
                    return "Unknown Unit";
            }
        }

        // Final method responsible ONLY for displaying collected information.
        // Clear separation of tasks makes the program maintainable and testable.
        private static void DisplaySummary(
            string name,
            (int heartRate, int oxygen, float temperature) vitals,
            string triage,
            string unit)
        {
            Console.WriteLine("\nSummary for " + name + ":");
            Console.WriteLine("  Heart Rate: " + vitals.heartRate);
            Console.WriteLine("  Oxygen: " + vitals.oxygen + "%");
            Console.WriteLine("  Temperature: " + vitals.temperature.ToString("0.0") + "°C");
            Console.WriteLine("  Triage Level: " + triage);
            Console.WriteLine("  Assigned Unit: " + unit);
        }
    }
}

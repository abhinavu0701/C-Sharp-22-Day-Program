using System;

namespace HealthcareDecisionApp
{
    internal class DiabetesRisk
    {
        // THIS is now the entry point
        public static void Main(string[] args)
        {
            Console.WriteLine("=== DIABETES RISK ASSESSMENT SYSTEM ===");

            Console.Write("Enter Fasting Blood Sugar (mg/dL): ");
            int fbs = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter HbA1c (%): ");
            float hba1c = float.Parse(Console.ReadLine());

            Console.Write("Enter BMI: ");
            float bmi = float.Parse(Console.ReadLine());

            Console.Write("Family History of Diabetes? (yes/no): ");
            string familyHistory = Console.ReadLine().ToLower();

            Console.WriteLine("\nAnalyzing risk...\n");

            string risk;

            if (fbs > 180 || hba1c > 8.0 || (bmi > 32 && familyHistory == "yes"))
            {
                risk = "HIGH RISK – Immediate Endocrinology Referral Recommended";
            }
            else if ((fbs >= 140 && fbs <= 180)
                     || (hba1c >= 6.5 && hba1c <= 8.0)
                     || (bmi >= 28 && bmi <= 32))
            {
                risk = "MODERATE RISK – Lifestyle Modification + Monitoring Required";
            }
            else if ((fbs >= 100 && fbs < 140)
                     || (hba1c >= 5.7 && hba1c < 6.5)
                     || (bmi >= 25 && bmi < 28))
            {
                risk = "LOW RISK – Basic Diet Advice & Annual Checkup";
            }
            else
            {
                risk = "NO RISK – Continue Healthy Lifestyle";
            }

            Console.WriteLine($"Diabetes Risk Level: {risk}");
        }
    }
}

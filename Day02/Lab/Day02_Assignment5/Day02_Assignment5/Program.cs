using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    static string connectionString =
        "Server=YOUR_SERVER;Database=CareBridgeDB;Trusted_Connection=True;";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n===== CareBridge Clinical Operations Console =====");
            Console.WriteLine("1. 30-Day Readmissions");
            Console.WriteLine("2. High-Risk Patients");
            Console.WriteLine("3. Provider Workload");
            Console.WriteLine("4. Revenue Analysis");
            Console.WriteLine("5. Exit");
            Console.Write("Select option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RunProcedure("sp_Readmissions30Days");
                    break;

                case "2":
                    RunProcedure("sp_HighRiskPatients");
                    break;

                case "3":
                    RunProcedure("sp_ProviderWorkload");
                    break;

                case "4":
                    RunProcedure("sp_RevenueAnalysis");
                    break;

                case "5":
                    return;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    static void RunProcedure(string procedureName)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand(procedureName, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n--- Results ---");

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader[i] + "  ");
                }
                Console.WriteLine();
            }

            reader.Close();
        }
    }
}
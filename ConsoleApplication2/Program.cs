using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        private static readonly string _user = @"Data Source=localhost;Initial Catalog = Buying Guide;user=admin;password=password;";
        static void Main(string[] args)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_user))
            {
                sqlConnection.Open();
                string query = "SELECT * FROM SHOP";
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.Add("", 5);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    foreach (var VARIABLE in reader)
                    {
                        Console.Write(VARIABLE);
                    }
                    Console.WriteLine();
                }
                Console.ReadKey();
            }
        }
    }
}

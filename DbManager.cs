using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CurrencyService
{
    public class DbManager
    {
        private readonly string connectionString = "Server=localhost;Database=DataManager_Tests;Trusted_Connection=True;";
        public async Task InsertAsync(List<CurrencyContent> currencies)
        {
            string insertQuery = "INSERT INTO Currency (Code, FullName, Rate, Date) VALUES (@Code, @FullName, @Rate, @Date)";
            using SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
                await connection.OpenAsync();
                foreach (var currency in currencies)
                {
                    
                    await connection.ExecuteAsync(insertQuery, currency);
                }


            }
            catch (Exception ex)
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.CloseAsync();
                }
                connection.OpenAsync();
                
                
                
            }

        }
        
        
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace UserService.Services
{
    public interface IUserService
    {
        Task<IEnumerable<string>> GetUser(string id);
    }

    public class UserService:IUserService
    {
        private readonly string _connectionString;
        private readonly ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration["UserDatabase"];
            _logger.LogInformation("Setting User Database CS:"+_connectionString);
        }

        public async Task<IEnumerable<string>> GetUser(string id)
        {
            List<string> allnames = new List<string>();

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT top 1 * FROM tblUser",connection);
                using(var reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        allnames.Add(reader["ID"].ToString());
                    }
                }
            }

            return allnames;
        }
    }
}
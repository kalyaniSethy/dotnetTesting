using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using Instana.Tracing.Core;
using Instana.Tracing.Sdk.Spans;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DataService.Services
{
    public interface IDataService
    {
        Task<IEnumerable<string>> GetDataForTrip(string id);
    }

    public class DataService:IDataService
    {
        private readonly string _connectionString;
        private readonly ILogger<DataService> _logger;
        public DataService(ILogger<DataService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration["TripDatabase"];
            _logger.LogInformation("Setting Database CS:"+_connectionString);
        }

        public async Task<IEnumerable<string>> GetDataForTrip(string id)
        {
            List<string> allnames = new List<string>();

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT TOP (100000) * FROM tblTrip",connection);
                using(var reader = cmd.ExecuteReader())
                {
                    using(var span = CustomSpan.Create(this, SpanType.INTERMEDIATE, "Process Data Items"))
                    {
                        int i=0;
                        while(reader.Read())
                        {
                            i++;
                            allnames.Add(reader["Id"].ToString());
                        }
                        span.SetTag("elements", i.ToString());
                    }
                }
            }

            return allnames;
        }
    }
}
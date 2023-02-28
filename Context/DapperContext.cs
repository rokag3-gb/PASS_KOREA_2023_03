using System.Data;
using Microsoft.Extensions.Configuration;

namespace WebAPI_PASS_KOREA_2023_03
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString(Secret.conn_str_public_endpoint);
        }
    }
}
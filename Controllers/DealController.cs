using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;

namespace WebAPI_PASS_KOREA_2023_03
{
    [ApiController]
    [Route("[controller]")]
    public class DealController : ControllerBase
    {
        private readonly DapperContext _context;
        private readonly ILogger<DealController> _logger;

        public DealController(ILogger<DealController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetDeal")]
        public async Task<IEnumerable<DealModel>> GetDealAsync(string DealDateStart = null, string DealDateEnd = null)
        {
            if (string.IsNullOrWhiteSpace(DealDateStart))
                DealDateStart = DateTime.Now.ToString("yyyy-MM-dd");

            if (string.IsNullOrWhiteSpace(DealDateEnd))
                DealDateEnd = DateTime.Now.ToString("yyyy-MM-dd");

            using (var conn = new SqlConnection(Secret.conn_str_public_endpoint))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("select  d.DealId");
                sb.AppendLine("        , d.DealDate");
                sb.AppendLine("        , d.Item");
                sb.AppendLine("        , d.UserId");
                sb.AppendLine("        , d.Amount");
                sb.AppendLine("        , d.Tax");
                sb.AppendLine("        , d.Total");
                sb.AppendLine("        , d.Note");
                sb.AppendLine("        , d.CreateAt");
                sb.AppendLine("from    db1.dbo.Deal d");
                sb.AppendLine("where   d.DealDate between @DealDateStart and @DealDateEnd");
                sb.AppendLine("order by d.DealDate, d.DealId;");

                var deals = await conn.QueryAsync<DealModel>(sb.ToString(), new { DealDateStart = DealDateStart, DealDateEnd = DealDateEnd });

                return deals;
            }
        }
    }
}
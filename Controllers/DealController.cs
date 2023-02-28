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
            try
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

                using (var conn = new SqlConnection(Secret.conn_str_public_endpoint))
                {
                    var deals = await conn.QueryAsync<DealModel>(sb.ToString(), new { DealDateStart = DealDateStart, DealDateEnd = DealDateEnd });

                    return deals;
                }
            }
            catch (Exception ex)
            {
                //return StatusCode(500, ex.Message);
                throw ex;
            }
        }

        // POST action
        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[Consumes(MediaTypeNames.Application.Json)]
        public ActionResult<DealModel> Create(DealModel deal)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("insert into db1.dbo.Deal (DealDate, Item, UserId, Amount, Tax, Note)");
                sb.AppendLine("values (@DealDate, @Item, @UserId, @Amount, @Tax, @Note);");

                var param = new DynamicParameters();
                param.Add("DealDate", deal.DealDate);
                param.Add("Item", deal.Item.Trim());
                param.Add("UserId", deal.UserId?.ToString());
                param.Add("Amount", deal.Amount);
                param.Add("Tax", deal.Tax);
                param.Add("Note", deal.Note);

                using (var conn = new SqlConnection(Secret.conn_str_public_endpoint))
                {
                    conn.Execute(sb.ToString(), param);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
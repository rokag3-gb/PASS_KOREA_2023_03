namespace WebAPI
{
    [ApiController]
    [Route("[controller]")]
    public class Local_endpointController : ControllerBase
    {
        private readonly DapperContext _context;
        private readonly ILogger<Local_endpointController> _logger;

        public Local_endpointController(
            DapperContext context
            , ILogger<Local_endpointController> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        [Route("/Deal/sqlmi_local_primary/")]
        [HttpGet]
        public async Task<IEnumerable<DealModel>> GetDeal_sqlmi_local_primary(
            string DealDateStart = null
            , string DealDateEnd = null)
        {
            string conn_str = Secret.conn_str_sqlmi_local_primary;
            //Console.WriteLine($"conn.ConnectionString = {conn_str.Split(';')?.GetValue(0)?.ToString()}");

            try
            {
                using (IDbConnection conn = _context.CreateConnection(conn_str))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();

                    var deals = await conn.QueryAsync<DealModel>(Query.str_query
                        , new { DealDateStart = DealDateStart, DealDateEnd = DealDateEnd }
                        );

                    if (conn.State == ConnectionState.Open) conn.Close();

                    return deals;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("/Deal/sqlmi_local_secondary/")]
        [HttpGet]
        public async Task<IEnumerable<DealModel>> GetDeal_sqlmi_local_secondary(
            string DealDateStart = null
            , string DealDateEnd = null)
        {
            string conn_str = Secret.conn_str_sqlmi_local_secondary;
            //Console.WriteLine($"conn.ConnectionString = {conn_str.Split(';')?.GetValue(0)?.ToString()}");

            try
            {
                using (IDbConnection conn = _context.CreateConnection(conn_str))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();

                    var deals = await conn.QueryAsync<DealModel>(Query.str_query
                        , new { DealDateStart = DealDateStart, DealDateEnd = DealDateEnd }
                        );

                    if (conn.State == ConnectionState.Open) conn.Close();

                    return deals;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("/Deal/sqlmi_local_primary/")]
        [HttpPost]
        public async Task<ActionResult> Insert_sqlmi_local_primary(DealModel deal)
        {
            string conn_str = Secret.conn_str_sqlmi_local_primary;
            //Console.WriteLine($"conn.ConnectionString = {conn_str.Split(';')?.GetValue(0)?.ToString()}");

            try
            {
                var param = new DynamicParameters();
                param.Add("DealDate", deal.DealDate);
                param.Add("Item", deal.Item.Trim());
                param.Add("UserId", deal.UserId?.ToString());
                param.Add("Amount", deal.Amount);
                param.Add("Tax", deal.Tax);
                param.Add("Note", deal.Note);

                int effected_row = 0;

                using (IDbConnection conn = _context.CreateConnection(conn_str))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();

                    effected_row = conn.Execute(Query.str_command, param);

                    if (conn.State == ConnectionState.Open) conn.Close();
                }

                return StatusCode(200, $"effected_row = {effected_row}");
                //return NoContent(); // 204
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("/Deal/sqlmi_local_secondary/")]
        [HttpPost]
        public async Task<ActionResult> Insert_sqlmi_local_secondary(DealModel deal)
        {
            string conn_str = Secret.conn_str_sqlmi_local_secondary;
            //Console.WriteLine($"conn.ConnectionString = {conn_str.Split(';')?.GetValue(0)?.ToString()}");

            try
            {
                var param = new DynamicParameters();
                param.Add("DealDate", deal.DealDate);
                param.Add("Item", deal.Item.Trim());
                param.Add("UserId", deal.UserId?.ToString());
                param.Add("Amount", deal.Amount);
                param.Add("Tax", deal.Tax);
                param.Add("Note", deal.Note);

                int effected_row = 0;

                using (IDbConnection conn = _context.CreateConnection(conn_str))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();

                    effected_row = conn.Execute(Query.str_command, param);

                    if (conn.State == ConnectionState.Open) conn.Close();
                }

                return StatusCode(200, $"effected_row = {effected_row}");
                //return NoContent(); // 204
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
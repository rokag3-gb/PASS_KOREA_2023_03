namespace WebAPI
{
    [ApiController]
    [Route("[controller]")]
    public class Public_endpointController : ControllerBase
    {
        private readonly DapperContext _context;
        private readonly ILogger<Public_endpointController> _logger;

        public Public_endpointController(
            DapperContext context
            , ILogger<Public_endpointController> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        [Route("/Deal/sqlmi_public_primary/")]
        [HttpGet]
        public async Task<IEnumerable<DealModel>> GetDeal_sqlmi_public_primary(
            string DealDateStart = null
            , string DealDateEnd = null)
        {
            string conn_str = Secret.conn_str_sqlmi_public_primary;
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

        [Route("/Deal/sqlmi_public_secondary/")]
        [HttpGet]
        public async Task<IEnumerable<DealModel>> GetDeal_sqlmi_public_secondary(
            string DealDateStart = null
            , string DealDateEnd = null)
        {
            string conn_str = Secret.conn_str_sqlmi_public_secondary;
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

        [Route("/Deal/sqldb_public_primary/")]
        [HttpGet]
        public async Task<IEnumerable<DealModel>> GetDeal_sqldb_public_primary(
            string DealDateStart = null
            , string DealDateEnd = null)
        {
            string conn_str = Secret.conn_str_sqldb_public_primary;
            //Console.WriteLine($"conn.ConnectionString = {conn_str.Split(';')?.GetValue(0)?.ToString()}");

            try
            {
                using (IDbConnection conn = _context.CreateConnection(conn_str))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();

                    var deals = await conn.QueryAsync<DealModel>(Query.str_query.Replace("db1.dbo.Deal", "dbo.Deal")
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

        [Route("/Deal/sqldb_public_secondary/")]
        [HttpGet]
        public async Task<IEnumerable<DealModel>> GetDeal_sqldb_public_secondary(
            string DealDateStart = null
            , string DealDateEnd = null)
        {
            string conn_str = Secret.conn_str_sqldb_public_secondary;
            //Console.WriteLine($"conn.ConnectionString = {conn_str.Split(';')?.GetValue(0)?.ToString()}");

            try
            {
                using (IDbConnection conn = _context.CreateConnection(conn_str))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();

                    var deals = await conn.QueryAsync<DealModel>(Query.str_query.Replace("db1.dbo.Deal", "dbo.Deal")
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

        [Route("/Deal/sqlmi_public_primary/")]
        [HttpPost]
        public async Task<ActionResult> Insert_sqlmi_public_primary(DealModel deal)
        {
            string conn_str = Secret.conn_str_sqlmi_public_primary;
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

        [Route("/Deal/sqlmi_public_secondary/")]
        [HttpPost]
        public async Task<ActionResult> Insert_sqlmi_public_secondary(DealModel deal)
        {
            string conn_str = Secret.conn_str_sqlmi_public_secondary;
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

        [Route("/Deal/sqldb_public_primary/")]
        [HttpPost]
        public async Task<ActionResult> Insert_sqldb_public_primary(DealModel deal)
        {
            string conn_str = Secret.conn_str_sqldb_public_primary;
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

        [Route("/Deal/sqldb_public_secondary/")]
        [HttpPost]
        public async Task<ActionResult> Insert_sqldb_public_secondary(DealModel deal)
        {
            string conn_str = Secret.conn_str_sqldb_public_secondary;
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
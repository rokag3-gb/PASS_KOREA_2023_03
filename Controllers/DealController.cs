namespace WebAPI
{
    [ApiController]
    [Route("[controller]")]
    public class DealController : ControllerBase
    {
        private readonly DapperContext _context;
        private readonly ILogger<DealController> _logger;

        private StringBuilder sb_query = new StringBuilder();
        private StringBuilder sb_command = new StringBuilder();

        public DealController(
            DapperContext context
            , ILogger<DealController> logger)
        {
            _context = context;
            _logger = logger;

            // Select query
            sb_query.AppendLine("select  d.DealId");
            sb_query.AppendLine("        , d.DealDate");
            sb_query.AppendLine("        , d.Item");
            sb_query.AppendLine("        , d.UserId");
            sb_query.AppendLine("        , d.Amount");
            sb_query.AppendLine("        , d.Tax");
            sb_query.AppendLine("        , d.Total");
            sb_query.AppendLine("        , d.Note");
            sb_query.AppendLine("        , d.CreateAt");
            sb_query.AppendLine("from    db1.dbo.Deal d");
            sb_query.AppendLine("where   d.DealDate between @DealDateStart and @DealDateEnd");
            sb_query.AppendLine("order by d.DealDate, d.DealId;");

            // Insert command
            sb_command.AppendLine("insert into db1.dbo.Deal (DealDate, Item, UserId, Amount, Tax, Note)");
            sb_command.AppendLine("values (@DealDate, @Item, @UserId, @Amount, @Tax, @Note);");
        }
        
        [Route("/Deal/public_endpoint/")]
        [HttpGet]
        public async Task<IEnumerable<DealModel>> GetDeal_public_endpoint(
            string DealDateStart = null
            , string DealDateEnd = null)
        {
            string conn_str = Secret.conn_str_public_endpoint;

            Console.WriteLine($"conn.ConnectionString = {conn_str.Split(';')?.GetValue(0)?.ToString()}");

            try
            {
                using (IDbConnection conn = _context.CreateConnection(conn_str))
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    var deals = await conn.QueryAsync<DealModel>(sb_query.ToString()
                        , new { DealDateStart = DealDateStart, DealDateEnd = DealDateEnd }
                        );

                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                    return deals;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("/Deal/local_endpoint/")]
        [HttpGet]
        public async Task<IEnumerable<DealModel>> GetDeal_local_endpoint(
            string DealDateStart = null
            , string DealDateEnd = null)
        {
            string conn_str = Secret.conn_str_local_endpoint;

            Console.WriteLine($"conn.ConnectionString = {conn_str.Split(';')?.GetValue(0)?.ToString()}");

            try
            {
                using (IDbConnection conn = _context.CreateConnection(conn_str))
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    var deals = await conn.QueryAsync<DealModel>(sb_query.ToString()
                        , new { DealDateStart = DealDateStart, DealDateEnd = DealDateEnd }
                        );

                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                    return deals;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("/Deal/readwrite_listener/")]
        [HttpGet]
        public async Task<IEnumerable<DealModel>> GetDeal_readwrite_listener(
            string DealDateStart = null
            , string DealDateEnd = null)
        {
            string conn_str = Secret.conn_str_readwrite_listener;

            Console.WriteLine($"conn.ConnectionString = {conn_str.Split(';')?.GetValue(0)?.ToString()}");

            try
            {
                using (IDbConnection conn = _context.CreateConnection(conn_str))
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    var deals = await conn.QueryAsync<DealModel>(sb_query.ToString()
                        , new { DealDateStart = DealDateStart, DealDateEnd = DealDateEnd }
                        );

                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                    return deals;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("/Deal/readonly_listener/")]
        [HttpGet]
        public async Task<IEnumerable<DealModel>> GetDeal_readonly_listener(
            string DealDateStart = null
            , string DealDateEnd = null)
        {
            string conn_str = Secret.conn_str_readonly_listener;

            Console.WriteLine($"conn.ConnectionString = {conn_str.Split(';')?.GetValue(0)?.ToString()}");

            try
            {
                using (IDbConnection conn = _context.CreateConnection(conn_str))
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    var deals = await conn.QueryAsync<DealModel>(sb_query.ToString()
                        , new { DealDateStart = DealDateStart, DealDateEnd = DealDateEnd }
                        );

                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                    return deals;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("/Deal/public_endpoint/")]
        [HttpPost]
        public async Task<ActionResult> Insert_public_endpoint(DealModel deal)
        {
            string conn_str = Secret.conn_str_public_endpoint;

            Console.WriteLine($"conn.ConnectionString = {conn_str.Split(';')?.GetValue(0)?.ToString()}");

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
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    effected_row = conn.Execute(sb_command.ToString(), param);

                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }

                return StatusCode(200, $"effected_row = {effected_row}");
                return NoContent(); // 204
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("/Deal/local_endpoint/")]
        [HttpPost]
        public async Task<ActionResult> Insert_local_endpoint(DealModel deal)
        {
            string conn_str = Secret.conn_str_local_endpoint;

            Console.WriteLine($"conn.ConnectionString = {conn_str.Split(';')?.GetValue(0)?.ToString()}");

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
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    effected_row = conn.Execute(sb_command.ToString(), param);

                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }

                return StatusCode(200, $"effected_row = {effected_row}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("/Deal/readwrite_listener/")]
        [HttpPost]
        public ActionResult<DealModel> Insert_readwrite_listener(DealModel deal)
        {
            string conn_str = Secret.conn_str_readwrite_listener;

            Console.WriteLine($"conn.ConnectionString = {conn_str.Split(';')?.GetValue(0)?.ToString()}");

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
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    effected_row = conn.Execute(sb_command.ToString(), param);

                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }

                return StatusCode(200, $"effected_row = {effected_row}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("/Deal/readonly_listener/")]
        [HttpPost]
        public ActionResult<DealModel> Insert_readonly_listener(DealModel deal)
        {
            string conn_str = Secret.conn_str_readonly_listener;

            Console.WriteLine($"conn.ConnectionString = {conn_str.Split(';')?.GetValue(0)?.ToString()}");

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
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    effected_row = conn.Execute(sb_command.ToString(), param);

                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }

                return StatusCode(200, $"effected_row = {effected_row}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
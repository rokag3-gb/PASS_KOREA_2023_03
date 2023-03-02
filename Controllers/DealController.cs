namespace WebAPI
{
    [ApiController]
    [Route("[controller]")]
    public class DealController : ControllerBase
    {
        private readonly ILogger<DealController> _logger;

        private StringBuilder sb_query = new StringBuilder();
        private StringBuilder sb_command = new StringBuilder();

        public DealController(
            ILogger<DealController> logger)
        {
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
        //[HttpGet(Name = "GetDeal_PublicEndpoint")]
        [HttpGet]
        public async Task<IEnumerable<DealModel>> GetDeal_public_endpoint(
            string DealDateStart = null
            , string DealDateEnd = null)
        {
            try
            {
                using (var conn = new SqlConnection(Secret.conn_str_public_endpoint))
                {
                    var deals = await conn.QueryAsync<DealModel>(sb_query.ToString()
                        , new { DealDateStart = DealDateStart, DealDateEnd = DealDateEnd }
                        );

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
            try
            {
                using (var conn = new SqlConnection(Secret.conn_str_local_endpoint))
                {
                    var deals = await conn.QueryAsync<DealModel>(sb_query.ToString()
                        , new { DealDateStart = DealDateStart, DealDateEnd = DealDateEnd }
                        );

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
            try
            {
                using (var conn = new SqlConnection(Secret.conn_str_readwrite_listener))
                {
                    var deals = await conn.QueryAsync<DealModel>(sb_query.ToString()
                        , new { DealDateStart = DealDateStart, DealDateEnd = DealDateEnd }
                        );

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
            try
            {
                using (var conn = new SqlConnection(Secret.conn_str_readonly_listener))
                {
                    var deals = await conn.QueryAsync<DealModel>(sb_query.ToString()
                        , new { DealDateStart = DealDateStart, DealDateEnd = DealDateEnd }
                        );

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
        public ActionResult<DealModel> Insert_public_endpoint(DealModel deal)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("DealDate", deal.DealDate);
                param.Add("Item", deal.Item.Trim());
                param.Add("UserId", deal.UserId?.ToString());
                param.Add("Amount", deal.Amount);
                param.Add("Tax", deal.Tax);
                param.Add("Note", deal.Note);

                using (var conn = new SqlConnection(Secret.conn_str_public_endpoint))
                {
                    conn.Execute(sb_command.ToString(), param);
                }

                return NoContent(); // 204
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("/Deal/local_endpoint/")]
        [HttpPost]
        public ActionResult<DealModel> Insert_local_endpoint(DealModel deal)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("DealDate", deal.DealDate);
                param.Add("Item", deal.Item.Trim());
                param.Add("UserId", deal.UserId?.ToString());
                param.Add("Amount", deal.Amount);
                param.Add("Tax", deal.Tax);
                param.Add("Note", deal.Note);

                using (var conn = new SqlConnection(Secret.conn_str_local_endpoint))
                {
                    conn.Execute(sb_command.ToString(), param);
                }

                return NoContent(); // 204
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
            try
            {
                var param = new DynamicParameters();
                param.Add("DealDate", deal.DealDate);
                param.Add("Item", deal.Item.Trim());
                param.Add("UserId", deal.UserId?.ToString());
                param.Add("Amount", deal.Amount);
                param.Add("Tax", deal.Tax);
                param.Add("Note", deal.Note);

                using (var conn = new SqlConnection(Secret.conn_str_readwrite_listener))
                {
                    conn.Execute(sb_command.ToString(), param);
                }

                return NoContent(); // 204
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
            try
            {
                var param = new DynamicParameters();
                param.Add("DealDate", deal.DealDate);
                param.Add("Item", deal.Item.Trim());
                param.Add("UserId", deal.UserId?.ToString());
                param.Add("Amount", deal.Amount);
                param.Add("Tax", deal.Tax);
                param.Add("Note", deal.Note);

                using (var conn = new SqlConnection(Secret.conn_str_readonly_listener))
                {
                    conn.Execute(sb_command.ToString(), param);
                }

                return NoContent(); // 204
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
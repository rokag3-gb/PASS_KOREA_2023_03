namespace WebAPI_PASS_KOREA_2023_03
{
    public class DealModel
    {
        public Int64 DealId { get; set; }
        public DateTime DealDate { get; set; }
        public string Item { get; set; }
        public object UserId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Tax { get; set; }
        public decimal? Total => Amount + Tax;
        //public decimal? Total { get; set; }
        public string? Note { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
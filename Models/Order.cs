namespace Media_MS.Models
{
    public class Order
    {
        public long OrderId { get ; set; }
        public string OrderDescription { get; set; }=string.Empty;
        public string OrderStatus { get; set; } = string.Empty;
        public string OrderDate { get; set; }
        public decimal Price { get; set; } 



    }
}

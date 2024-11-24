namespace ConnectingAPIs.DTO
{
    public class UpdateProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool InStock { get; set; }
        public int Quantity {  get; set; }
    }
}

namespace ConnectingAPIs.DTO
{
    public class ResponseDetail
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public Result Result { get; set; }
        public int ResultCode { get; set; }
    }
    public class Result
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool InStock { get; set; }
        public int Quantity { get; set; }
    }
}

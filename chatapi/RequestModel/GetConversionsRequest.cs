namespace chatapi.RequestModel
{
    public class GetConversionsRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }
}

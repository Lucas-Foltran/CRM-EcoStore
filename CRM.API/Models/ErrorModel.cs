namespace CRM.API.Models
{
    public class ErrorModel
    {
        public string status { get; set; }
        public string statusText { get; set; }
        public string url { get; set; }
        public string ok { get; set; }
        public string name { get; set; }
        public string message { get; set; }
        public string error { get; set; }
    }
}

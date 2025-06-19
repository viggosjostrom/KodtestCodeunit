namespace KodtestCodeunit.Models
{
    public class GetDataResponse
    {
        public string ReferenceId { get; set; } = string.Empty;
        public List<GetDataModel> Products { get; set; } = new List<GetDataModel>();
    }
}
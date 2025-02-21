namespace ScimplyUI.UI.DTOs.Chart
{
    public class AdminUserChartsQueryResponse
    {
        public List<ChartLocationCountDTO> ChartLocationCountResponseDTO { get; set; }
        public List<ChartDateCountDTO> ChartMostCommonDateResponseDTO { get; set; }
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int InactiveUsers { get; set; }
	}

    public class ChartLocationCountDTO
    {
        public string Location { get; set; }
        public int Count { get; set; }
    }

    public class ChartDateCountDTO
    {
        public string Date { get; set; }
        public int Count { get; set; }
    }

}

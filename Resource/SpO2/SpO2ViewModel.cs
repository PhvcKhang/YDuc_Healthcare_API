namespace HealthCareApplication.Resource.SpO2
{
    public class SpO2ViewModel
    {
        public int Value { get; set; }
        public string? ImageLink { get; set; } = null;
        public DateTime Timestamp { get; set; }
    }
}

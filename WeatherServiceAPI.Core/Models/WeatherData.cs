namespace WeatherServiceAPI.Core.Models
{
    public class WeatherData : Entity
    {
        public int Id { get; set; }
        public int GeolocationDataId { get; set; }
        public GeolocationData GeolocationData { get; set; }
        public DateTime Timestamp { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
    }
}

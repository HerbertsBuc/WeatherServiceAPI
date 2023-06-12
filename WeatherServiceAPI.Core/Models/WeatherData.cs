namespace WeatherServiceAPI.Core.Models
{
    public class WeatherData : Entity
    {
        public string Location { get; set; }
        public double Temperature { get; set; }
        public string Conditions { get; set; }
        public double WindSpeed { get; set; }
        public string WindDirection { get; set; }
        public int Humidity { get; set; }
        public GeolocationData LocationData { get; set; }
        public int LocationDataId { get; set; }
        public IpAddressData IpAddress { get; set; }
        public int IpAddressId { get; set; }
    }
}

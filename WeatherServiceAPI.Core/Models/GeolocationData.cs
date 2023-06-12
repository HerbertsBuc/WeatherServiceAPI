namespace WeatherServiceAPI.Core.Models
{
    public class GeolocationData : Entity
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public IpAddressData IpAddress { get; set; }
        public int IpAddressId { get; set; }
    }
}

namespace WeatherServiceAPI.Core.Models
{
    public class GeolocationData : Entity
    {
        public int Id { get; set; }
        public int IPAddressDataId { get; set; }
        public IPAddressData IPAddressData { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}

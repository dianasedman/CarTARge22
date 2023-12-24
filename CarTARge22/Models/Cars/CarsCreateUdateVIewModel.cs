namespace CarTARge22.Models.Cars
{
    public class CarsCreateUdateVIewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public DateTime Year { get; set; }
        public string Transmission { get; set; }

        public string Color { get; set; }
        public string Fuel { get; set; }
        public int TopSpeed { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}

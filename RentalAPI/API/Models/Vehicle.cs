namespace API.Models
{
    public class Vehicle: ModelBase
    {
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public float Price { get; set; } = 0;
        public bool Ordered { get; set; } = false;
        public int CategoryId { get; set; }
        public VehicleCategory Category { get; set; } = new VehicleCategory();
    }
}

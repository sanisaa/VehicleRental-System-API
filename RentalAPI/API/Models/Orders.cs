namespace API.Models
{
    public class Orders : ModelBase
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public int VehicleId { get; set; }
        public string? VehicleName { get; set; }
        public DateTime OrderedOn { get; set; }
        public int Returned { get; set; }
    }
}

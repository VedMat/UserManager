namespace UserManager.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public double Latitude {  get; set; }
        public double Longitude { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
    }
}

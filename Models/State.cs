namespace UserManager.Models
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Relazione 1 a molte (uno stato può avere più città)
        public List<Location> Locations { get; set; }
    }
}

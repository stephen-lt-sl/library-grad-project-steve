namespace LibraryGradProject.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public System.DateTime BeginDate { get; set; }
        public System.DateTime EndDate { get; set; }
    }
}
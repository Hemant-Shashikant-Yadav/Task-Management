namespace Server3.Models
{
    public class Task
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string Status { get; set; }

        public required DateTime DueDate { get; set; }

        public required int UserId { get; set; }

    }
}

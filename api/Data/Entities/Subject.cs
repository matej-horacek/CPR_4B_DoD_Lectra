namespace api.Data.Entities;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<Recording> Recordings { get; set; } = [];
}

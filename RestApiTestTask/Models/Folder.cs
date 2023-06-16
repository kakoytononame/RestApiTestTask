namespace RestApiTestTask.Models;

public class Folder
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public List<Folder> folders { get; set; }
}
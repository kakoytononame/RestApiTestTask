using RestApiTestTask.Models;

namespace RestApiTestTask.Services;

public interface IUserService
{
    public Folder Get(string parrentName);

    public List<Folder> GetAll();

    public bool Add(string parrentName, string name);

    public bool Update(Folder thisfolder);

    public bool Delete(string Name);

    public bool RemoveFolderByName(Folder folder, string folderName);
}
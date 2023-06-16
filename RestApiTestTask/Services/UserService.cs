using RestApiTestTask.Models;

namespace RestApiTestTask.Services;

public class UserService:IUserService
{
    public List<Folder> Folders;
    private readonly FindObject _findObject;

    public UserService(FindObject findObject)
    {
        _findObject = findObject;
        Folders = new List<Folder>
        {
            new()
            {
                Id = Guid.Parse("52f163d0-e358-4e11-9f82-dcce1ca464ff"),
                Name = "Folder1",
                folders = new List<Folder>
                {
                    new()
                    {
                        Id = Guid.Parse("b6e5b9a3-71c4-4e63-97f6-9daf6ffb7a2f"),
                        Name = "Folder1.1",
                        folders = new List<Folder>
                        {
                            new()
                            {
                                Id = Guid.Parse("52f163d0-e358-4e11-9f82-dcce1ca464ff"),
                                Name = "Folder1.1.1",
                            }
                        }
                    }
                    
                }
            },
            new()
            {
                Id = Guid.Parse("c6bbce90-8826-4585-b3e9-c0792b4ecfc9"),
                Name = "Folder2",
                folders = new List<Folder>()
                {
                    new()
                    {
                        Id = Guid.Parse("516b86b3-6dd8-4ea2-b717-3b9848264e53"),
                        Name = "Folder2.1"
                    }
                    
                }
            }
            
        };
    }

    public Folder Get(string parrentName)
    {
        var parrentFolder = new List<Folder>();
        foreach (var folder in Folders)
        {
            parrentFolder= _findObject.FindPersonsByName(folder,parrentName);
            if (parrentFolder is not null)
            {
                return parrentFolder[0];
            }
        }

        if (parrentFolder is null)
        {
            throw new Exception("Такой папки не найдено");
        }

      return parrentFolder[0];
    }

    public List<Folder> GetAll()
    {
        return Folders;
    }


    public bool Add(string parrentName,string name)
    {
        if (name =="")
        {
            throw new Exception("Не заполнены данные");
        }
        
        try
        {

            var parrentFolder =
                new Folder
                {
                    Id = Guid.NewGuid(),
                    Name = name
                };
                foreach (var folder in Folders)
                {
                    AddFolder(folder,parrentName,parrentFolder);
                }
                   
                return true;
                
        }
        catch
        {
            throw new Exception("Папка не добавлена");
        }
    }

    public bool Update(Folder thisfolder)
    {
        if (thisfolder is null)
        {
            throw new Exception("Не заполнены данные ");
        }

        try
        {
            
            foreach (var folder in Folders)
            {
                UpdateFolder(folder, thisfolder.Name, thisfolder);
            }
            
            return true;
        }
        catch
        {
            throw new Exception("Папка не обновлена");
        }
    }

    public bool Delete(string Name)
    {
        var parrentFolder = new List<Folder>();
        foreach (var folder in Folders)
        {
            parrentFolder= _findObject.FindPersonsByName(folder,Name);
        }
        if (parrentFolder is null)
        {
            throw new Exception("Такой папки не найдено");
        }

        try
        {
            foreach (var folder in Folders)
            {
                RemoveFolderByName(folder,Name);
            }
            
            return true;
        }
        catch
        {
            throw new Exception("Папка не удалена");
        }
    }
    public  bool RemoveFolderByName(Folder folder, string folderName)
    {
        if (folder.folders != null)
        {
            for (int i = folder.folders.Count - 1; i >= 0; i--)
            {
                if (folder.folders[i].Name == folderName)
                {
                    folder.folders=null;
                    return true;
                }

                if (RemoveFolderByName(folder.folders[i], folderName))
                {
                    return true;
                }
            }
        }

        return false;
    }
    
    public  bool AddFolder(Folder folder, string folderName,Folder newfolder)
    {
        if (folder.Name == folderName)
        {
            if (folder.folders is null)
            {
                folder.folders = new List<Folder>();
            }
            folder.folders.Add(newfolder);
        }
        if (folder.folders != null)
        {
            for (int i = folder.folders.Count - 1; i >= 0; i--)
            {
                if (folder.folders[i].Name == folderName)
                {
                    if (folder.folders[i].folders is null)
                    {
                        folder.folders[i].folders = new List<Folder>();
                    }
                    folder.folders[i].folders.Add(newfolder);
                    return true;
                }

                if (AddFolder(folder.folders[i], folderName,newfolder))
                {
                    return true;
                }
            }
        }

        return false;
    }
    
    public  bool UpdateFolder(Folder folder, string folderName,Folder newfolder)
    {
        if (folder.folders != null)
        {
            for (int i = folder.folders.Count - 1; i >= 0; i--)
            {
                if (folder.folders[i].Name == folderName)
                {
                    folder.folders[i].Id = newfolder.Id;
                    folder.folders[i].Name = newfolder.Name;
                    folder.folders[i].folders = newfolder.folders;
                    return true;
                }

                if (UpdateFolder(folder.folders[i], folderName,newfolder))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
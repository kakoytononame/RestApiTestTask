using RestApiTestTask.Models;

namespace RestApiTestTask.Services;

public class FindObject
{
    
    public List<Folder> FindPersonsByName(Folder person, string name)
    {
            List<Folder> foundPersons = new List<Folder>();

            // Проверяем текущий объект
            if (person.Name == name)
            {
                foundPersons.Add(person);
            }

            // Рекурсивно проверяем детей
            if (person.folders != null)
            {
                foreach (var child in person.folders)
                {
                    List<Folder> foundChildren = FindPersonsByName(child, name);
                    foundPersons.AddRange(foundChildren);
                }
            }

            return foundPersons;
    }
    
    
    
}
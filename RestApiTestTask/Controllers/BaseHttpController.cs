using Microsoft.AspNetCore.Mvc;
using RestApiTestTask.DTOS;
using RestApiTestTask.Models;
using RestApiTestTask.Services;

namespace RestApiTestTask.Controllers;

[Route("User/")] 
public class BaseHttpController:Controller
{
    
    private readonly IUserService _userService;

    public BaseHttpController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Create")]
    public bool Create([FromBody]CreateFolderDTO newfolder)
    {
        return _userService.Add(newfolder.ParrentName,newfolder.NewName);
    }
    
    [HttpGet("Get")]
    public Folder Get(string ParrentName)
    {
        return _userService.Get(ParrentName);
    }
    
    [HttpGet("GetAll")]
    public List<Folder> GetAll()
    {
      return _userService.GetAll();
    }
    
    [HttpPut("Update")]
    public bool Update([FromBody]Folder user)
    {
        return _userService.Update(user);
    }

    [HttpDelete("Delete")]
    public bool Delete(string Name)
    {
        return _userService.Delete(Name);
    }
}
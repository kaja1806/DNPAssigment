using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Models;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]

public class PostsController : ControllerBase //changes
{

    private readonly IPostLogic postLogic; //

    public PostsController(IPostLogic postLogic) //
    {
        this.postLogic = postLogic; //
    }

    [HttpPost]
    public async Task<ActionResult<Post>> CreateAsync(PostCreationDto dto) //
    {
        try
        {
            Post created = await postLogic.CreateAsync(dto); //
            return Created($"/posts/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}
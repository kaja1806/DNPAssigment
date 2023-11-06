using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Models;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]

public class PostsController : ControllerBase
{

    private readonly IPostLogic postLogic; 

    public PostsController(IPostLogic postLogic) 
    {
        this.postLogic = postLogic; 
    }

    [HttpPost]
    public async Task<ActionResult<Post>> CreateAsync(PostCreationDto dto)
    {
        try
        {
            Post created = await postLogic.CreateAsync(dto); 
            return Created($"/posts/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]  
    public async Task<ActionResult<IEnumerable<Post>>> GetAsync([FromQuery] string? title)
    {
        try
        {
            ViewAllPostsDto allposts = new(title);
            IEnumerable<Post> posts = await postLogic.GetAsync(allposts);
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ViewAPostDto>> GetById([FromRoute] int id)
    {
        try
        {
           ViewAPostDto result = await postLogic.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}
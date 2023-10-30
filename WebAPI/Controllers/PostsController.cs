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
            Post created = await postLogic.CreateAsync(dto); // Id for each post? for now; but will link to user upon login soon
            return Created($"/posts/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]  //complete these here what each view a post will get
    public async Task<ActionResult<IEnumerable<Post>>> GetAsync([FromQuery] string? title)
    //, [FromQuery] string? postbody)
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
    
    //anything here for 1 view post?
    
    
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
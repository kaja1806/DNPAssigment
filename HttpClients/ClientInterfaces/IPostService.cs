using Shared.DTOs;
using Shared.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
   // Task CreateAsync(PostCreationDto dto);
    Task<Post>CreateAsync(PostCreationDto dto);
    //or create?
}
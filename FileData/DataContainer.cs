using Shared.Models;

namespace FileData;

public class DataContainer
{
    public ICollection<UserModel> Users { get; set; }
    public ICollection<PostModel> Posts { get; set; }
}
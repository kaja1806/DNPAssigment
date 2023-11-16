using System.Text.Json;
using Shared.Models;

namespace FileData;

public class FileContext
{
    private const string filePath = "data.json";
    private DataContainer? dataContainer;  
    
    public ICollection<PostModel> Posts
    {
        get
        {      
            LoadData();  
            return dataContainer!.Posts;  
        }
    }
    
    
    public ICollection<UserModel> Users
    {
        get
        {
            LoadData();
            return dataContainer!.Users;
        }
    }

    private void LoadData() // acts as LazyLoadData and LoadData from Git
    {
        if (dataContainer != null) return;

        if (!File.Exists(filePath))
        {
            dataContainer = new()
            {
                Posts = new List<PostModel>(),
                Users = new List<UserModel>()
            };
            return;
        }

        string content = File.ReadAllText(filePath);
        dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }
  
    
    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(dataContainer, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(filePath, serialized);
        dataContainer = null;
    }

}
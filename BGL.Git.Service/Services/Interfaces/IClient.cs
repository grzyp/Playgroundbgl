using BGL.Git.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BGL.Git.Service.Services.Interfaces
{
    public interface IClient
    {
        Task<GitUser> GetUserRequest(string path);
        Task<List<Repository>> GetRepositoriesRequest(string path);
    }
}

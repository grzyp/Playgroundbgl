using BGL.Git.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BGL.Git.Service.Services.Interfaces
{
    public interface IGitService
    {
        Task<UserData> GetUserData(string username);
        Task<GitUser> GetGitUser(string username);
        Task<List<Repository>> GetRepositoriesData(string username);
    }
}

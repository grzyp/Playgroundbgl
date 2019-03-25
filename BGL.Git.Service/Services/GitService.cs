using BGL.Git.Service.Models;
using BGL.Git.Service.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BGL.Git.Service.Services
{
    public class GitService : IGitService
    {
        private readonly IClient _client;

        public GitService(IClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Get data (Github user and repositories) sorted by top 5 with biggest stargazers count
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<UserData> GetUserData(string username)
        {
            var user = await GetGitUser(username);
            var topRepositories = await GetRepositoriesData(username);
            topRepositories = topRepositories?.OrderByDescending(x => x.StargazersCount).Take(5).ToList();
            return new UserData()
            {
                User = user,
                Repositories = topRepositories
            };
        }

        /// <summary>
        /// Make a Get request and read content into GitUserClass
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<GitUser> GetGitUser(string username)
        {
            return await _client.GetUserRequest(string.Format(@"users/{0}", username));

        }

        /// <summary>
        /// Make a Get request and read content into List of Repository objects
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<List<Repository>> GetRepositoriesData(string username)
        {
            return await _client.GetRepositoriesRequest(string.Format(@"users/{0}/repos", username));
        }

    }
}

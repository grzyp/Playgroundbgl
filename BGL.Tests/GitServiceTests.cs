using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BGL.Git.Service.Services.Interfaces;
using BGL.Git.Service.Services;
using BGL.Web.Controllers;
using BGL.Git.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BGL.Tests
{
    [TestClass]
    public class GitServiceTests
    {
        GitUser gitUserGrzyp;
        List<Repository> reposGrzyp;
        GitUser gitUserDotNet;
        List<Repository> reposDotNet;
        GitUser gitUserNonExist;
        List<Repository> reposNonExist;
        Mock<IClient> mockClient;
        GitService gitService;
        UserData grzypUserData;
        UserData dotnetUserData;
        UserData nonexistUserData;

        [TestInitialize]
        public void TestInit()
        {
            gitUserGrzyp = new GitUser()
            {
                Avatar = "https://avatars1.githubusercontent.com/u/7986386?v=4",
                Location = String.Empty,
                ReposUrl = "https://api.github.com/users/grzyp/repos",
                Username = "grzyp"
            };
            reposGrzyp = new List<Repository>()
            {
                new Repository()
                {
                    Name = "grzyp/alexa-ve-skill",
                    StargazersCount = 0
                },
            };

            gitUserDotNet = new GitUser()
            {
                Avatar = "https://avatars0.githubusercontent.com/u/9141961?v=4",
                Location = String.Empty,
                ReposUrl = "https://api.github.com/users/dotnet/repos",
                Username = "dotnet"
            };
            reposDotNet = new List<Repository>()
            {
                new Repository()
                {
                    Name = "dotnet/apireviews",
                    StargazersCount = 125
                },

                new Repository()
                {
                    Name = "dotnet/announcements",
                    StargazersCount = 610
                },

                new Repository()
                {
                    Name = "dotnet/cli",
                    StargazersCount = 3162
                },

                new Repository()
                {
                    Name = "dotnet/BenchmarkDotNet",
                    StargazersCount = 3813
                },

                new Repository()
                {
                    Name = "dotnet/core",
                    StargazersCount = 10232
                },

                new Repository()
                {
                    Name = "dotnet/coreclr ",
                    StargazersCount = 11175
                },

                new Repository()
                {
                    Name = "dotnet/corefx ",
                    StargazersCount = 16055
                },
            };

            gitUserNonExist = null;
            reposNonExist = null;

            mockClient = new Mock<IClient>();
            mockClient.Setup(x => x.GetUserRequest("users/grzyp")).Returns(Task.FromResult(gitUserGrzyp));
            mockClient.Setup(x => x.GetRepositoriesRequest("users/grzyp/repos")).Returns(Task.FromResult(reposGrzyp));
            mockClient.Setup(x => x.GetUserRequest("users/dotnet")).Returns(Task.FromResult(gitUserDotNet));
            mockClient.Setup(x => x.GetRepositoriesRequest("users/dotnet/repos")).Returns(Task.FromResult(reposDotNet));
            mockClient.Setup(x => x.GetUserRequest("users/nonexist")).Returns(Task.FromResult(gitUserNonExist));
            mockClient.Setup(x => x.GetRepositoriesRequest("users/nonexist/repos")).Returns(Task.FromResult(reposNonExist));
            gitService = new GitService(mockClient.Object);

            grzypUserData = new UserData()
            {
                Repositories = reposGrzyp,
                User = gitUserGrzyp
            };

            dotnetUserData = new UserData()
            {
                Repositories = reposDotNet.OrderByDescending(x=>x.StargazersCount).Take(5).ToList(),
                User = gitUserDotNet
            };

            nonexistUserData = new UserData()
            {
                Repositories = null,
                User = null
            };
        }


        [TestMethod]
        public async Task GetUserExistingUser1()
        {
            var result = await gitService.GetGitUser("grzyp");

            Assert.AreEqual(result, gitUserGrzyp);
        }

        [TestMethod]
        public async Task GetUserExistingUser2()
        {
            var result = await gitService.GetGitUser("dotnet");

            Assert.AreEqual(result, gitUserDotNet);
        }

        [TestMethod]
        public async Task GetUserNonExistingUser()
        {
            var result = await gitService.GetGitUser("nonexist");

            Assert.AreEqual(result, null);
        }

        [TestMethod]
        public async Task GetReposExistingUser1()
        {
            var result = await gitService.GetRepositoriesData("grzyp");

            Assert.AreEqual(result, reposGrzyp);
        }

        [TestMethod]
        public async Task GetReposExistingUser2()
        {
            var result = await gitService.GetRepositoriesData("dotnet");

            Assert.AreEqual(result, reposDotNet);
        }

        [TestMethod]
        public async Task GetReposNonExistingUser()
        {
            var result = await gitService.GetRepositoriesData("nonexist");

            Assert.AreEqual(result, null);
        }

        [TestMethod]
        public async Task GetGetUserDataUser1()
        {
            var result = await gitService.GetUserData("grzyp");

            Assert.AreEqual(result.User, gitUserGrzyp);
            Assert.AreEqual(result.Repositories.Count, 1);
        }

        [TestMethod]
        public async Task GetGetUserDataUser2()
        {
            var result = await gitService.GetUserData("dotnet");

            Assert.AreEqual(result.User, gitUserDotNet);
            Assert.AreEqual(result.Repositories.Count, 5);
            Assert.AreEqual(result.Repositories.First().StargazersCount, 16055);
        }

        [TestMethod]
        public async Task GetGetUserDataUser3()
        {
            var result = await gitService.GetUserData("nonexist");

            Assert.AreEqual(result.User, null);
            Assert.AreEqual(result.Repositories, null);
        }


    }
}

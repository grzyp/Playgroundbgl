using BGL.Git.Service.Services.Interfaces;
using BGL.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BGL.Web.Controllers
{
    public class GitController : Controller
    {
        private readonly IGitService _gitService;

        public GitController(IGitService gitService)
        {
            _gitService = gitService;
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View(new SearchResultViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Search(SearchResultViewModel search)
        {
            if(!ModelState.IsValid)
            {
                return View(search);
            }

            var userData = await _gitService.GetUserData(search.SearchPhrase);

            if (userData != null)
            {
                if (userData.User != null)
                {
                    search.User = new GitUserViewModel()
                    {
                        Avatar = userData.User.Avatar,
                        Location = userData.User.Location,
                        Username = userData.User.Username
                    };
                }

                if (userData.Repositories != null)
                {
                    search.Repositories = userData.Repositories.Select(x => new RepositoryViewModel()
                    {
                        Name = x.Name,
                        StargazersCount = x.StargazersCount
                    }).ToList();
                }
            }

            return View(search);
        }

    }
}
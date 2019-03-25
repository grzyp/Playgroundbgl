using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BGL.Git.Service.Models
{
    public class GitUser
    {
        [JsonProperty("login")]
        public string Username { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("avatar_url")]
        public string Avatar { get; set; }
        [JsonProperty("repos_url")]
        public string ReposUrl { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BGL.Git.Service.Models
{
    public class Repository
    {
        [JsonProperty("full_name")]
        public string Name { get; set; }
        [JsonProperty("stargazers_count")]        
        public int StargazersCount { get; set; }
    }
}

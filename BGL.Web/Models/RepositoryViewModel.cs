using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BGL.Web.Models
{
    public class RepositoryViewModel
    {
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Stargazers count")]
        public int StargazersCount { get; set; }
    }
}

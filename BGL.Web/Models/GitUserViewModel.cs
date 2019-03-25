using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BGL.Web.Models
{
    public class GitUserViewModel
    {
        [DisplayName("Username")]
        public string Username { get; set; }
        [DisplayName("Location")]
        public string Location { get; set; }
        [DisplayName("Avatar")]
        public string Avatar { get; set; }
    }
}

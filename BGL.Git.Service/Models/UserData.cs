using System;
using System.Collections.Generic;
using System.Text;

namespace BGL.Git.Service.Models
{
    public class UserData
    {
        public GitUser User { get; set; }
        public List<Repository> Repositories { get; set; }
    }
}

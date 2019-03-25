using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BGL.Web.Models
{
    public class SearchViewModel
    {
        [Required]
        [DisplayName("Search phrase")]
        public string SearchPhrase { get; set; }
    }

    public class SearchResultViewModel
    {
        [Required]
        [DisplayName("Search phrase")]
        public string SearchPhrase { get; set; }
        public GitUserViewModel User { get; set; }
        public List<RepositoryViewModel> Repositories { get; set; }
    }
}
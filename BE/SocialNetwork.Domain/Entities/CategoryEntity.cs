using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Entities
{
    [Table("Categories")]

    public class CategoryEntity : BaseEntity
    {
        [Required]
        public string CategoryName { get; set; }
        public string ContentAllowed { get; set; } = "";
        public string CoverImagePath { get; set; } = "";
        [RegularExpression(@"^[a-z0-9-]*$")]
        public string Slug { set; get; } = "";

        public ICollection<PostEntity> Posts { get; set; }
    }
}

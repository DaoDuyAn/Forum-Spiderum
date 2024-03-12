using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Entities
{
    [Table("Categories")]

    public class CategoryEntity : BaseEntity
    {
        [Required]
        public string CategoryName { get; set; }
        public string ContentAllowed { get; set; } = "";

        public ICollection<PostEntity> Posts { get; set; }
    }
}

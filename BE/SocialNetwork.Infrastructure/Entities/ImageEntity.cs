using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Entities
{
    [Table("Images")]
    public class ImageEntity : BaseEntity
    {
        public string ImagePath { get; set; } = "";
        public Guid PostId{ get; set; }
        [ForeignKey("PostId")]
        public PostEntity Post { get; set; }
    }
}

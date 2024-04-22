using SocialNetwork.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.DTOs.Category
{
    public class CategoryResponseDTO
    {
        public string Id { get; set; }
        public string CategoryName { get; set; }
        public string CoverImagePath { get; set; } = "";
        [RegularExpression(@"^[a-z0-9-]*$")]
        public string Slug { set; get; } = "";
     
    }
}

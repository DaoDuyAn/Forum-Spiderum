namespace SocialNetwork.API.DTOs
{
    public class AddCategoryRequest
    {
        public string CategoryName { get; set; }
        public string ContentAllowed { get; set; }
        public IFormFile Image { get; set; }
    }
}

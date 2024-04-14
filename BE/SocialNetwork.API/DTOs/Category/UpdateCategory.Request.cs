namespace SocialNetwork.API.DTOs
{
    public class UpdateCategoryRequest
    {
        public string Id { get; set; }
        public string CategoryName { get; set; }
        public string ContentAllowed { get; set; }
        public IFormFile Image { get; set; }
    }
}

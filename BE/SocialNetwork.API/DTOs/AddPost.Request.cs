namespace SocialNetwork.API.DTOs
{
    public class AddPostRequest
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Content { get; set; } = "";

        public string UserId { get; set; } = "";
        public string CategoryId { get; set; } = "";
    }
}

namespace SocialNetwork.API.DTOs
{
    public class UpdatePostRequest
    {
        public string Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Content { get; set; } = "";

        public string UserId { get; set; } = "";
        public string CategoryId { get; set; } = "";
    }
}

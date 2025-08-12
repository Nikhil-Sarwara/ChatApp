namespace ChatApp.Application.DTOs
{
    public class UpdateUserProfileDto
    {
        public string ProfilePictureUrl { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}

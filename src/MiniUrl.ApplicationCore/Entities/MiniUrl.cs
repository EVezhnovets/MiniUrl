namespace MiniUrl.ApplicationCore
{
    public class MiniUrl
    {
        public Guid Id { get; set; }
        public string? Url { get; set; }
        public string? Domain { get; set; }
        public string? Alias { get; set; }
        public string? Tags { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
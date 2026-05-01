namespace Models
{
    public class TokenDetail
    {
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? Email { get; set; }
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddHours(1);
    }
}

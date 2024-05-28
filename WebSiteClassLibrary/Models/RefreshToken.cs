namespace WebSiteClassLibrary.Models
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public DateTime ExpiryDate { get; set; }

        public User User { get; set; }
    }
}

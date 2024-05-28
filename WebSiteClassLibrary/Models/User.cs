using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebSiteClassLibrary.Models;

public class User
{
    [Key]
    public Guid id { get; set; } = Guid.NewGuid();

    [MinLength(4)]
    public string? login { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [Required, NotNull]
    public string password { get; set; }

    [Required, NotNull]
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    [Required, NotNull]
    public string Role { get; set; }
    [Required, NotNull]
    public bool IsAnonymous {  get; set; }


    public RefreshToken token { get; set; }
}
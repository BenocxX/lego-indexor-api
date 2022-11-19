using System.ComponentModel.DataAnnotations;

namespace lego_indexor_api.Core.Models.DTOs.AuthenticationRequests;

public class AuthenticationTokenRequest
{
    [Required]
    public string? Token { get; set; }
}
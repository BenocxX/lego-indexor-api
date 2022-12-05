using System.ComponentModel.DataAnnotations;

namespace lego_indexor_api.Core.Models.DTOs.AuthenticationRequests;

public class AuthenticationSignupRequest : AuthenticationRequest
{
    [Required]
    [StringLength(255)]
    public string? ConfirmPassword { get; set; }
    
    [Required]
    public int RaspberryPiId { get; set; }
}
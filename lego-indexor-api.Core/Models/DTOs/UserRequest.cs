using System.ComponentModel.DataAnnotations;

namespace lego_indexor_api.Core.Models.DTOs;

public class UserRequest
{
    [Required]
    [StringLength(255)]
    public string? Username { get; set; }
    
    [Required]
    [StringLength(255)]
    public string? Password { get; set; }
}
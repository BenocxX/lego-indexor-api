using System.ComponentModel.DataAnnotations;

namespace lego_indexor_api.Core.Models.DTOs;

public class ScanRequest
{
    [Required]
    public string? Token { get; set; }
}
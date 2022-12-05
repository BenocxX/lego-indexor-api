using System.ComponentModel.DataAnnotations;

namespace lego_indexor_api.Core.Models.DTOs;

public class LearnScanRequest : TokenRequest
{
    [Required] 
    public string FolderName { get; set; }
}
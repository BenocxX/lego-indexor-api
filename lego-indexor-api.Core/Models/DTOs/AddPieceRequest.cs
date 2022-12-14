using System.ComponentModel.DataAnnotations;

namespace lego_indexor_api.Core.Models.DTOs;

public class AddPieceRequest : TokenRequest
{
    [Required] 
    [StringLength(255)]
    public string Type { get; set; }
    
    [StringLength(255)]
    public string? Name { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "Veuillez entrer un nombre valide.")]
    public int? Count { get; set; }
    
    [StringLength(1000)]
    public string? Description { get; set; }
}
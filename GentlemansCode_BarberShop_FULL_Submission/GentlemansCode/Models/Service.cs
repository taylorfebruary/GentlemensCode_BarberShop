using System.ComponentModel.DataAnnotations;

namespace GentlemansCode.Models;

public class Service
{
    public int ServiceId { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int DurationMinutes { get; set; }

    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; } = true;
}

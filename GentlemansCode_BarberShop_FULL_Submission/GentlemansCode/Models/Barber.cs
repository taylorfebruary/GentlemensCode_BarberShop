using System.ComponentModel.DataAnnotations;

namespace GentlemansCode.Models;

public class Barber
{
    public int BarberId { get; set; }

    [Required, StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [StringLength(1000)]
    public string Bio { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }

    [StringLength(100)]
    public string Specialty { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}

using System.ComponentModel.DataAnnotations;

namespace BookingRoom.Models;

public class Booking
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string BorrowerName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string RoomName { get; set; } = string.Empty;

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "pending"; // pending/approved/rejected

    [StringLength(255)]
    public string? Purpose { get; set; }

    
}

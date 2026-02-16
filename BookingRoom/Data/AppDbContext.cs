using Microsoft.EntityFrameworkCore;
using BookingRoom.Models;

namespace BookingRoom.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Booking> Bookings => Set<Booking>();
}

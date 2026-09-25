using GentlemansCode.Models;
using Microsoft.EntityFrameworkCore;

namespace GentlemansCode.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Service> Services => Set<Service>();
    public DbSet<Barber> Barbers => Set<Barber>();
    public DbSet<Booking> Bookings => Set<Booking>();
}

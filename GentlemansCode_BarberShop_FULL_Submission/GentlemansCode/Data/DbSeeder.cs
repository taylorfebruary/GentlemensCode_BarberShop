using GentlemansCode.Models;

namespace GentlemansCode.Data;

public static class DbSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        if (!context.Services.Any())
        {
            context.Services.AddRange(
                new Service { Name = "Classic Haircut", Description = "A clean, timeless haircut finished to perfection.", Price = 180, DurationMinutes = 45 },
                new Service { Name = "Skin Fade", Description = "A sharp skin fade with detailed finishing.", Price = 220, DurationMinutes = 50 },
                new Service { Name = "Beard Trim", Description = "Professional beard shaping and finishing.", Price = 120, DurationMinutes = 30 },
                new Service { Name = "Haircut + Beard", Description = "Our classic haircut combined with a detailed beard trim.", Price = 280, DurationMinutes = 70 },
                new Service { Name = "Kids Cut", Description = "A stylish and comfortable cut for younger clients.", Price = 140, DurationMinutes = 35 },
                new Service { Name = "The Gentleman's Cut", Description = "Premium haircut, beard detailing and hot towel finish.", Price = 350, DurationMinutes = 90 }
            );
            context.SaveChanges();
        }

        if (!context.Barbers.Any())
        {
            context.Barbers.AddRange(
                new Barber { FullName = "Marcus Williams", Specialty = "Skin Fades", Bio = "Specialist in modern fades and precision styling." },
                new Barber { FullName = "Liam Daniels", Specialty = "Classic Cuts", Bio = "Known for timeless cuts and detailed finishing." },
                new Barber { FullName = "Ethan Cole", Specialty = "Beard Grooming", Bio = "Expert in beard shaping and premium grooming." }
            );
            context.SaveChanges();
        }
    }
}

using GentlemansCode.Data;
using GentlemansCode.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
namespace GentlemansCode.Controllers;
public class BookingController : Controller
{
    private readonly ApplicationDbContext _db;
    public BookingController(ApplicationDbContext db) => _db = db;
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var vm = new BookingViewModel { Services = await _db.Services.Where(x=>x.IsActive).OrderBy(x=>x.Price).ToListAsync(), Barbers = await _db.Barbers.Where(x=>x.IsActive).OrderBy(x=>x.FullName).ToListAsync() };
        return View(vm);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(BookingViewModel vm)
    {
        vm.Services = await _db.Services.Where(x=>x.IsActive).OrderBy(x=>x.Price).ToListAsync();
        vm.Barbers = await _db.Barbers.Where(x=>x.IsActive).OrderBy(x=>x.FullName).ToListAsync();
        if (vm.AppointmentDate.Date < DateTime.Today) ModelState.AddModelError(nameof(vm.AppointmentDate), "Please choose a future date.");
        if (!TimeSpan.TryParse(vm.AppointmentTime, out var parsed) || parsed < TimeSpan.FromHours(9) || parsed >= TimeSpan.FromHours(17)) ModelState.AddModelError(nameof(vm.AppointmentTime), "Please choose a time between 09:00 and 17:00.");
        var service = await _db.Services.FindAsync(vm.ServiceId); var barber = await _db.Barbers.FindAsync(vm.BarberId);
        if (service is null || barber is null) ModelState.AddModelError("", "Please select a valid service and barber.");
        if (!ModelState.IsValid) return View(vm);
        var start = vm.AppointmentDate.Date + parsed; var end = start.AddMinutes(service!.DurationMinutes);
        if (end.TimeOfDay > TimeSpan.FromHours(18)) ModelState.AddModelError(nameof(vm.AppointmentTime), "That appointment would finish after closing time.");
        var conflict = await _db.Bookings.AnyAsync(b => b.BarberId == vm.BarberId && b.AppointmentDate == vm.AppointmentDate.Date && b.AppointmentTime == parsed && b.Status != "Cancelled");
        if (conflict) ModelState.AddModelError(nameof(vm.AppointmentTime), "That time is already booked with this barber. Please choose another slot.");
        if (!ModelState.IsValid) return View(vm);
        var booking = new Booking { ServiceId=vm.ServiceId, BarberId=vm.BarberId, AppointmentDate=vm.AppointmentDate.Date, AppointmentTime=parsed, CustomerName=vm.CustomerName.Trim(), CustomerEmail=vm.CustomerEmail.Trim(), CustomerPhone=vm.CustomerPhone.Trim(), Notes=string.IsNullOrWhiteSpace(vm.Notes)?null:vm.Notes.Trim(), CreatedAt=DateTime.UtcNow, Status="Confirmed" };
        _db.Bookings.Add(booking); await _db.SaveChangesAsync();
        booking.Service=service; booking.Barber=barber;
        return RedirectToAction(nameof(Confirmation), new { id=booking.BookingId });
    }
    [HttpGet] public async Task<IActionResult> Confirmation(int id)
    {
        var booking = await _db.Bookings.Include(x=>x.Service).Include(x=>x.Barber).FirstOrDefaultAsync(x=>x.BookingId==id);
        if (booking is null) return NotFound();
        var start = DateTime.ParseExact($"{booking.AppointmentDate:yyyy-MM-dd} {booking.AppointmentTime}", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
        var end = start.AddMinutes(booking.Service!.DurationMinutes);
        var details = $"Barber: {booking.Barber!.FullName}\\nService: {booking.Service.Name}\\nCustomer: {booking.CustomerName}\\nPhone: {booking.CustomerPhone}";
        var google = "https://calendar.google.com/calendar/render?action=TEMPLATE" + $"&text={Uri.EscapeDataString("Gentleman's Code - "+booking.Service.Name)}" + $"&dates={start.ToUniversalTime():yyyyMMddTHHmmssZ}/{end.ToUniversalTime():yyyyMMddTHHmmssZ}" + $"&details={Uri.EscapeDataString(details)}" + $"&location={Uri.EscapeDataString("18 Bree Street, Cape Town, South Africa")}";
        return View(new BookingConfirmationViewModel { Booking=booking, GoogleCalendarUrl=google, IcsUrl=Url.Action(nameof(Calendar), new { id }, Request.Scheme)! });
    }
    [HttpGet] public async Task<IActionResult> Calendar(int id)
    {
        var b = await _db.Bookings.Include(x=>x.Service).Include(x=>x.Barber).FirstOrDefaultAsync(x=>x.BookingId==id); if(b is null) return NotFound();
        var start = DateTime.ParseExact($"{b.AppointmentDate:yyyy-MM-dd} {b.AppointmentTime}", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture); var end=start.AddMinutes(b.Service!.DurationMinutes);
        string Esc(string s)=>s.Replace("\\","\\\\").Replace(";","\\;").Replace(",","\\,").Replace("\n","\\n");
        var body="BEGIN:VCALENDAR\r\nVERSION:2.0\r\nPRODID:-//Gentleman's Code//Booking//EN\r\nBEGIN:VEVENT\r\n"+$"UID:booking-{b.BookingId}@gentlemanscode\r\nDTSTAMP:{DateTime.UtcNow:yyyyMMddTHHmmssZ}\r\nDTSTART:{start:yyyyMMddTHHmmss}\r\nDTEND:{end:yyyyMMddTHHmmss}\r\nSUMMARY:{Esc("Gentleman's Code - "+b.Service.Name)}\r\nLOCATION:{Esc("18 Bree Street, Cape Town, South Africa")}\r\nDESCRIPTION:{Esc("Barber: "+b.Barber!.FullName+"\\nCustomer: "+b.CustomerName+"\\nPhone: "+b.CustomerPhone)}\r\nEND:VEVENT\r\nEND:VCALENDAR\r\n";
        return File(Encoding.UTF8.GetBytes(body), "text/calendar", "gentlemans-code-appointment.ics");
    }
}

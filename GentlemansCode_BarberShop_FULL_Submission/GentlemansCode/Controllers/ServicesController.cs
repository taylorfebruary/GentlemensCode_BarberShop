using GentlemansCode.Data; using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore;
namespace GentlemansCode.Controllers;
public class ServicesController : Controller { private readonly ApplicationDbContext _db; public ServicesController(ApplicationDbContext db)=>_db=db; public async Task<IActionResult> Index()=>View(await _db.Services.Where(x=>x.IsActive).OrderBy(x=>x.Price).ToListAsync()); }

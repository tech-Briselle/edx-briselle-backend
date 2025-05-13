using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using edx_briselle.Server.Data; // Import your DbContext namespace
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class DatabaseController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DatabaseController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("test-connection")]
    public async Task<IActionResult> TestConnection()
    {
        var canConnect = await _context.Database.CanConnectAsync();
        return canConnect ? Ok("✅ Database connected successfully!") : StatusCode(500, "❌ Database connection failed!");
    }
}

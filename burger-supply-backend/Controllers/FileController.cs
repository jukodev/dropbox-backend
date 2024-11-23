using burger_supply_backend.Database;
using burger_supply_backend.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace burger_supply_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileController(DataContext dataContext) : ControllerBase
{
    [HttpGet("/download/{id}")]
    public async Task<IActionResult> DownloadFile(string id)
    {
        var file = await dataContext.Files.Where(f => f.PublicId == id).FirstOrDefaultAsync();
        if (file == null)
        {
            return NotFound();
        }
        return File(file.FilePath, "application/octet-stream", file.FileName);
    }
    
    [HttpPost("/upload")]
    [RequestSizeLimit(long.MaxValue)]
    [RequestFormLimits(MultipartBodyLengthLimit = long.MaxValue)]
    public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
    {
        var fileName = file.FileName;
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fileName);
        var user = HttpContext.Items["User"] as UserDbModel;
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        var fileDbModel = new FileDbModel
        {
            FileName = fileName,
            FilePath = filePath,
            CreatedAt = DateTime.Now,
            CreatedBy = user!,
            PublicId = Guid.NewGuid().ToString()
        };
        await dataContext.Files.AddAsync(fileDbModel);
        await dataContext.SaveChangesAsync();
        return Ok(fileDbModel);
    }
}
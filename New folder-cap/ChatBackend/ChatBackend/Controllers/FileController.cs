using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    [HttpPost("upload")]
    public IActionResult Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file selected.");

        // Save file to wwwroot/uploads
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", file.FileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        var url = $"http://localhost:5000/uploads/{file.FileName}";

        return Ok(new { attachmentUrl = url });
    }
}

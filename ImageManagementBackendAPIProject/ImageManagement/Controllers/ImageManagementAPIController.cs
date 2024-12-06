namespace ImageManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImageManagementAPIController : ControllerBase
{
	private readonly ImageContext _context;
	private readonly IWebHostEnvironment _env;

	public ImageManagementAPIController(ImageContext context, IWebHostEnvironment env)
	{
		_context = context;
		_env = env;
	}

	[HttpGet]
	public async Task<IActionResult> GetImages()
	{
		var images = await _context.Images.ToListAsync();
		return Ok(images);
	}

	[HttpPost]
	public async Task<IActionResult> UploadImage(IFormFile file, ImageModel imageDetails)
	{
		if (file == null || file.Length == 0)
		{
			return BadRequest("No file uploaded.");
		}

		var uploadsFolder = Path.Combine(imageDetails.Url, "uploads");
		if (!Directory.Exists(uploadsFolder))
		{
			Directory.CreateDirectory(uploadsFolder);
		}

		var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
		var filePath = Path.Combine(uploadsFolder, fileName);

		using (var stream = new FileStream(filePath, FileMode.Create))
		{
			await file.CopyToAsync(stream);
		}

		var image = new ImageModel
		{
			User = imageDetails.User,
			Url = imageDetails.Url,
			Description = imageDetails.Description
		};

		_context.Images.Add(image);
		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(GetImages), new { id = image.Id }, image);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteImage(ImageModel imageDetails)
	{
		var image = await _context.Images.FindAsync(imageDetails.Id);
		if (image == null)
		{
			return NotFound();
		}

		var filePath = Path.Combine(imageDetails.Url, image.Url.TrimStart('/'));
		if (System.IO.File.Exists(filePath))
		{
			System.IO.File.Delete(filePath);
		}

		_context.Images.Remove(image);
		await _context.SaveChangesAsync();

		return NoContent();
	}
}
namespace ImageManagement.Domain;

public class ImageContext : DbContext
{
	public ImageContext(DbContextOptions<ImageContext> options) : base(options) { }
	public DbSet<ImageModel> Images { get; set; }
}
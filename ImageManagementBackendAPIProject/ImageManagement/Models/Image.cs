using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ImageManagement.Models;

[Table("Images")]
public class ImageModel
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	[Required]
	public string User { get; set; }
	[Required]
	[Url]
	public string Url { get; set; }
	[Required]
	public string Description { get; set; }
	public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}
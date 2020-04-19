using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models {
	public abstract class PageViewModel { }

	public abstract class PageEditModel {
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		public string Html { get; set; }
	}
}
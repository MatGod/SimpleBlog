using System.ComponentModel.DataAnnotations;
using DataLayer.Entityes;

namespace PresentationLayer.Models {
	public class MaterialViewModel : PageViewModel {
		public Material Material { get; set; }
	}

	public class MaterialEditModel : PageEditModel {
		[Required] public int DirectoryId { get; set; }
	}
}
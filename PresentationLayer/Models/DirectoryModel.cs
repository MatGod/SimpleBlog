using System.Collections.Generic;
using DomenModels.Models;

namespace PresentationLayer.Models {
	public class DirectoryViewModel : PageViewModel {
		public Directory Directory { get; set; } = new Directory();
		public List<MaterialViewModel> Materials { get; set; } = new List<MaterialViewModel>();
	}

	public class DirectoryEditModel : PageEditModel {
		
	}
}
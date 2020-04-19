using System.Linq;
using DataLayer.Entityes;

namespace DataLayer {
	public static class SampleData {
		public static void InitData(EFDBContext context) {
			if (!context.Directory.Any()) {
				context.Directory.Add(new Directory {
					Title = "FirstDirectory", 
					Html = "<b>DirectoryContent</b>"
				});
				context.Directory.Add(new Directory {
					Title = "SecondDirectory", 
					Html = "<b>DirectoryContent</b>"
				});
				context.SaveChanges();
			}

			if (context.Material.Any()) return;
			context.Material.Add(new Material { 
				Title = "First Material", 
				Html = "<i>MaterialContent</i>", 
				DirectoryId = context.Directory.ToList().First().Id
			});
			context.Material.Add(new Material {
				Title = "Second Material",
				Html = "<i>MaterialContent</i>",
				DirectoryId = context.Directory.ToList().Last().Id
			});
			context.SaveChanges();
		}
	}
}
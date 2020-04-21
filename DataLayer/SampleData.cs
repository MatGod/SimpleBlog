using System.Linq;
using DataLayer.Entityes;

namespace DataLayer {
	public static class SampleData {
		public static void InitData(EFDBContext context) {
			if (!context.Directory.Any()) {
				context.Directory.Add(new Directory {
					Title = "First Directory", 
					Html = "Directory Content"
				});
				context.Directory.Add(new Directory {
					Title = "Second Directory", 
					Html = "Directory Content"
				});
				context.SaveChanges();
			}

			if (context.Material.Any()) return;
			context.Material.Add(new Material { 
				Title = "First Material", 
				Html = "Material Content", 
				DirectoryId = context.Directory.ToList().First().Id
			});
			context.Material.Add(new Material {
				Title = "Second Material",
				Html = "Material Content",
				DirectoryId = context.Directory.ToList().Last().Id
			});
			context.SaveChanges();
		}
	}
}
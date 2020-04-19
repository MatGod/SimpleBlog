using System.Linq;
using DataLayer.Entityes;

namespace DataLayer {
	public class SampleData {
		public static void InitData(EFDBContext context) {
			if (!context.Directory.Any()) {
				context.Directory.Add(new Entityes.Directory() {
					Title = "FirstDirectory", 
					Html = "<b>DirectoryContent</b>"
				});
				context.Directory.Add(new Entityes.Directory() {
					Title = "SecondDirectory", 
					Html = "<b>DirectoryContent</b>"
				});
				context.SaveChanges();
			}

			if (!context.Material.Any()) {
				context.Material.Add(new Entityes.Material() { 
					Title = "First Material", 
					Html = "<i>MaterialContent</i>", 
					DirectoryId = context.Directory.First().Id
				});
				context.Material.Add(new Entityes.Material() {
					Title = "Second Material",
					Html = "<i>MaterialContent</i>",
					DirectoryId = context.Directory.Last().Id
				});
				context.SaveChanges();
			}
		}
	}
}
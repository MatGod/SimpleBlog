using System.Collections.Generic;
using DataLayer;
using DataLayer.Entityes;

namespace BusinessLayerTests {
	public static class TestBaseInitializer {
		public static void Initialize(EFDBContext context) {
			var directories = new[] {
				new Directory {Title = "FirstDirectory"}, 
				new Directory {Title = "SecondDirectory"},
				new Directory {Title = "Third empty Directory"}
			};
			var materials = new[] {
				new Material {Directory = directories[0], DirectoryId = directories[0].Id, Title = "First material"},
				new Material {Directory = directories[0], DirectoryId = directories[0].Id, Title = "Second material"},
				new Material {Directory = directories[1], DirectoryId = directories[1].Id, Title = "Third material"}
			};
			directories[0].Materials = new List<Material> {materials[0], materials[1]};
			directories[1].Materials = new List<Material> {materials[2]};

			context.Directory.AddRange(directories);
			context.Material.AddRange(materials);
			context.SaveChanges();
		}
	}
}
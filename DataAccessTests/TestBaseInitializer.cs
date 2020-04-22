using System.Collections.Generic;
using DataLayer;
using DataLayer.Entityes;

namespace DataAccessTests {
	public static class TestBaseInitializer {
		public static void Initialize(EFDBContext context) {
			var directories = new[] {
				new DbDirectory {Title = "FirstDirectory"}, 
				new DbDirectory {Title = "SecondDirectory"},
				new DbDirectory {Title = "Third empty Directory"}
			};
			var materials = new[] {
				new DbMaterial {DirectoryId = directories[0].Id, Title = "First material"},
				new DbMaterial {DirectoryId = directories[0].Id, Title = "Second material"},
				new DbMaterial {DirectoryId = directories[1].Id, Title = "Third material"}
			};
			directories[0].Materials = new List<DbMaterial> {materials[0], materials[1]};
			directories[1].Materials = new List<DbMaterial> {materials[2]};

			context.Directory.AddRange(directories);
			context.Material.AddRange(materials);
			context.SaveChanges();
		}
	}
}
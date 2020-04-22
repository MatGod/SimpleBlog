using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataAccessLayer.Interfaces;
using DataLayer;
using DataLayer.Entityes;
using Microsoft.EntityFrameworkCore;
using DomenModels.Models;

namespace DataAccessLayer.Implementations {
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EFDirectoryRepository : IDirectoryRepository {
		private readonly EFDBContext _context;

		public EFDirectoryRepository(EFDBContext context) {
			_context = context;
		}

		public IEnumerable<Directory> GetAllDirectories() {
			var dbDirectories = _context.Set<DbDirectory>()
			                            .Include(x => x.Materials).ToList();

			return dbDirectories.Select(ConvertDBModel).ToList();
		}

		public Directory GetDirectoryById(int directoryId) {
			return ConvertDBModel(_context.Set<DbDirectory>().Include(x => x.Materials).
			                               FirstOrDefault(x => x.Id == directoryId));
		}

		public int SaveDirectory(Directory directory) {
			var dbDirectory = _context.Set<DbDirectory>().Include(x => x.Materials)
			                          .FirstOrDefault(x => x.Id == directory.Id);
			
			if (dbDirectory == null) {
				dbDirectory = ConvertModel(directory);
				_context.Directory.Add(dbDirectory);
			}
			else {
				dbDirectory.Title = directory.Title;
				dbDirectory.Html = directory.Html;
				_context.Entry(dbDirectory).State = EntityState.Modified;
			}

			_context.SaveChanges();
			return dbDirectory.Id;
		}

		public void DeleteDirectory(Directory directory) {
			var dbDirectory = _context.Set<DbDirectory>().Include(x => x.Materials)
			                          .FirstOrDefault(x => x.Id == directory.Id);
			if (dbDirectory == null) return;
			_context.Directory.Remove(dbDirectory);
			_context.SaveChanges();
		}

		private static Directory ConvertDBModel(DbDirectory dbDirectory) {
			if (dbDirectory == null) return null;
			var dir = new Directory {
				Id = dbDirectory.Id,
				Materials = new List<Material>(),
				Html = dbDirectory.Html,
				Title = dbDirectory.Title
			};
			foreach (var material in dbDirectory.Materials.Select(EFMaterialRepository.ConvertDBModel)) {
				dir.Materials.Add(material);
			}
			
			return dir;
		}

		private static DbDirectory ConvertModel(Directory directory) {
			var dir = new DbDirectory {
				Id = directory.Id,
				Materials = new List<DbMaterial>(),
				Html = directory.Html,
				Title = directory.Title
			};
			if (directory.Materials == null) return dir;
			foreach (var material in directory.Materials.Select(material => new DbMaterial() {
				Id = material.Id,
				DirectoryId = dir.Id,
				Html = material.Html,
				Title = material.Title
			})) {
				dir.Materials.Add(material);
			}
			return dir;
		}
	}
}
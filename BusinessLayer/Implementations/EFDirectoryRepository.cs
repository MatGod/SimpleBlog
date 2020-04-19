using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Interfaces;
using DataLayer;
using DataLayer.Entityes;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Implementations {
	public class EFDirectoryRepository : IDirectoryRepository {
		private readonly EFDBContext _context;

		public EFDirectoryRepository(EFDBContext context) {
			this._context = context;
		}
		
		public IEnumerable<Directory> GetAllDirectories(bool includeMaterials = false) {
			return includeMaterials
				? _context.Set<Directory>().Include(x => x.Materials).AsNoTracking().ToList()
				: _context.Directory.ToList();
		}

		public Directory GetDirectoryById(int directoryId, bool includeMaterials = false) {
			return includeMaterials
				? _context.Set<Directory>().Include(x => x.Materials).AsNoTracking().
				        FirstOrDefault(x => x.Id == directoryId)
				: _context.Directory.FirstOrDefault(x => x.Id == directoryId);
		}

		public int SaveDirectory(Directory directory) {
			if (directory.Id == 0) {
				_context.Directory.Add(directory);
			}
			else {
				_context.Entry(directory).State = EntityState.Modified;
			}

			_context.SaveChanges();
			return directory.Id;
		}

		public void DeleteDirectory(Directory directory) {
			_context.Directory.Remove(directory);
			_context.SaveChanges();
		}
	}
}
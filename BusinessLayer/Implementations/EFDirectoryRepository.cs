using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BusinessLayer.Interfaces;
using DataLayer;
using DataLayer.Entityes;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Implementations {
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EFDirectoryRepository : IDirectoryRepository {
		private readonly EFDBContext _context;

		public EFDirectoryRepository(EFDBContext context) {
			this._context = context;
		}
		
		public IEnumerable<Directory> GetAllDirectories() {
			return _context.Set<Directory>().Include(x => x.Materials).ToList();
		}

		public Directory GetDirectoryById(int directoryId) {
			return _context.Set<Directory>().Include(x => x.Materials).
				        FirstOrDefault(x => x.Id == directoryId);
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
			try {
				_context.Directory.Remove(directory);
				_context.SaveChanges();
			}
			catch (DbUpdateConcurrencyException) { }
		}
	}
}
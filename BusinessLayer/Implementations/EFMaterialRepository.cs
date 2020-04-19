using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Interfaces;
using DataLayer;
using DataLayer.Entityes;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Implementations {
	public class EFMaterialRepository : IMaterialRepository {
		private readonly EFDBContext _context;

		public EFMaterialRepository(EFDBContext context) {
			this._context = context;
		}
		
		public IEnumerable<Material> GetAllMaterials(bool includeDirectory = false) {
			return includeDirectory
				? _context.Set<Material>().Include(x => x.Directory).AsNoTracking().ToList()
				: _context.Material.ToList();
		}

		public Material GetMaterialById(int materialId, bool includeDirectory = false) {
			return includeDirectory
				? _context.Set<Material>().Include(x => x.Directory).AsNoTracking()
				         .FirstOrDefault(x => x.Id == materialId)
				: _context.Material.FirstOrDefault(x => x.Id == materialId);
		}

		public int SaveMaterial(Material material) {
			if (material.Id == 0) {
				_context.Material.Add(material);
			}
			else {
				_context.Entry(material).State = EntityState.Modified;
			}

			_context.SaveChanges();
			return 1;
		}

		public void DeleteMaterial(Material material) {
			_context.Material.Remove(material);
			_context.SaveChanges();
		}
	}
}
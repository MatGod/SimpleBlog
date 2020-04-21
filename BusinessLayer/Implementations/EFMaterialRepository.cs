using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BusinessLayer.Interfaces;
using DataLayer;
using DataLayer.Entityes;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Implementations {
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EFMaterialRepository : IMaterialRepository {
		private readonly EFDBContext _context;

		public EFMaterialRepository(EFDBContext context) {
			this._context = context;
		}
		
		public IEnumerable<Material> GetAllMaterials() {
			return _context.Set<Material>().Include(x => x.Directory).ToList();
		}

		public Material GetMaterialById(int materialId) {
			return _context.Set<Material>().Include(x => x.Directory)
				         .FirstOrDefault(x => x.Id == materialId);
		}

		public int SaveMaterial(Material material) {
			if (material.Id == 0) {
				_context.Material.Add(material);
			}
			else {
				_context.Entry(material).State = EntityState.Modified;
			}

			_context.SaveChanges();
			return material.Id;
		}

		public void DeleteMaterial(Material material) {
			try {
				_context.Material.Remove(material);
				_context.SaveChanges();
			} catch (DbUpdateConcurrencyException) {}
		}
	}
}
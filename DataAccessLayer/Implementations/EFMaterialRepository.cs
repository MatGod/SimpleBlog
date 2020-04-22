using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataAccessLayer.Interfaces;
using DataLayer;
using DataLayer.Entityes;
using DomenModels.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Implementations {
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EFMaterialRepository : IMaterialRepository {
		private readonly EFDBContext _context;

		public EFMaterialRepository(EFDBContext context) {
			_context = context;
		}
		
		public IEnumerable<Material> GetAllMaterials() {
			return _context.Material.Select(dbMaterial => ConvertDBModel(dbMaterial)).ToList();
		}

		public Material GetMaterialById(int materialId) {
			return ConvertDBModel(_context.Material.FirstOrDefault(x => x.Id == materialId));
		}

		public int SaveMaterial(Material material) {
			var dbMaterial = _context.Material.FirstOrDefault(x => x.Id == material.Id);
			if (dbMaterial == null) {
				dbMaterial = ConvertModel(material);
				_context.Material.Add(dbMaterial);
			}
			else {
				dbMaterial.Title = material.Title;
				dbMaterial.Html = material.Html;
				_context.Entry(dbMaterial).State = EntityState.Modified;
			}

			_context.SaveChanges();
			return dbMaterial.Id;
		}

		public void DeleteMaterial(Material material) {
			var dbMaterial = _context.Material.FirstOrDefault(x => x.Id == material.Id);
			if (dbMaterial == null) return;
			_context.Material.Remove(dbMaterial);
			_context.SaveChanges();
		}

		private DbMaterial ConvertModel(Material material) {
			var dbMaterial = _context.Material.FirstOrDefault(x => x.Id == material.Id) ?? new DbMaterial {
				Id = material.Id,
				Title = material.Title,
				Html = material.Html,
				DirectoryId = material.DirectoryId,
				Directory = _context.Directory.FirstOrDefault(x => x.Id == material.DirectoryId)
			};

			return dbMaterial;
		}

		public static Material ConvertDBModel(DbMaterial dbMaterial) {
			if (dbMaterial == null) return null;
			return new Material {
				Id = dbMaterial.Id,
				Title = dbMaterial.Title,
				DirectoryId = dbMaterial.DirectoryId,
				Html = dbMaterial.Html
			};
		}
	}
}
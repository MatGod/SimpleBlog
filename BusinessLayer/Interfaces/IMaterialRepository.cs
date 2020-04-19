using System.Collections.Generic;
using DataLayer.Entityes;

namespace BusinessLayer.Interfaces {
	public interface IMaterialRepository {
		IEnumerable<Material> GetAllMaterials(bool includeDirectory = false);
		Material GetMaterialById(int materialId, bool includeDirectory = false);
		int SaveMaterial(Material material);
		void DeleteMaterial(Material material);
	}
}
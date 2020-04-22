using System.Collections.Generic;
using DomenModels.Models;

namespace DataAccessLayer.Interfaces {
	public interface IMaterialRepository {
		IEnumerable<Material> GetAllMaterials();
		Material GetMaterialById(int materialId);
		int SaveMaterial(Material material);
		void DeleteMaterial(Material material);
	}
}
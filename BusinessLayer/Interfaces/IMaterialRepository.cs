using System.Collections.Generic;
using DataLayer.Entityes;

namespace BusinessLayer.Interfaces {
	public interface IMaterialRepository {
		IEnumerable<Material> GetAllMaterials();
		Material GetMaterialById(int materialId);
		int SaveMaterial(Material material);
		void DeleteMaterial(Material material);
	}
}
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;
using DomenModels.Models;

namespace BusinessLayer.Logic {
	public class MaterialLogic {
		private readonly DataManager _dataManager;
		
		public MaterialLogic(DataManager dataManager) {
			_dataManager = dataManager;
		}

		public List<Material> GetAll() {
			return _dataManager.Material.GetAllMaterials().ToList();
		}

		public Material GetById(int id) {
			return _dataManager.Material.GetMaterialById(id);
		}

		public int Save(Material material) {
			return _dataManager.Material.SaveMaterial(material);
		}

		public void Delete(Material material) {
			_dataManager.Material.DeleteMaterial(material);
		}

		public string GetDirectoryTitle(Material material) {
			return _dataManager.Directory.GetDirectoryById(material.DirectoryId).Title;
		}

		public void MoveToDirectory(Material material, Directory directory) {
			var dir = _dataManager.Directory.GetDirectoryById(material.DirectoryId);
			dir.Materials.Remove(material);
			_dataManager.Directory.SaveDirectory(dir);
			directory.Materials.Add(material);
			material.DirectoryId = directory.Id;
			_dataManager.Material.SaveMaterial(material);
			_dataManager.Directory.SaveDirectory(directory);
		}
	}
}
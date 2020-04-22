using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;
using DomenModels.Models;

namespace BusinessLayer.Logic {
	public class DirectoryLogic {
		private readonly DataManager _dataManager;
		
		public DirectoryLogic(DataManager dataManager) {
			_dataManager = dataManager;
		}

		public List<Directory> GetAll() {
			return _dataManager.Directory.GetAllDirectories().ToList();
		}

		public Directory GetById(int id) {
			return _dataManager.Directory.GetDirectoryById(id);
		}

		public int Save(Directory directory) {
			return _dataManager.Directory.SaveDirectory(directory);
		}

		public void Delete(Directory directory) {
			_dataManager.Directory.DeleteDirectory(directory);
		}

		public void Clear(Directory directory) {
			if (directory.Materials == null) return;
			directory.Materials.Clear();
			_dataManager.Directory.SaveDirectory(directory);
		}

		public bool Contains(Directory directory, Material material) {
			return directory.Materials.Contains(material);
		}
	}
}
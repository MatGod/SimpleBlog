using System.Diagnostics.CodeAnalysis;
using BusinessLayer;
using DataLayer.Entityes;
using PresentationLayer.Models;

namespace PresentationLayer.Services {
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class MaterialService {
		private readonly DataManager _dataManager;

		public MaterialService(DataManager dataManager) {
			_dataManager = dataManager;
		}

		public MaterialViewModel MaterialDBModelToView(int materialId) {
			return new MaterialViewModel {
				Material = _dataManager.MaterialRepository.GetMaterialById(materialId, true)
			};
		}

		public MaterialViewModel SaveMaterialEditModelToDb(MaterialEditModel editModel) {
			var material = editModel.Id != 0 
				? _dataManager.MaterialRepository.GetMaterialById(editModel.Id) 
				: new Material();
			material.Title = editModel.Title;
			material.Html = editModel.Html;
			material.DirectoryId = editModel.DirectoryId;
			_dataManager.MaterialRepository.SaveMaterial(material);
			return MaterialDBModelToView(material.Id);
		}
		
		public MaterialEditModel GetMaterialEditModel(int materialId) {
			var databaseModel = _dataManager.MaterialRepository.GetMaterialById(materialId);
			return new MaterialEditModel() {
				Id = databaseModel.Id,
				DirectoryId = databaseModel.DirectoryId,
				Title = databaseModel.Title,
				Html = databaseModel.Html
			};
		}
	}
}
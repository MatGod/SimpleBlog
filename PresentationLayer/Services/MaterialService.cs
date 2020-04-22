using System.Diagnostics.CodeAnalysis;
using BusinessLayer;
using DomenModels.Models;
using PresentationLayer.Models;

namespace PresentationLayer.Services {
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class MaterialService {
		private readonly LogicManager _logicManager;

		public MaterialService(LogicManager logicManager) {
			_logicManager = logicManager;
		}

		public MaterialViewModel MaterialDBModelToView(int materialId) {
			return new MaterialViewModel {
				Material = _logicManager.Material.GetById(materialId)
			};
		}

		public MaterialViewModel SaveMaterialEditModelToDb(MaterialEditModel editModel) {
			var material = editModel.Id != 0 
				? _logicManager.Material.GetById(editModel.Id) 
				: new Material();
			material.Title = editModel.Title;
			material.Html = editModel.Html;
			material.DirectoryId = editModel.DirectoryId;
			material.Id = _logicManager.Material.Save(material);
			return MaterialDBModelToView(material.Id);
		}
		
		public MaterialEditModel GetMaterialEditModel(int materialId, int directoryId) {
			var databaseModel = _logicManager.Material.GetById(materialId);
			if (databaseModel != null) {
				return new MaterialEditModel() {
					Id = databaseModel.Id,
					DirectoryId = databaseModel.DirectoryId,
					Title = databaseModel.Title,
					Html = databaseModel.Html
				};
			}

			return new MaterialEditModel() {
				DirectoryId = directoryId
			};
		}
		
		public void DeleteMaterialEditModelFromDb(MaterialEditModel materialEditModel) {
			_logicManager.Material.Delete(_logicManager.
			                              Material.
			                              GetById(materialEditModel.Id));
		}
	}
}
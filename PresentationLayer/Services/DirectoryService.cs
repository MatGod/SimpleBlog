using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BusinessLayer;
using DomenModels.Models;
using PresentationLayer.Models;

namespace PresentationLayer.Services {
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class DirectoryService {
		private readonly LogicManager _logicManager;
		private readonly MaterialService _materialService;

		public DirectoryService(LogicManager logicManager) {
			_logicManager = logicManager;
			_materialService = new MaterialService(logicManager);
		}

		public List<DirectoryViewModel> GetDirectoriesList() {
			return _logicManager.Directory.GetAll().Select(directory => new DirectoryViewModel {
				Directory = directory,
				Materials = directory.Materials.Select(material => new MaterialViewModel {Material = material}).ToList()
			}).ToList();
		}

		public DirectoryViewModel DirectoryDBToViewModelById(int directoryId) {
			var directory = _logicManager.Directory.GetById(directoryId);
			List<MaterialViewModel> materialViewModels = null;
			try {
				materialViewModels = directory
				                         .Materials.Select(item => _materialService.MaterialDBModelToView(item.Id))
				                         .ToList();
			} catch(NullReferenceException){}

			return new DirectoryViewModel {
				Directory = directory,
				Materials = materialViewModels
			};
		}

		public DirectoryEditModel GetDirectoryEditModel(int directoryId = 0) {
			var directoryDBModel = _logicManager.Directory.GetById(directoryId);
			if (directoryDBModel == null) return new DirectoryEditModel();
			return new DirectoryEditModel {
				Id = directoryDBModel.Id,
				Title = directoryDBModel.Title,
				Html = directoryDBModel.Html
			};
		}

		public DirectoryViewModel SaveDirectoryEditModelToDb(DirectoryEditModel directoryEditModel) {
			directoryEditModel.Id = _logicManager.Directory.Save(new Directory {
				Id = directoryEditModel.Id,
				Title = directoryEditModel.Title,
				Html = directoryEditModel.Html
			});
			return DirectoryDBToViewModelById(directoryEditModel.Id);
		}

		public void DeleteDirectoryEditModelFromDb(DirectoryEditModel directoryEditModel) {
			if (directoryEditModel.Id != 0) {
				_logicManager.Directory.Delete(_logicManager
				                                                 .Directory
				                                                 .GetById(directoryEditModel.Id));
			}
		}
	}
}
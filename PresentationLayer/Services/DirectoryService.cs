using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BusinessLayer;
using DataLayer.Entityes;
using PresentationLayer.Models;

namespace PresentationLayer.Services {
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class DirectoryService {
		private readonly DataManager _dataManager;
		private readonly MaterialService _materialService;

		public DirectoryService(DataManager dataManager) {
			_dataManager = dataManager;
			_materialService = new MaterialService(dataManager);
		}

		public List<DirectoryViewModel> GetDirectoriesList() {
			var dirs = _dataManager.DirectoryRepository.GetAllDirectories();

			return dirs.Select(item => DirectoryDBToViewModelById(item.Id)).ToList();
		}

		public DirectoryViewModel DirectoryDBToViewModelById(int directoryId) {
			var directory = _dataManager.DirectoryRepository.GetDirectoryById(directoryId, true);
			var materialViewModels = directory.Materials.Select(item => _materialService.MaterialDBModelToView(item.Id)).ToList();
			return new DirectoryViewModel { Directory = directory, Materials = materialViewModels};
		}

		public DirectoryEditModel GetDirectoryEditModel(int directoryId = 0) {
			if (directoryId == 0) {
				return new DirectoryEditModel();
			}

			var directoryDBModel = _dataManager.DirectoryRepository.GetDirectoryById(directoryId);
			return new DirectoryEditModel {
				Id = directoryDBModel.Id,
				Title = directoryDBModel.Title,
				Html = directoryDBModel.Html
			};
		}

		public DirectoryViewModel SaveDirectoryEditModelToDb(DirectoryEditModel directoryEditModel) {
			var directoryDbModel = directoryEditModel.Id != 0 
				? _dataManager.DirectoryRepository.GetDirectoryById(directoryEditModel.Id) 
				: new Directory();
			directoryDbModel.Title = directoryEditModel.Title;
			directoryDbModel.Html = directoryEditModel.Html;
			

			return DirectoryDBToViewModelById(directoryDbModel.Id);
		}
	}
}
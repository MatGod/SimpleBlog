using BusinessLayer;
using PresentationLayer.Services;

namespace PresentationLayer {
	public class ServiceManager {
		private readonly DataManager _dataManager;
		private DirectoryService _directoryService;
		private MaterialService _materialService;
		public ServiceManager(DataManager dataManager) {
			_dataManager = dataManager;
			_directoryService = new DirectoryService(_dataManager);
			_materialService = new MaterialService(_dataManager);
		}
		
		public DirectoryService DirectoryService => _directoryService;
		public MaterialService MaterialService => _materialService;
	}
}
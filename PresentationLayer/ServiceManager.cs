using BusinessLayer;
using PresentationLayer.Services;

namespace PresentationLayer {
	public class ServiceManager {
		public DirectoryService DirectoryService { get; }
		public MaterialService MaterialService { get; }
		
		public ServiceManager(DataManager dataManager) {
			DirectoryService = new DirectoryService(dataManager);
			MaterialService = new MaterialService(dataManager);
		}
	}
}
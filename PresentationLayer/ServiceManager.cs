using BusinessLayer;
using DataAccessLayer;
using PresentationLayer.Services;

namespace PresentationLayer {
	public class ServiceManager {
		public DirectoryService DirectoryService { get; }
		public MaterialService MaterialService { get; }
		
		public ServiceManager(LogicManager logicManager) {
			DirectoryService = new DirectoryService(logicManager);
			MaterialService = new MaterialService(logicManager);
		}
	}
}
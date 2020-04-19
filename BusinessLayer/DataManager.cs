using BusinessLayer.Interfaces;

namespace BusinessLayer {
	public class DataManager {
		public IDirectoryRepository DirectoryRepository { get; }
		public IMaterialRepository MaterialRepository { get; }

		public DataManager(IDirectoryRepository directoryRepository, IMaterialRepository materialRepository) {
			DirectoryRepository = directoryRepository;
			MaterialRepository = materialRepository;
		}
	}
}
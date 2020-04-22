using DataAccessLayer.Interfaces;

namespace DataAccessLayer {
	public class DataManager {
		public IDirectoryRepository Directory { get; }
		public IMaterialRepository Material { get; }

		public DataManager(IDirectoryRepository directory, IMaterialRepository material) {
			Directory = directory;
			Material = material;
		}
	}
}
using BusinessLayer.Logic;
using DataAccessLayer;

namespace BusinessLayer {
	public class LogicManager {
		public DirectoryLogic Directory { get; }
		public MaterialLogic Material { get; }

		public LogicManager(DirectoryLogic directory, MaterialLogic material) {
			Directory = directory;
			Material = material;
		}
	}
}
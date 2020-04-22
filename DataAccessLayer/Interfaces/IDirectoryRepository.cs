using System.Collections.Generic;
using DomenModels.Models;

namespace DataAccessLayer.Interfaces {
	public interface IDirectoryRepository {
		IEnumerable<Directory> GetAllDirectories();
		Directory GetDirectoryById(int directoryId);
		int SaveDirectory(Directory directory);
		void DeleteDirectory(Directory directory);
	}
}
using System.Collections.Generic;
using DataLayer.Entityes;

namespace BusinessLayer.Interfaces {
	public interface IDirectoryRepository {
		IEnumerable<Directory> GetAllDirectories();
		Directory GetDirectoryById(int directoryId);
		int SaveDirectory(Directory directory);
		void DeleteDirectory(Directory directory);
	}
}
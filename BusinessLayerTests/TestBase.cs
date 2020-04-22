using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Logic;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DomenModels.Models;
using Moq;

namespace BusinessLayerTests {
	public class TestBase {
		protected readonly DirectoryLogic _directoryLogic;
		protected readonly MaterialLogic _materialLogic;
		protected List<Material> _materials;
        protected List<Directory> _directories;

        protected TestBase() {
	        var directoryMock = new Mock<IDirectoryRepository>();
        	var materialMock = new Mock<IMaterialRepository>();
        	_directoryLogic = new DirectoryLogic(new DataManager(directoryMock.Object, materialMock.Object));
            _materialLogic = new MaterialLogic(new DataManager(directoryMock.Object, materialMock.Object));
        	_materials = new List<Material> {
        		new Material {Id = 1, DirectoryId = 1, Html = "1", Title = "Material 1"},
        		new Material {Id = 2, DirectoryId = 1, Html = "2", Title = "Material 2"},
        		new Material {Id = 3, DirectoryId = 2, Html = "3", Title = "Material 3"}
        	};
        	_directories = new List<Directory> {
        		new Directory {Id = 1, Html = "1", Title = "Directory 1"},
        		new Directory {Id = 2, Html = "2", Title = "Directory 2"},
        		new Directory {Id = 3, Html = "2", Title = "Directory 3"}
        	};
            _directories[0].Materials.Add(_materials[0]);
            _directories[0].Materials.Add(_materials[1]);
            _directories[1].Materials.Add(_materials[2]);
            
        	directoryMock.Setup(manager => manager.GetAllDirectories()).Returns(_directories);
            
        	directoryMock.Setup(manager => manager.GetDirectoryById(It.IsAny<int>()))
        	             .Returns<int>(id => _directories.Find(dir => dir.Id == id));
            
        	directoryMock.Setup(manager => manager.SaveDirectory(It.IsAny<Directory>()))
        	             .Returns<Directory>(dir => {
        		             var index = _directories.IndexOf(dir);
        		             if (index == -1) {
        			             dir.Id = _directories.Last().Id + 1;
        			             _directories.Add(dir);
        			             return dir.Id;
        		             }
                             _directories[index] = dir;
	                         return dir.Id;
                         });
            
	        directoryMock.Setup(manager => manager.DeleteDirectory(It.IsAny<Directory>()))
	                     .Callback((Directory dir) => _directories.Remove(dir));
	        

	        materialMock.Setup(manager => manager.GetAllMaterials()).Returns(_materials);
	        
	        materialMock.Setup(manager => manager.GetMaterialById(It.IsAny<int>()))
	                    .Returns<int>(id => _materials.Find(m => m.Id == id));
	        
	        materialMock.Setup(manager => manager.SaveMaterial(It.IsAny<Material>()))
	                    .Returns<Material>(m => {
		                    var index = _materials.IndexOf(m);
		                    if (index == -1) {
			                    m.Id = _materials.Last().Id + 1;
			                    _materials.Add(m);
			                    return m.Id;
		                    }

		                    _materials[index] = m;
		                    return m.Id;
	                    });
	        
	        materialMock.Setup(manager => manager.DeleteMaterial(It.IsAny<Material>()))
	                    .Callback((Material m) => {
		                    var dir = _directories.Find(d => d.Id == m.DirectoryId);
		                    dir?.Materials.Remove(m);
		                    _materials.Remove(m);
	                    });
		}
	}
}
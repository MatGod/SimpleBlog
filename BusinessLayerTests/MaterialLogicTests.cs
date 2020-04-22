using System;
using System.Linq;
using DomenModels.Models;
using Xunit;

namespace BusinessLayerTests {
	public class MaterialLogicTests : TestBase {
		[Fact]
		public void GetAll() {
			var result = _materialLogic.GetAll();
			Assert.Equal(_materials.Count, result.Count);
		}

		[Fact]
		public void GetById() {
			for (var i = 0; i < 3; ++i) {
				var result = _materialLogic.GetById(i);
				Assert.Equal(_materials.Find(m => m.Id == i), result);
			}
		}

		[Fact]
		public void Save_New() {
			var lastMId = _materials.Last().Id;
			var result = _materialLogic.Save(new Material {Title = "New Material"});
			Assert.Equal(lastMId + 1, result);
			string matTitle = null;
			try {
				matTitle = _materials.Find(m => m.Id == result).Title;
			}
			catch (NullReferenceException) { }

			Assert.Equal("New Material", matTitle);
		}

		[Fact]
		public void Save_Old() {
			var material = new Material {
				Id = 1, Title = "New Material"
			};
			var result = _materialLogic.Save(material);
			Assert.Equal(material.Id, result);
			string dirTitle = null;
			try {
				dirTitle = _materials.Find(m => m.Id == result).Title;
			}
			catch (NullReferenceException) { }

			Assert.Equal("New Material", dirTitle);
		}

		[Fact]
		public void Delete() {
			var material = _materials[0];
			_materialLogic.Delete(_materials[0]);
			Assert.Equal(-1, _materials.IndexOf(material));
		}

		[Fact]
		public void GetDirectoryTitle() {
			Assert.Equal("Directory 1", _materialLogic.GetDirectoryTitle(_materials[0]));
		}

		[Fact]
		public void MoveToDirectory() {
			_materialLogic.MoveToDirectory(_materials[0], _directories[1]);
			Assert.True(_directoryLogic.Contains(_directories[1], _materials[0]));
			Assert.False(_directoryLogic.Contains(_directories[0], _materials[0]));
		}
	}
}
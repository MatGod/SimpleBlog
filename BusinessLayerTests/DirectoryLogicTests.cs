using System;
using System.Linq;
using Castle.Core.Internal;
using DomenModels.Models;
using Xunit;

namespace BusinessLayerTests {
	public class DirectoryLogicTests : TestBase {
		[Fact]
		public void GetAll() {
			var result = _directoryLogic.GetAll();
			Assert.Equal(_directories.Count, result.Count);
		}

		[Fact]
		public void GetById() {
			for (var i = 0; i < 3; ++i) {
				var result = _directoryLogic.GetById(i);
				Assert.Equal(_directories.Find(dir => dir.Id == i), result);
			}
		}

		[Fact]
		public void Save_New() {
			var lastDirId = _directories.Last().Id;
			var result = _directoryLogic.Save(new Directory {Title = "New Directory"});
			Assert.Equal(lastDirId + 1, result);
			string dirTitle = null;
			try {
				dirTitle = _directories.Find(x => x.Id == result).Title;
			}
			catch (NullReferenceException) { }
			
			Assert.Equal("New Directory", dirTitle);
		}

		[Fact]
		public void Save_Old() {
			var dir = new Directory {
				Id = 1, Title = "new Directory"
			};
			dir.Title = "New Directory";
			var result = _directoryLogic.Save(dir);
			Assert.Equal(dir.Id, result);
			string dirTitle = null;
			try {
				dirTitle = _directories.Find(x => x.Id == result).Title;
			}
			catch (NullReferenceException) { }

			Assert.Equal("New Directory", dirTitle);
		}

		[Fact]
		public void Delete() {
			var dir = _directories[0];
			_directoryLogic.Delete(_directories[0]);
			Assert.Equal(-1, _directories.IndexOf(dir));
		}

		[Fact]
		public void Clear_NotEmptyDirectory() {
			var dir = _directories.Find(d => d.Materials.Count != 0);
			_directoryLogic.Clear(dir);
			Assert.Empty(dir.Materials);
		}

		[Fact]
		public void Clear_EmptyDirectory() {
			var dir = _directories.Find(d => d.Materials.IsNullOrEmpty());
			_directoryLogic.Clear(dir);
			Assert.Empty(dir.Materials);
		}

		[Fact]
		public void Contains() {
			Assert.True(_directoryLogic.Contains(_directories[0], _materials[0]));
			Assert.False(_directoryLogic.Contains(_directories[0], _materials[2]));
		}
	}
}
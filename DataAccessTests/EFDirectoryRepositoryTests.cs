using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataAccessLayer.Implementations;
using DataLayer;
using DomenModels.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BusinessLayerTests {
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EFDirectoryRepositoryTests : IDisposable{
		private readonly EFDBContext _context;
		private readonly EFDBContext _emptyContext;
		private readonly EFDirectoryRepository _directoryRepository;
		private readonly EFDirectoryRepository _emptyDirectoryRepository;

		public EFDirectoryRepositoryTests() {
			var optionsBuilder = new DbContextOptionsBuilder<EFDBContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString());
			optionsBuilder.EnableSensitiveDataLogging();
			var options = optionsBuilder.Options;
			
			_context = new EFDBContext(options);
			_context.Database.EnsureCreated();
			_directoryRepository = new EFDirectoryRepository(_context);
			
			TestBaseInitializer.Initialize(_context);
			
			optionsBuilder = new DbContextOptionsBuilder<EFDBContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString());
			optionsBuilder.EnableSensitiveDataLogging();
			options = optionsBuilder.Options;
			
			_emptyContext = new EFDBContext(options);
			_emptyContext.Database.EnsureCreated();
			_emptyDirectoryRepository = new EFDirectoryRepository(_emptyContext);
		}

		[Fact]
		public void GetAllDirectories_BaseHaveData() {
			var result = _directoryRepository.GetAllDirectories();
			var directories = result.ToList();
			Assert.Equal(3, directories.Count);
			Assert.Equal(2, directories[0].Materials.Count);
			Assert.Single(directories[1].Materials);
			Assert.Empty(directories[2].Materials);
		}

		[Fact]
		public void GetAllDirectories_EmptyBase() {
			var result = _emptyDirectoryRepository.GetAllDirectories();
			var directories = result.ToList();
			Assert.Empty(directories);
		}

		[Fact]
		public void GetDirectoryById_CurrentIdInDatabase() {
			var result = _directoryRepository.GetDirectoryById(2);
			Assert.Equal(2, result.Id);
			Assert.Single(result.Materials);
		}

		[Fact]
		public void GetDirectoryById_NoCurrentIdInDatabase() {
			var result = _directoryRepository.GetDirectoryById(4);
			Assert.Null(result);
		}

		[Fact]
		public void SaveDirectory_DirectoryNotInBase() {
			var directory = new Directory {Title = "Saved Directory"};
			var result = _directoryRepository.SaveDirectory(directory);
			Assert.Equal("Saved Directory", _directoryRepository.GetDirectoryById(result).Title);
		}
		
		[Fact]
		public void SaveDirectory_DirectoryInBase() {
			var directory = _directoryRepository.GetDirectoryById(1);
			directory.Title = "Saved Directory";
			var directoryId = _directoryRepository.SaveDirectory(directory);
			Assert.Equal("Saved Directory", _directoryRepository.GetDirectoryById(directoryId).Title);
		}

		[Fact]
		public void DeleteDirectory_DirectoryInBase() {
			var directory = _directoryRepository.GetDirectoryById(1);
			var directoryId = directory.Id;
			Assert.Equal("FirstDirectory", directory.Title);
			_directoryRepository.DeleteDirectory(directory);
			Assert.Null(_directoryRepository.GetDirectoryById(directoryId));
		}
		
		[Fact]
		public void DeleteDirectory_DirectoryNotInBase() {
			var directory = new Directory {Title = "Saved Directory", Id = 0};
			_directoryRepository.DeleteDirectory(directory);
			Assert.Null(_directoryRepository.GetDirectoryById(directory.Id));
		}

		public void Dispose() {
			_context.Database.EnsureDeleted();
			_context.Dispose();
			_emptyContext.Database.EnsureDeleted();
			_emptyContext.Dispose();
		}
	}
}
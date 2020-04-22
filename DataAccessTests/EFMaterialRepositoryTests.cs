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
	public class EFMaterialRepositoryTests : IDisposable{
		private readonly EFDBContext _context;
		private readonly EFDBContext _emptyContext;
		private readonly EFMaterialRepository _materialRepository;
		private readonly EFMaterialRepository _emptyMaterialRepository;
		private readonly EFDirectoryRepository _directoryRepository;

		public EFMaterialRepositoryTests() {
			var optionsBuilder = new DbContextOptionsBuilder<EFDBContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString());
			optionsBuilder.EnableSensitiveDataLogging();
			var options = optionsBuilder.Options;
			
			_context = new EFDBContext(options);
			_context.Database.EnsureCreated();
			_materialRepository = new EFMaterialRepository(_context);
			_directoryRepository = new EFDirectoryRepository(_context);
			
			TestBaseInitializer.Initialize(_context);
			
			optionsBuilder = new DbContextOptionsBuilder<EFDBContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString());
			optionsBuilder.EnableSensitiveDataLogging();
			options = optionsBuilder.Options;
			
			_emptyContext = new EFDBContext(options);
			_emptyContext.Database.EnsureCreated();
			_emptyMaterialRepository = new EFMaterialRepository(_emptyContext);
		}

		[Fact]
		public void GetAllMaterials_BaseHaveData() {
			var result = _materialRepository.GetAllMaterials();
			var materials = result.ToList();
			Assert.Equal(3, materials.Count);
		}

		[Fact]
		public void GetAllMaterials_EmptyBase() {
			var result = _emptyMaterialRepository.GetAllMaterials();
			var materials = result.ToList();
			Assert.Empty(materials);
		}

		[Fact]
		public void GetMaterialById_CurrentIdInDatabase() {
			var result = _materialRepository.GetMaterialById(2);
			Assert.Equal(2, result.Id);
		}

		[Fact]
		public void GetMaterialById_NoCurrentIdInDatabase() {
			var result = _materialRepository.GetMaterialById(0);
			Assert.Null(result);
		}

		[Fact]
		public void SaveMaterial_MaterialNotInBase() {
			var material = new Material {Title = "Saved Material"};
			var result = _materialRepository.SaveMaterial(material);
			Assert.Equal("Saved Material", _materialRepository.GetMaterialById(result).Title);
		}
		
		[Fact]
		public void SaveMaterial_MaterialInBase() {
			var material = _materialRepository.GetMaterialById(1);
			material.Title = "Saved Material";
			var materialId = _materialRepository.SaveMaterial(material);
			Assert.Equal("Saved Material", _materialRepository.GetMaterialById(materialId).Title);
		}

		[Fact]
		public void DeleteMaterial_MaterialInBase() {
			var material = _materialRepository.GetMaterialById(1);
			var materialId = material.Id;
			_materialRepository.DeleteMaterial(material);
			Assert.Null(_materialRepository.GetMaterialById(materialId));
		}
		
		[Fact]
		public void DeleteMaterial_MaterialNotInBase() {
			var material = new Material {Title = "Saved Material"};
			_materialRepository.DeleteMaterial(material);
			Assert.Null(_materialRepository.GetMaterialById(material.Id));
		}

		public void Dispose() {
			_context.Database.EnsureDeleted();
			_context.Dispose();
			_emptyContext.Database.EnsureDeleted();
			_emptyContext.Dispose();
		}
	}
}
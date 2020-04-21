using BusinessLayer;
using DataLayer.Enums;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer;
using PresentationLayer.Models;

namespace WebApplication.Controllers {
	public class PageController : Controller {
		private readonly ServiceManager _serviceManager;

		public PageController(DataManager dataManager) {
			_serviceManager = new ServiceManager(dataManager);
		}

		// GET
		public IActionResult Index(int pageId, PageEnums.PageType pageType) {
			PageViewModel viewModel = pageType switch {
				PageEnums.PageType.Directory => _serviceManager.DirectoryService.DirectoryDBToViewModelById(pageId),
				PageEnums.PageType.Material => _serviceManager.MaterialService.MaterialDBModelToView(pageId),
				_ => null
			};

			ViewBag.PageType = pageType;
			return View(viewModel);
		}

		[HttpGet]
		public IActionResult PageEditor(int pageId, PageEnums.PageType pageType, int directoryId) {
			PageEditModel editModel = null;
			switch (pageType) {
				case PageEnums.PageType.Directory: {
					editModel = _serviceManager.DirectoryService.GetDirectoryEditModel(pageId);
					break;
				}
				case PageEnums.PageType.Material: {
					editModel = _serviceManager.MaterialService.GetMaterialEditModel(pageId, directoryId);
					break;
				}
			}

			ViewBag.PageType = pageType;
			return View(editModel);
		}

		[HttpPost]
		public IActionResult SaveDirectory(DirectoryEditModel model) {
			var directoryId = _serviceManager.DirectoryService.SaveDirectoryEditModelToDb(model).Directory.Id;
			return RedirectToAction("Index", "Page",
			                        new {pageId = directoryId, pageType = PageEnums.PageType.Directory});
		}

		[HttpPost]
		public IActionResult DeleteDirectory(DirectoryEditModel model) {
			_serviceManager.DirectoryService.DeleteDirectoryEditModelFromDb(model);
			return RedirectToAction("Index", "Home");
		}
		
		[HttpPost]
		public IActionResult SaveMaterial(MaterialEditModel model) {
			var materialId = _serviceManager.MaterialService.SaveMaterialEditModelToDb(model).Material.Id;
			return RedirectToAction("Index", "Page",
			                        new {pageId = materialId, pageType = PageEnums.PageType.Material});
		}

		[HttpPost]
		public IActionResult DeleteMaterial(MaterialEditModel model) {
			_serviceManager.MaterialService.DeleteMaterialEditModelFromDb(model);
			return RedirectToAction("Index", "Home");
		}
	}
}
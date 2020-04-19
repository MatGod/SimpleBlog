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
		public IActionResult PageEditor(int pageId, PageEnums.PageType pageType) {
			PageEditModel editModel = null;
			switch (pageType) {
				case PageEnums.PageType.Directory: {
					editModel = _serviceManager.DirectoryService.GetDirectoryEditModel(pageId);
					break;
				}
				case PageEnums.PageType.Material: {
					editModel = _serviceManager.MaterialService.GetMaterialEditModel(pageId);
					break;
				}
			}

			ViewBag.PageType = pageType;
			return View(editModel);
		}

		[HttpPost]
		public IActionResult SaveDirectory(DirectoryEditModel model) {
			_serviceManager.DirectoryService.SaveDirectoryEditModelToDb(model);
			return RedirectToAction("Index", "Page",
			                        new {pageId = model.Id, pageType = PageEnums.PageType.Directory});
		}
		
		[HttpPost]
		public IActionResult SaveMaterial(MaterialEditModel model) {
			_serviceManager.MaterialService.SaveMaterialEditModelToDb(model);
			return RedirectToAction("Index", "Page",
			                        new {pageId = model.Id, pageType = PageEnums.PageType.Material});
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;

using FilesStorage.BLL.Interfaces;
using FilesStorage.Entities.DTOs;
using FilesStorage.Entities.Mappers;
using FilesStorage.Entities.ViewModels;

namespace FilesStorage.PL.Web.Controllers
{
    [Authorize]
    public class MyFilesController : Controller
    {
        private readonly IFilesLogic _filesLogic;
        private readonly ITagsLogic _tagsLogic;
        private readonly IPLMapper _mapper;

        private const string defaultController = "MyFiles";
        private const string defaultAction = "Index";

        public MyFilesController(IFilesLogic filesLogic, IPLMapper mapper, ITagsLogic tagsLogic)
        {
            _filesLogic = filesLogic;
            _tagsLogic = tagsLogic;
            _mapper = mapper;
        }

        public MyFilesController() { }

        private IEnumerable<FileView> GetUserFiles()
        {
            var files = _filesLogic.GetAllUserFiles(User.Identity.Name);
            var filesViews = _mapper.Map<IEnumerable<FileView>, IEnumerable<FileDto>>(files);
            return filesViews;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var filesViews = GetUserFiles();
            var filesModel = new FilesViewModel() { filesViews = filesViews };
            return View(filesModel);
        }

        [HttpPost]
        public ActionResult Index(FilesViewModel filesView)
        {
            if(!String.IsNullOrEmpty(filesView.SearchString))
            {
                var searchQuery = new FilesSearchDto() 
                { 
                    LoginName = User.Identity.Name,
                    SearchString = filesView.SearchString
                };
                try
                {
                    var found = _filesLogic.SearchUserFiles(searchQuery);
                    var foundView = _mapper.Map<IEnumerable<FileView>, IEnumerable<FileDto>>(found);
                    filesView.filesViews = foundView;
                }
                catch (ArgumentException)
                {
                    ViewBag.ErrorMessage = "The search query format is incorect";
                    filesView.filesViews = new List<FileView>();
                }                
                
            }
            else
            {
                filesView.filesViews = GetUserFiles();
            }            
            return View(filesView);
        }

        private IEnumerable<TagsView> GetUserTags()
        {
            var userTags = _tagsLogic.GetAllUserTags(User.Identity.Name);
            var tagsView = _mapper.Map<IEnumerable<TagsView>, IEnumerable<TagDto>>(userTags);
            return tagsView;
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.UserTags = GetUserTags();
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddFileView createFileView, int[] selectedIds)
        {
            if (ModelState.IsValid)
            {                                
                var dto = _mapper.Map<FileDto, AddFileView>(createFileView);
                if(selectedIds != null && selectedIds.Any()) 
                {
                    dto.Tags = selectedIds.Select(id => new TagDto { Id = id }).ToList();
                }                                
                _filesLogic.Add(dto, User.Identity.Name);
                return RedirectToAction(defaultAction, defaultController);
            }
            ViewBag.UserTags = GetUserTags();
            return View(createFileView);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if(!id.HasValue)
            {
                return HttpNotFound();
            }
            _filesLogic.DeleteFile(id.Value);
            return RedirectToAction(defaultAction, defaultController);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            var file = _filesLogic.FindFileById(id.Value);
            var fileView = _mapper.Map<CreateEditFileView, FileDto>(file);
            ViewBag.UserTags = GetUserTags();
            return View(fileView);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditFileView editFileView)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<FileDto, CreateEditFileView>(editFileView);
                _filesLogic.EditFile(dto);
                return RedirectToAction(defaultAction, defaultController);
            }
            ViewBag.UserTags = GetUserTags();
            return View(editFileView);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                var file = _filesLogic.FindFileById(id.Value);
                var fileView = _mapper.Map<CreateEditFileView, FileDto>(file);
                return View(fileView);
            }
            return HttpNotFound();
        }

    }
}
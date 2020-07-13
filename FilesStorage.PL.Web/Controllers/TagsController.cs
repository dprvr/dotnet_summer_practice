using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Antlr.Runtime.Tree;

using FilesStorage.BLL.Interfaces;
using FilesStorage.Entities.DTOs;
using FilesStorage.Entities.Mappers;
using FilesStorage.Entities.ViewModels;

namespace FilesStorage.PL.Web.Controllers
{
    [Authorize]
    public class TagsController : Controller
    {
        private readonly ITagsLogic _tagsLogic;
        private readonly IPLMapper _mapper;

        public TagsController() { }

        public TagsController(ITagsLogic tagsLogic, IPLMapper mapper)
        {
            _tagsLogic = tagsLogic;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var found = _tagsLogic.GetAllUserTags(User.Identity.Name);
            var views = _mapper.Map<IEnumerable<TagsView>, IEnumerable<TagDto>>(found);
            return View(views);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(TagsView view)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<TagDto, TagsView>(view);
                _tagsLogic.AddTag(dto, User.Identity.Name);
                RedirectToAction("Index", "Tags");
            }
            return View(view);
        }

        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                _tagsLogic.DeleteTagById(id.Value);
                return RedirectToAction("Index", "Tags");
            }
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                var found = _tagsLogic.FindTagById(id.Value);
                var view = _mapper.Map<TagsView, TagDto>(found);
                return View(view);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Edit(TagsView view)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<TagDto, TagsView>(view);
                _tagsLogic.EditTag(dto);
                RedirectToAction("Index, Tags");
            }
            return View(view);
        }

    }
}
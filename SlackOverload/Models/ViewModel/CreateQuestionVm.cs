using Microsoft.AspNetCore.Mvc.Rendering;
using SlackOverload.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace SlackOverload.Models.ViewModel
{
    public class CreateQuestionVm
    {
        public List<SelectListItem> Tags { get; } = new List<SelectListItem>();


        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name ="Question")]
        public string Description { get; set; }


        [Display(Name = "Tags")]
        public string TagId { get; set; }

        public Tags? Tag { get; set; }


        public CreateQuestionVm(HashSet<Tags> tags)
        {
            foreach (Tags t in tags)
            {
                Tags.Add(new SelectListItem(t.TagName, t.Id.ToString()));
            }
        }

        public CreateQuestionVm() { }
    }
}

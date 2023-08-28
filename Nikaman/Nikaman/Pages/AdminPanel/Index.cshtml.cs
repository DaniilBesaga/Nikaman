using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nikaman.DataContext;
using Nikaman.Models;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

namespace Nikaman.Pages.AdminPanel
{
    public class IndexModel : PageModel
    {
        ExperienceDataContext db { get; set; }
        public List<Exp> works { get; set; }
        [BindProperty]
        public Exp E { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.Get("token") == null)
            {
                return RedirectToPage("AdminPanelAccess");
            }
            using (db = new ExperienceDataContext())
            {
                works = db.Exps.ToList();
            }

            return Page();
        }
        public void OnPutUpdate([FromForm]UpdateModel model)
        {
            using(db = new ExperienceDataContext())
            {
                Exp exp = db.Exps.FirstOrDefault(x => x.Id == model.Id);
                if (model.Video != null)
                {
                    Stream? stream = model.Video.OpenReadStream();
                    var reader = new BinaryReader(stream);
                    exp.Video = reader.ReadBytes((int)stream.Length);
                }

                if (model.Preview != null)
                {
                    Stream? stream1 = model.Preview.OpenReadStream();
                    var reader1 = new BinaryReader(stream1);
                    exp.Preview = reader1.ReadBytes((int)stream1.Length);
                }
                exp.Title = model.Title == null ? exp.Title : model.Title;
                exp.Likes_TT = model.LikesTT==null? exp.Likes_TT : (Regex.IsMatch(model.LikesTT, @"\d") ? model.LikesTT : "-");
                exp.Likes_YT = model.LikesYT == null ? exp.Likes_YT : (Regex.IsMatch(model.LikesTT, @"\d") ? model.LikesYT : "-");
                exp.Likes_INST = model.LikesInst == null ? exp.Likes_INST : (Regex.IsMatch(model.LikesTT, @"\d") ? model.LikesInst : "-");
                model.ViewsTT = model.ViewsTT == null ? null : (Regex.IsMatch(model.LikesTT, @"\d") ? model.ViewsTT : "-");
                model.ViewsYT = model.ViewsYT == null ? null : (Regex.IsMatch(model.LikesTT, @"\d") ? model.ViewsYT : "-");
                model.ViewsInst = model.ViewsInst == null ? null : (Regex.IsMatch(model.LikesTT, @"\d") ? model.ViewsInst : "-");
                db.SaveChanges();
                works = db.Exps.ToList();
            }
            
        }

        public void OnPostAddWork([FromForm] UpdateModel model)
        {

        }

        [BindProperties(SupportsGet = true)]
        public class UpdateModel
        {
            public int Id { get; set; }
            public string? Title { get; set; }
            public string? LikesTT { get; set; }
            public string? LikesYT { get; set; }
            public string? LikesInst { get; set; }
            public string? ViewsTT { get; set; }
            public string? ViewsYT { get; set; }
            public string? ViewsInst { get; set; }
            public IFormFile? Video { get; set; }
            public IFormFile? Preview { get; set; }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nikaman.DataContext;
using Nikaman.Models;
using System.Security.Cryptography.Xml;
using Microsoft.Extensions.Configuration.UserSecrets;
namespace Nikaman.Pages.MyWorks
{
    
    public class IndexModel : PageModel
    {
        static ExperienceDataContext db { get; set; }
        public List<Exp> exps { get; set; }
        public void OnGet()
        {
            AddToDB();
            
        }
        public void AddToDB()
        {
            
            using(ExperienceDataContext  db = new ExperienceDataContext())
            {
                if(db.Exps.ToList().Count==0)
                {
                    string img = "C:\\Users\\1\\Downloads\\deku.png";
                    string vid = "C:\\Users\\1\\Downloads\\deku.mp4";
                    
                    Exp E1 = new Exp { Title = "Deku", Views="600k", Likes=" 20k+" };
                    E1.Video = System.IO.File.ReadAllBytes(vid);
                    E1.Preview = System.IO.File.ReadAllBytes(img);


                    img = "C:\\Users\\1\\Downloads\\robin.png";
                    vid = "C:\\Users\\1\\Downloads\\robin.mp4";
                    Exp E2 = new Exp { Title = "Niko Robin", Views = "800k", Likes = " 40k+" };
                    E2.Video = System.IO.File.ReadAllBytes(vid);
                    E2.Preview = System.IO.File.ReadAllBytes(img);


                    img = "C:\\Users\\1\\Downloads\\luffytopjoyboy.png";
                    vid = "C:\\Users\\1\\Downloads\\luffytopjoyboy.mp4";
                    Exp E3 = new Exp { Title = "Luffy Joy Boy", Views = "1M+", Likes = " 80k+" };
                    E3.Video = System.IO.File.ReadAllBytes(vid);
                    E3.Preview = System.IO.File.ReadAllBytes(img);
                    db.Exps.AddRange(E1, E2, E3);
                    db.SaveChanges();
                }
                if (db.Exps.ToList().Count == 3)
                {
                    string img = "C:\\Users\\1\\Downloads\\luffydofl.png";
                    string vid = "C:\\Users\\1\\Downloads\\luffydofl.mp4";

                    Exp E1 = new Exp { Title = "Luffy VS Doflamingo", Views = "300k", Likes = " 18k+" };
                    E1.Video = System.IO.File.ReadAllBytes(vid);
                    E1.Preview = System.IO.File.ReadAllBytes(img);


                    img = "C:\\Users\\1\\Downloads\\snakeman.png";
                    vid = "C:\\Users\\1\\Downloads\\snakeman.mp4";
                    Exp E2 = new Exp { Title = "Snakeman", Views = "500k", Likes = " 35k+" };
                    E2.Video = System.IO.File.ReadAllBytes(vid);
                    E2.Preview = System.IO.File.ReadAllBytes(img);


                    img = "C:\\Users\\1\\Downloads\\zoro.png";
                    vid = "C:\\Users\\1\\Downloads\\zoro.mp4";
                    Exp E3 = new Exp { Title = "Zoro was forgotten!", Views = "900k+", Likes = " 55k+" };
                    E3.Video = System.IO.File.ReadAllBytes(vid);
                    E3.Preview = System.IO.File.ReadAllBytes(img);
                    db.Exps.AddRange(E1, E2, E3);
                    db.SaveChanges();
                }
                exps = db.Exps.ToList();
            }
        }
        
        public IActionResult OnGetVideo(int index)
        {
            using (ExperienceDataContext db = new ExperienceDataContext())
            {
                Exp? E = db.Exps.FirstOrDefault(a => a.Id == index+1);
                return new OkObjectResult(Convert.ToBase64String(E.Video));
            }
        }

        public IActionResult OnGetNewPhoto(int index, string queue)
        {
            using (ExperienceDataContext db = new ExperienceDataContext())
            {
                Exp? E = db.Exps.FirstOrDefault(a => a.Id == index + 1);
                if(queue=="main")
                {
                    NewPhoto N = new NewPhoto();
                    N.Title = E.Title;
                    N.Likes = E.Likes;
                    N.Views = E.Views;
                    N.Image = Convert.ToBase64String(E.Preview);
                    return new OkObjectResult(N);
                }
                else
                {
                    return new OkObjectResult(Convert.ToBase64String(E.Preview));
                }
            }
        }
        public class NewPhoto
        {
            public string? Title { get; set; }
            public string? Views { get; set; }
            public string? Likes { get; set; }
            public string? Image { get; set; }
        }
    }
}

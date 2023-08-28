using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Nikaman.Pages.OfferForm
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
    public class FormOffer
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Please, enter your email adress")]
        [DataType (DataType.EmailAddress)]
        public string? Email { get; set; }
        public string? Instagram { get; set; }
        [Required(ErrorMessage = "Please, enter the type of work")]
        public bool Type { get; set; }
        [Required(ErrorMessage = "Please, add some information")]
        public string? AddInfo { get; set; }
        [Required(ErrorMessage = "Please, write a link to files")]
        public string? Files { get; set; }
        [Required(ErrorMessage = "Please, agree with proceding of personal data")]
        public bool PersonalData { get; set; }
    }
}

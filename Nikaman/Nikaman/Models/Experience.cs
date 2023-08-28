using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nikaman.Models
{
    public class Exp
    {
        public int Id { get; set; }
        public string? Title { get; set; } 
        public byte[]? Video { get; set; }
        public string? Likes { get; set; }
        public string? Likes_TT { get; set; }
        public string? Likes_YT { get; set; }
        public string? Likes_INST { get; set; }
        public string? Views { get; set; }
        public byte[]? Preview { get; set; }
    }
}

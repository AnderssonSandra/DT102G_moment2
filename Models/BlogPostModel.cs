using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DT102G_moment2.Models
{
    public class BlogPostModel
    {
        [Required(ErrorMessage = "Du måste fylla i namn")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Du måster välja vilken nivå du är på")]
        public string Level { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Du måste fylla i titel")]
        [MaxLength(50, ErrorMessage = "Titeln kan max vara 50 tecken långt")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Du måste fylla i text i textfältet")]
        [MinLength(5, ErrorMessage = "Texten måste vara över 5 tecken lång.")]
        public string Text { get; set; }

        [CheckboxMustBeTrue(ErrorMessage = "Du måste klicka i rutan för att publicera inlägget")]
        public bool Checkbox { get; set; }

        public class CheckboxMustBeTrue : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                return value is bool && (bool)value;
            }
        }
        public BlogPostModel()
        {

        }
    }
}

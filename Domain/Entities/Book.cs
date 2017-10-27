using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class Book
    {
        [HiddenInput(DisplayValue =false)]
        [Display(Name = "ID")]
        public int BookId { get; set; }
        
        [Display(Name="Название")]
        [Required(ErrorMessage ="Пожалуйста, введите название книги")]
        public string Name { get; set; }

        [Display(Name = "Автор")]
        [Required(ErrorMessage = "Пожалуйста, укажите имя автора")]
        public string Author { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Пожалуйста, введите описание книги")]
        public string Description { get; set; }

        [Display(Name = "Жанр")]
        [Required(ErrorMessage = "Пожалуйста, введите жанр произведения")]
        public string Genre { get; set; }

        [Display(Name = "Цена (грн)")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение цены")]
        public decimal Price { get; set; }
    }
}

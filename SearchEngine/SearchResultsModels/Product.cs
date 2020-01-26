using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngine.SearchResultsModels
{
    public class Product
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Sklep")]
        public string Provider { get; set; }

        [Display(Name = "Kategoria")]
        public string Category { get; set; }

        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Marka")]
        public string Mark { get; set; }

        [Required]
        [Display(Name = "Złote")]
        public Nullable<int> PriceZl { get; set; }

        [Required]
        [Display(Name = "Grosze")]
        public Nullable<int> PriceGr { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Ilość")]
        public Nullable<int> Quantity { get; set; }

        [Required]
        [Display(Name = "Link")]
        public string Url { get; set; }

        [Required]
        [Display(Name = "Oferta Sprawdzona")]
        public DateTime DownloadDate { get; set; }

        public Product()
        {

        }

        public Product(string provider, string category, string name, string mark, int? zl, int? gr)
        {
            Provider = provider;
            Category = category;
            Name = name;
            Mark = mark;
            PriceZl = zl;
            PriceGr = gr;
            DownloadDate = DateTime.Now;
        }
    }
}

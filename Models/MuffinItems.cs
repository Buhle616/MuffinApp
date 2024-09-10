using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace MuffinApp.Models
{
    public class MuffinItems
    {
        [Key]
        public int ItemNo { get; set; }
        public string MuffinName {  get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double DiscountRate { get; set; }
        public double VatRate { get; set; }
    }
}
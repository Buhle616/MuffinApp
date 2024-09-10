using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MuffinApp.Models
{
    public class Orders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderNo { get; set; }
        public int ItemNo { get; set; }
        public virtual MuffinItems MuffinItems { get; set; }
        public string CustomerName { get; set; }
        public string UniversityRole { get; set; }
        public int NumMuffins { get; set; }
        public double BasicCost { get; set; }
        public double DiscountAmt { get; set; }
        public double VatAmt { get; set; }
        public double TotalCost { get; set; }

        public double Pullprice()
        {
            MuffinDbcontext db = new MuffinDbcontext();
            var bh = (from t in db.muffinitem
                      where t.ItemNo == ItemNo
                      select t.Price).Single();
            return bh;
        }
        public double PullDiscountRate()
        {
            MuffinDbcontext db = new MuffinDbcontext();
            var bh = (from t in db.muffinitem
                      where t.ItemNo == ItemNo
                      select t.DiscountRate).Single();
            return bh;
        }
        public double PullVatRate()
        {
            MuffinDbcontext db = new MuffinDbcontext();
            var bh = (from t in db.muffinitem
                      where t.ItemNo == ItemNo
                      select t.VatRate).Single();
            return bh;
        }

        public double CalBasicCost()
        {
            return Pullprice() * NumMuffins;
        }

        public double CalcDiscount()
        {
            if (UniversityRole == "Staff" && NumMuffins >= 5)
            {
                return (((PullDiscountRate() * 0.25) + PullDiscountRate()) / 100) * CalBasicCost();
            }
            else if (UniversityRole == "Student" && NumMuffins >= 3)
            {
                return (((PullDiscountRate() * 0.5) + PullDiscountRate()) / 100) * CalBasicCost();
            }
            else
            {
                return (PullDiscountRate() / 100) * CalBasicCost();
            }
        }

        public double CalVat()
        {
            return CalBasicCost() * (PullVatRate() / 100);
        }

        public double CalcTotal()
        {
            return (CalBasicCost() - CalcDiscount()) + CalVat();
        }
    }
}
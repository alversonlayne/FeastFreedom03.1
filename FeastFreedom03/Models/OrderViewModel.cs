using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeastFreedom03.Views.ViewModels
{
    public class OrderViewModel
    {
        [ Key, Required]
        public int ItemID { get; set; }


        [Display(Name="Item Name")]
        public string ItemName { get; set; }

        [Display(Name="Item Price")]
        public decimal ItemPrice { get; set; }

        public bool isVeg { get; set; }

        public int Quantity { get; set; }

        [Display(Name="Kitchen")]
        public string KitchenName { get; set; }

        public int KitchenID { get; set; }
        public int OrderID { get; set; }

        public int DetailsID { get; set; }

       public List<OrderViewModel> orderViewModelList { get; set; }

    }

}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.PO
{
    public class ParcelAdd : NotifyPropertyChangedBase, IDataErrorInfo
    {
        private int customerSender;
        [Required(ErrorMessage = "required")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int CustomerSender
        {
            get => customerSender;
            set=>Set(ref customerSender, value);
        }
        private int customerReceives;
        [Required(ErrorMessage = "required")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int CustomerReceives
        {
            get => customerReceives;
            set => Set(ref customerReceives, value);
        }
        private WeightCategories? weight;
        [Required(ErrorMessage = "required")]
        public WeightCategories? Weight
        {
            get => weight;
            set => Set(ref weight, value);
        }
        private Priorities? piority;
        [Required(ErrorMessage = "required")]
        public Priorities? Piority
        {
            get => piority;
            set => Set(ref piority, value);
        }
        public string Error => Validation.ErorrCheck(this);
        public string this[string columnName] => Validation.PropError(columnName, this);

        
        public override string ToString()
        {
            return this.ToStringProperties();
        }

    }
}



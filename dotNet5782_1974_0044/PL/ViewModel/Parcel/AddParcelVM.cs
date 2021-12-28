﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PL.PO;

namespace PL
{
   public class AddParcelVM
    {
       public ParcelAdd parcel { set; get; }
       public RelayCommand AddParcelCommand { get; set; }
        public List<int> customers { get; set; }
        public Array piorities { get; set; }
        public Array Weight { get; set; }
        public AddParcelVM()
        {
            parcel = new();
            customers = new CustomerHandler().GetCustomers().Select(customer => customer.Id).ToList();
            AddParcelCommand = new(Add, param => parcel.Error == null);
            piorities = Enum.GetValues(typeof(Priorities));
            Weight = Enum.GetValues(typeof(WeightCategories));

        }
        public void Add(object param)
        {
            try
            {
                new ParcelHandler().AddParcel(parcel);
                MessageBox.Show("succses");
                DelegateVM.Parcel();
                DelegateVM.Customer();
            }
            catch(KeyNotFoundException)
            {
                MessageBox.Show("sender or reciver not exist");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PL.PO;

namespace PL.View
{
  public  class AddStationVM
    {
        public StationAdd station { set; get; }
        public RelayCommand AddStationCommand { get; set; }
        public AddStationVM()
        {
            station = new();
            AddStationCommand = new(Add, param => station.Error == null);
        }
        public void Add(object param)
        {
            try
            {
                new StationHundler().AddStation(station);
                MessageBox.Show("seccess");
            }
            catch(BO.ThereIsAnObjectWithTheSameKeyInTheListException)
            {
                MessageBox.Show("Id has already exsist");
            }
        }

    }
}

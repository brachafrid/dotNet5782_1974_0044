using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : UserControl
    {
        /// <summary>
        /// add drone
        /// </summary>
        public Drone()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// input drone
        /// </summary>
        /// <param name="droneToList"></param>
        public Drone(DroneToList droneToList)
        {
            InitializeComponent();
        }
    }
}

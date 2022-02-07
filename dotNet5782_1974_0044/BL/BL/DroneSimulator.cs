using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DLApi;
using BLApi;
using System.Threading;
using System.Runtime.CompilerServices;

namespace BL
{
   internal class DroneSimulator
    {
        enum Maintenance { Starting, Going, Charging }
        BL bl { set; get; }
        IDal dal { set; get; }
        Drone drone { set; get; }
        private const int DELAY = 500;
        private Maintenance maintenance = Maintenance.Starting;
        public DroneSimulator(int id)
        {
            try
            {
                 bl = BL.Instance;
                 dal = bl.dal;
                 drone = bl.GetDrone(id);
                    
            }
            catch (KeyNotFoundException ex)
            {

                throw new KeyNotFoundException("",ex);
            }
        }
        private void AvailbleDrone()
        {
            lock(bl)
            {
                try
                {
                    bl.AssingParcelToDrone(drone.Id);
                }
                catch(KeyNotFoundException)
                {
                    if (drone.BattaryMode == 100)
                        return;
                    drone.DroneState = DroneState.MAINTENANCE;
                    maintenance = Maintenance.Starting;
                }
            }
        }
        private void MaintenanceDrone()
        {
            switch (maintenance)
            {
                case Maintenance.Starting:
                    {

                        break;
                    }
                    
                case Maintenance.Going:
                    break;
                case Maintenance.Charging:
                    break;
                default:
                    break;
            }
            lock (bl)
            {

            }
        }
        private static bool sleepDelayTime()
        {
            try { Thread.Sleep(DELAY); } catch (ThreadInterruptedException) { return false; }
            return true;
        }

    }
}

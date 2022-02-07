using BO;
using DLApi;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BL
{
    internal class DroneSimulator
    {
        enum Maintenance { Starting, Going, Charging }
        BL bl { set; get; }
        IDal Dal { set; get; }
        Drone Drone { set; get; }
        private const int DELAY = 500;
        private Maintenance maintenance = Maintenance.Starting;
        public DroneSimulator(int id, BL BL, Action update)
        {
            try
            {
                bl = BL;
                Dal = bl.dal;
                Drone = bl.GetDrone(id);
            }
            catch (KeyNotFoundException ex)
            {

                throw new KeyNotFoundException("", ex);
            }
        }
        private void AvailbleDrone()
        {
            lock (bl)
            {
                try
                {
                    bl.AssingParcelToDrone(Drone.Id);
                }
                catch (NotExsistSutibleParcelException)
                {
                    if (Drone.BattaryMode == 100)
                        return;
                    Drone.DroneState = DroneState.MAINTENANCE;
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
                        // למצוא תחנה 
                        break;
                    }

                case Maintenance.Going:
                    {
                        //להתחיל ללכת לתחנה
                        break;
                    }

                case Maintenance.Charging:
                    {
                        //   להטעין
                        break;
                    }

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

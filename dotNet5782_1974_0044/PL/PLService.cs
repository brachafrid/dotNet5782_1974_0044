using BLApi;
using PL.PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public static class PLService
    {
        private static IBL ibal = BLFactory.GetBL();

        /// <summary>
        /// Get administor passsword
        /// </summary>
        /// <returns>task of string</returns>
        public async static Task<string> GetAdministorPasssword()
        {
            TaskCompletionSource<string> completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => e.Result = ibal.GetAdministorPasssword();
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult(e.Result as string);
            workerPl.RunWorkerAsync();
            return await completedTask.Task;
        }

        #region customer 

        /// <summary>
        /// Add customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>task</returns>
        public static async Task AddCustomer(CustomerAdd customer)
        {

            var workerCompleted = new TaskCompletionSource();

            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, args) => ibal.AddCustomer(PlServiceConvert.ConvertAddCustomer(customer));
            workerPl.RunWorkerCompleted += (sender, e) => workerCompleted.SetResult();
            workerPl.RunWorkerAsync();

            await workerCompleted.Task;
        }

        /// <summary>
        /// Get customer
        /// </summary>
        /// <param name="id">id of customer</param>
        /// <returns>Task of customer</returns>
        public static async Task<Customer> GetCustomer(int id)
        {
            var taskCompletionSource = new TaskCompletionSource<Customer>();
            try
            {
                BackgroundWorker workerPl = new();
                workerPl.DoWork += (sender, e) => e.Result = PlServiceConvert.ConvertCustomer(ibal.GetCustomer(id));
                workerPl.RunWorkerCompleted += (sender, e) => taskCompletionSource.SetResult(e.Result as Customer);
                workerPl.RunWorkerAsync();
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return await taskCompletionSource.Task;
        }

        /// <summary>
        /// Get customers
        /// </summary>
        /// <returns>Task of IEnumerable of CustomerToList</returns>
        public static async Task<IEnumerable<CustomerToList>> GetCustomers()
        {
            var taskCompletionSource = new TaskCompletionSource<IEnumerable<CustomerToList>>();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => e.Result = ibal.GetActiveCustomers().Select(customer => PlServiceConvert.ConvertCustomerToList(customer));
            workerPl.RunWorkerCompleted += (sender, e) => taskCompletionSource.SetResult((IEnumerable<CustomerToList>)e.Result);
            workerPl.RunWorkerAsync();
            return await taskCompletionSource.Task;
        }

        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="id">id of customer</param>
        /// <param name="name">name of customer</param>
        /// <param name="phone">phone of customer</param>
        /// <returns>task</returns>
        public static async Task UpdateCustomer(int id, string name, string phone)
        {
            var workerCompleted = new TaskCompletionSource();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => ibal.UpdateCustomer(id, name, phone);
            workerPl.RunWorkerCompleted += (sender, e) => workerCompleted.SetResult();
            workerPl.RunWorkerAsync();
            await workerCompleted.Task;
        }

        /// <summary>
        /// Delete customer
        /// </summary>
        /// <param name="id">id of cstomer</param>
        /// <returns>task</returns>
        public static async Task DeleteCustomer(int id)
        {
            TaskCompletionSource taskCompletion = new();
            BackgroundWorker worker = new();
            worker.DoWork += (sender, e) => ibal.DeleteCustomer(id);
            worker.RunWorkerCompleted += (sender, e) => taskCompletion.SetResult();
            worker.RunWorkerAsync();
            await taskCompletion.Task;
        }

        /// <summary>
        /// Check if the customer is not active
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>if the customer is not active</returns>
        public static async Task<bool> IsNotActiveCustomer(int id)
        {
            TaskCompletionSource<bool> completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => e.Result = ibal.IsNotActiveCustomer(id);
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult((bool)e.Result);
            workerPl.RunWorkerAsync();
            return await completedTask.Task;
        }
        #endregion

        #region station

        /// <summary>
        /// Add station
        /// </summary>
        /// <param name="station">station</param>
        /// <returns>task</returns>
        public static async Task AddStation(StationAdd station)
        {
            TaskCompletionSource completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => ibal.AddStation(PlServiceConvert.ConverBackStationAdd(station));
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult();
            workerPl.RunWorkerAsync();
            await completedTask.Task;
        }

        /// <summary>
        /// Update station
        /// </summary>
        /// <param name="id">id of station</param>
        /// <param name="name">name of station</param>
        /// <param name="chargeSlots">charge slots of station</param>
        /// <returns>task</returns>
        public static async Task UpdateStation(int id, string name, int chargeSlots)
        {
            TaskCompletionSource completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => ibal.UpdateStation(id, name, chargeSlots);
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult();
            workerPl.RunWorkerAsync();
            await completedTask.Task;
        }

        /// <summary>
        /// Get station
        /// </summary>
        /// <param name="id">id of station</param>
        /// <returns>task of station</returns>
        public static async Task<Station> GetStation(int id)
        {
            TaskCompletionSource<Station> completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => e.Result = PlServiceConvert.ConverterStation(ibal.GetStation(id));
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult(e.Result as Station);
            workerPl.RunWorkerAsync();
            return await completedTask.Task;
        }

        /// <summary>
        /// Delete station
        /// </summary>
        /// <param name="id">id of station</param>
        /// <returns>task</returns>
        public static async Task DeleteStation(int id)
        {
            TaskCompletionSource taskCompletion = new();
            BackgroundWorker worker = new();
            worker.DoWork += (sender, e) => ibal.DeleteStation(id);
            worker.RunWorkerCompleted += (sender, e) => taskCompletion.SetResult();
            await taskCompletion.Task;
        }

        /// <summary>
        /// Check if station is not active
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>if station is not active</returns>
        public static async Task<bool> IsNotActiveStation(int id)
        {
            TaskCompletionSource<bool> completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => e.Result = ibal.IsNotActiveStation(id);
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult((bool)e.Result);
            workerPl.RunWorkerAsync();
            return await completedTask.Task;
        }

        /// <summary>
        /// Get stations
        /// </summary>
        /// <returns>task of IEnumerable of StationToList</returns>
        public static async Task<IEnumerable<StationToList>> GetStations()
        {
            var taskCompletionSource = new TaskCompletionSource<IEnumerable<StationToList>>();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => e.Result = ibal.GetActiveStations().Select(item => PlServiceConvert.ConverterStationToList(item));
            workerPl.RunWorkerCompleted += (sender, e) => taskCompletionSource.SetResult((IEnumerable<StationToList>)e.Result);
            workerPl.RunWorkerAsync();
            return await taskCompletionSource.Task;
        }

        /// <summary>
        /// Get staions with empty charge slots
        /// </summary>
        /// <returns>Task of IEnumerable of StationToList</returns>
        public static async Task<IEnumerable<StationToList>> GetStaionsWithEmptyChargeSlots()
        {
            var taskCompletionSource = new TaskCompletionSource<IEnumerable<StationToList>>();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => e.Result = ibal.GetStaionsWithEmptyChargeSlots((int chargeSlots) => chargeSlots > 0).Select(item => PlServiceConvert.ConverterStationToList(item));
            workerPl.RunWorkerCompleted += (sender, e) => taskCompletionSource.SetResult(e.Result as IEnumerable<StationToList>);
            workerPl.RunWorkerAsync();
            return await taskCompletionSource.Task;

        }
        #endregion

        #region parcel

        /// <summary>
        /// Add parcel
        /// </summary>
        /// <param name="parcel">parcel</param>
        /// <returns>task</returns>
        public static async Task AddParcel(ParcelAdd parcel)
        {
            TaskCompletionSource completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => ibal.AddParcel(PlServiceConvert.ConvertBackParcelAdd(parcel));
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult();
            workerPl.RunWorkerAsync();
            await completedTask.Task;
        }

        /// <summary>
        /// Delete parcel
        /// </summary>
        /// <param name="id">id of parcel</param>
        /// <returns>task</returns>
        public static async Task DeleteParcel(int id)
        {
            TaskCompletionSource taskCompletion = new();
            BackgroundWorker worker = new();
            worker.DoWork += (sender, e) => ibal.DeleteParcel(id); ;
            worker.RunWorkerCompleted += (sender, e) => taskCompletion.SetResult();
            await taskCompletion.Task;

        }

        /// <summary>
        /// Check if parcel is not active
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>if parcel is not active</returns>
        public static async Task<bool> IsNotActiveParcel(int id)
        {
            TaskCompletionSource<bool> completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => e.Result = ibal.IsNotActiveParcel(id); ;
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult((bool)e.Result);
            workerPl.RunWorkerAsync();
            return await completedTask.Task;
        }

        /// <summary>
        /// Get parcel
        /// </summary>
        /// <param name="id">id of parcel</param>
        /// <returns>task of parcel</returns>
        public static async Task<Parcel> GetParcel(int id)
        {
            TaskCompletionSource<Parcel> completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => e.Result = PlServiceConvert.ConvertParcel(ibal.GetParcel(id));
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult(e.Result as Parcel);
            workerPl.RunWorkerAsync();
            return await completedTask.Task;
        }

        /// <summary>
        /// Get parcels
        /// </summary>
        /// <returns>Task of IEnumerable of ParcelToList</returns>
        public static async Task<IEnumerable<ParcelToList>> GetParcels()
        {
            var taskCompletionSource = new TaskCompletionSource<IEnumerable<ParcelToList>>();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => e.Result = ibal.GetActiveParcels().Select(parcel => PlServiceConvert.ConvertParcelToList(parcel));
            workerPl.RunWorkerCompleted += (sender, e) => taskCompletionSource.SetResult((IEnumerable<ParcelToList>)e.Result);
            workerPl.RunWorkerAsync();
            return await taskCompletionSource.Task;

        }

        #endregion

        #region drone

        /// <summary>
        /// Add drone
        /// </summary>
        /// <param name="drone">drone</param>
        /// <returns>task</returns>
        public static async Task AddDrone(DroneAdd drone)
        {
            TaskCompletionSource completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => ibal.AddDrone(PlServiceConvert.ConvertBackDroneToAdd(drone), drone.StationId);
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult();
            workerPl.RunWorkerAsync();
            await completedTask.Task;
        }

        /// <summary>
        /// Update drone
        /// </summary>
        /// <param name="id">id of drone</param>
        /// <param name="model">new model of drone</param>
        /// <returns>task</returns>
        public static async Task UpdateDrone(int id, string model)
        {
            var workerCompleted = new TaskCompletionSource();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => ibal.UpdateDrone(id, model);
            workerPl.RunWorkerCompleted += (sender, e) => workerCompleted.SetResult();
            workerPl.RunWorkerAsync();
            await workerCompleted.Task;
        }

        /// <summary>
        /// Send drone for charging
        /// </summary>
        /// <param name="id">id of drone</param>
        /// <returns>task</returns>
        public static async Task SendDroneForCharg(int id)
        {
            TaskCompletionSource completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => ibal.SendDroneForCharg(id);
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult();
            workerPl.RunWorkerAsync();
            await completedTask.Task;
        }

        /// <summary>
        /// Release drone from charging
        /// </summary>
        /// <param name="id">id of drone</param>
        /// <returns>task</returns>
        public static async Task ReleaseDroneFromCharging(int id)
        {
            TaskCompletionSource completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => ibal.ReleaseDroneFromCharging(id);
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult();
            workerPl.RunWorkerAsync();
            await completedTask.Task;
        }

        /// <summary>
        /// Delete drone
        /// </summary>
        /// <param name="id">id of drone</param>
        /// <returns>task</returns>
        public static async Task DeleteDrone(int id)
        {
            TaskCompletionSource taskCompletion = new();
            BackgroundWorker worker = new();
            worker.DoWork += (sender, e) => ibal.DeleteDrone(id); ;
            worker.RunWorkerCompleted += (sender, e) => taskCompletion.SetResult();
            await taskCompletion.Task;
        }

        /// <summary>
        /// Check if drone is not active
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>if drone is not active</returns>
        public static async Task<bool> IsNotActiveDrone(int id)
        {
            TaskCompletionSource<bool> completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => e.Result = ibal.IsNotActiveDrone(id);
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult((bool)e.Result);
            workerPl.RunWorkerAsync();
            return await completedTask.Task;
        }

        /// <summary>
        /// Get drone
        /// </summary>
        /// <param name="id">id of drone</param>
        /// <returns>task of drone</returns>
        public static async Task<Drone> GetDrone(int id)
        {
            try
            {
                TaskCompletionSource<Drone> completedTask = new();
                BackgroundWorker worker = new();
                worker.DoWork += (sender, e) => e.Result = PlServiceConvert.ConvertDrone(ibal.GetDrone(id));
                worker.RunWorkerCompleted += (sender, e) => completedTask.SetResult(e.Result as Drone);
                worker.RunWorkerAsync();
                return await completedTask.Task;
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }

        }

        /// <summary>
        /// Get drones
        /// </summary>
        /// <returns>Task of IEnumerable of DroneToList</returns>
        public static async Task<IEnumerable<DroneToList>> GetDrones()
        {
            TaskCompletionSource<IEnumerable<DroneToList>> completedTask = new();
            BackgroundWorker worker = new();
            worker.DoWork += (sender, e) => e.Result = ibal.GetActiveDrones().Select(item => PlServiceConvert.ConvertDroneToList(item));
            worker.RunWorkerCompleted += (sender, e) => completedTask.SetResult(e.Result as IEnumerable<DroneToList>);
            worker.RunWorkerAsync();
            return await completedTask.Task;
        }

        /// <summary>
        /// Assing parcel to drone
        /// </summary>
        /// <param name="droneId">drone's id</param>
        /// <returns>task</returns>
        public static async Task AssingParcelToDrone(int droneId)
        {
            try
            {
                TaskCompletionSource completedTask = new();
                BackgroundWorker workerPl = new();
                workerPl.DoWork += (sender, e) => ibal.AssingParcelToDrone(droneId);
                workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult();
                workerPl.RunWorkerAsync();
                await completedTask.Task;
            }
            catch (BO.NotExsistSutibleParcelException ex)
            {
                MessageBox.Show(ex.Message != string.Empty ? ex.Message : ex.ToString());
            }
        }

        /// <summary>
        /// Parcel collection by drone
        /// </summary>
        /// <param name="droneId">drone's id</param>
        /// <returns>task</returns>
        public static async Task ParcelCollectionByDrone(int droneId)
        {
            TaskCompletionSource completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => ibal.ParcelCollectionByDrone(droneId);
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult();
            workerPl.RunWorkerAsync();
            await completedTask.Task;
        }

        /// <summary>
        /// Delivery parcel by drone
        /// </summary>
        /// <param name="droneId">drone's id</param>
        /// <returns>task</returns>
        public static async Task DeliveryParcelByDrone(int droneId)
        {
            TaskCompletionSource completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => ibal.DeliveryParcelByDrone(droneId);
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult();
            workerPl.RunWorkerAsync();
            await completedTask.Task;
        }

        /// <summary>
        /// Start drone simulator
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="update">Action - update</param>
        /// <param name="checkStop">Func - check stop</param>
        public static void StartDroneSimulator(int id, Action<int?, int?, int?, int?> update, Func<bool> checkStop)
        {
            ibal.StartDroneSimulator(id, update, checkStop);
        }
        #endregion
    }
}

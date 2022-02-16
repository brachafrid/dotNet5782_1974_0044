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
            workerPl.DoWork += (sender, e) =>
            {
                try
                {
                    var pasw = ibal.GetAdministorPasssword();
                    completedTask.SetResult(pasw);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    completedTask.SetException(ex);
                }
            };
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
            workerPl.DoWork += (sender, args) =>
            {
                try
                {
                    ibal.AddCustomer(PlServiceConvert.ConvertAddCustomer(customer));
                    workerCompleted.SetResult();
                }
                catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException)
                {
                    MessageBox.Show("Id has already exist");
                    customer.Id = null;
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    workerCompleted.SetException(ex);
                }
            };
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

            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) =>
            {
                try
                {
                    var customer = PlServiceConvert.ConvertCustomer(ibal.GetCustomer(id));
                    taskCompletionSource.SetResult(customer);
                }
                catch (KeyNotFoundException ex)
                {
                    taskCompletionSource.SetException(ex);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    taskCompletionSource.SetException(ex);
                }
            };
            workerPl.RunWorkerAsync();
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
            workerPl.DoWork += (sender, e) =>
            {
                try
                {
                    var customers = ibal.GetActiveCustomers().Select(customer => PlServiceConvert.ConvertCustomerToList(customer));
                    taskCompletionSource.SetResult(customers);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    taskCompletionSource.SetException(ex);
                }
            };
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
            workerPl.DoWork += (sender, e) =>
                {
                    try
                    {
                        ibal.UpdateCustomer(id, name, phone);
                        workerCompleted.SetResult();
                    }
                    catch (ArgumentNullException ex)
                    {
                        workerCompleted.SetException(ex);
                    }
                    catch (KeyNotFoundException ex)
                    {
                        workerCompleted.SetException(ex);
                    }
                    catch (BO.XMLFileLoadCreateException ex)
                    {
                        workerCompleted.SetException(ex);
                    }
                };
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
            worker.DoWork += (sender, e) =>
            {
                try
                {
                    ibal.DeleteCustomer(id);
                    taskCompletion.SetResult();
                }
                catch (KeyNotFoundException ex)
                {
                    taskCompletion.SetException(ex);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    taskCompletion.SetException(ex);
                }
            };
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
            workerPl.DoWork += (sender, e) =>
            {
                try
                {
                    var active = ibal.IsNotActiveCustomer(id);
                    completedTask.SetResult(active);
                }
                catch (KeyNotFoundException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    completedTask.SetException(ex);
                }
            };
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
            workerPl.DoWork += (sender, e) =>
            {
                try
                {
                    ibal.AddStation(PlServiceConvert.ConverBackStationAdd(station));
                    completedTask.SetResult();
                }
                catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    completedTask.SetException(ex);
                }
            };
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
            workerPl.DoWork += (sender, e) =>
            {
                try
                {
                    ibal.UpdateStation(id, name, chargeSlots);
                    completedTask.SetResult();
                }
                catch (ArgumentNullException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (KeyNotFoundException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    completedTask.SetException(ex);
                }
            };
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
            workerPl.DoWork += (sender, e) =>
            {
                try
                {
                    var stations = PlServiceConvert.ConverterStation(ibal.GetStation(id));
                    completedTask.SetResult(stations);
                }
                catch (KeyNotFoundException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    completedTask.SetException(ex);
                }
            };
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
            worker.DoWork += (sender, e) =>
            {
                try
                {
                    ibal.DeleteStation(id);
                    taskCompletion.SetResult();
                }
                catch (BO.ThereAreAssociatedOrgansException ex)
                {
                    taskCompletion.SetException(ex);
                }
                catch (KeyNotFoundException ex)
                {
                    taskCompletion.SetException(ex);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    taskCompletion.SetException(ex);
                }
            };
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
            workerPl.DoWork += (sender, e) =>
            {
                try
                {
                    var active = ibal.IsNotActiveStation(id);
                    completedTask.SetResult(active);
                }
                catch (KeyNotFoundException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    completedTask.SetException(ex);
                }
            };
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
            workerPl.DoWork += (sender, e) =>
            {
                try
                {
                    var stations = ibal.GetActiveStations().Select(item => PlServiceConvert.ConverterStationToList(item));
                    taskCompletionSource.SetResult(stations);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    taskCompletionSource.SetException(ex);
                }
            };
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
            workerPl.DoWork += (sender, e) =>
            {
                try
                {
                    var stations = ibal.GetStaionsWithEmptyChargeSlots((int chargeSlots) => chargeSlots > 0).Select(item => PlServiceConvert.ConverterStationToList(item));
                    taskCompletionSource.SetResult(stations);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    taskCompletionSource.SetException(ex);
                }
            };
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
            workerPl.DoWork += (sender, e) =>
            {
                try
                {
                    ibal.AddParcel(PlServiceConvert.ConvertBackParcelAdd(parcel));
                    completedTask.SetResult();
                }
                catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (KeyNotFoundException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    completedTask.SetException(ex);
                }
            };
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
            worker.DoWork += (sender, e) =>
            {
                try
                {
                    ibal.DeleteParcel(id);
                    taskCompletion.SetResult();

                }
                catch (KeyNotFoundException ex)
                {
                    taskCompletion.SetException(ex);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    taskCompletion.SetException(ex);
                }
            };
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
            workerPl.DoWork += (sender, e) =>
            {
                try
                {
                    var active = ibal.IsNotActiveParcel(id);
                    completedTask.SetResult(active);

                }
                catch (KeyNotFoundException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    completedTask.SetException(ex);
                }
            };
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
            workerPl.DoWork += (sender, e) =>
            {
                try
                {
                    var parcel = PlServiceConvert.ConvertParcel(ibal.GetParcel(id));
                    completedTask.SetResult(parcel);
                }
                catch (KeyNotFoundException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    completedTask.SetException(ex);
                }
            };
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
            workerPl.DoWork += (sender, e) =>
            {
                try
                {
                    var parcels = ibal.GetActiveParcels().Select(parcel => PlServiceConvert.ConvertParcelToList(parcel));
                    taskCompletionSource.SetResult(parcels);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    taskCompletionSource.SetException(ex);
                }
            };
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
            workerPl.DoWork += (sender, e) =>
            {
                try
                {
                    ibal.AddDrone(PlServiceConvert.ConvertBackDroneToAdd(drone), drone.StationId);
                    completedTask.SetResult();
                }
                catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (KeyNotFoundException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    completedTask.SetException(ex);
                }
            };
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
            workerPl.DoWork += (sender, e) =>
            {
                try
                {
                    ibal.UpdateDrone(id, model);
                    workerCompleted.SetResult();

                }
                catch (KeyNotFoundException ex)
                {
                    workerCompleted.SetException(ex);
                }
                catch (ArgumentNullException ex)
                {
                    workerCompleted.SetException(ex);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    workerCompleted.SetException(ex);
                }
            };
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
            workerPl.DoWork += (sender, e) =>
            {
                try
                {
                    ibal.SendDroneForCharg(id);
                    completedTask.SetResult();

                }
                catch (BO.ThereIsNoNearbyBaseStationThatTheDroneCanReachException)
                {
                    completedTask.SetResult();
                }
                catch (KeyNotFoundException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.DeletedExeption ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.InvalidDroneStateException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    completedTask.SetException(ex);
                }
                catch(BO.NotExsistSutibleParcelException ex)
                {
                    completedTask.SetException(ex);
                }
            };
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
            workerPl.DoWork += (sender, e) =>
            {
                try
                {
                    ibal.ReleaseDroneFromCharging(id);
                    completedTask.SetResult();

                }
                catch (KeyNotFoundException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.DeletedExeption ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.InvalidDroneStateException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.TheDroneIsNotInChargingException ex)
                {
                    completedTask.SetException(ex);
                }
            };
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
            worker.DoWork += (sender, e) =>
            {
                try
                {
                    ibal.DeleteDrone(id);
                    taskCompletion.SetResult();

                }
                catch (KeyNotFoundException ex)
                {
                    taskCompletion.SetException(ex);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    taskCompletion.SetException(ex);
                }
                catch (BO.DeletedExeption ex)
                {
                    taskCompletion.SetException(ex);
                }
                catch (BO.InvalidDroneStateException ex)
                {
                    taskCompletion.SetException(ex);
                }
                catch (BO.TheDroneIsNotInChargingException ex)
                {
                    taskCompletion.SetException(ex);
                }
            };
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
            workerPl.DoWork += (sender, e) =>
            {
                try
                {
                    var active = ibal.IsNotActiveDrone(id);
                    completedTask.SetResult(active);

                }
                catch (KeyNotFoundException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    completedTask.SetException(ex);
                }
            };
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
            TaskCompletionSource<Drone> completedTask = new();
            BackgroundWorker worker = new();
            worker.DoWork += (sender, e) =>
            {
                try
                {
                    var drone = PlServiceConvert.ConvertDrone(ibal.GetDrone(id));
                    completedTask.SetResult(drone);

                }
                catch (ArgumentNullException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    completedTask.SetException(ex);
                }
            };
            worker.RunWorkerAsync();
            return await completedTask.Task;
        }

        /// <summary>
        /// Get drones
        /// </summary>
        /// <returns>Task of IEnumerable of DroneToList</returns>
        public static async Task<IEnumerable<DroneToList>> GetDrones()
        {
            TaskCompletionSource<IEnumerable<DroneToList>> completedTask = new();
            BackgroundWorker worker = new();
            worker.DoWork += (sender, e) =>
            {
                try
                {
                    var drone = ibal.GetActiveDrones().Select(item => PlServiceConvert.ConvertDroneToList(item));
                    completedTask.SetResult(drone);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    completedTask.SetException(ex);
                }
            };
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
            TaskCompletionSource completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) =>
            {
                try
                {
                    ibal.AssingParcelToDrone(droneId);
                    completedTask.SetResult();
                }
                catch (BO.NotExsistSuitibleStationException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.DeletedExeption ex)
                {
                    completedTask.SetException(ex);
                }
                catch (ArgumentNullException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.InvalidParcelStateException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    completedTask.SetException(ex);
                };
            };
            workerPl.RunWorkerAsync();
            await completedTask.Task;

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
            workerPl.DoWork += (sender, e) =>
            {
                try
                {
                    ibal.ParcelCollectionByDrone(droneId);
                    completedTask.SetResult();
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.DeletedExeption ex)
                {
                    completedTask.SetException(ex);
                }
                catch (ArgumentNullException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.InvalidParcelStateException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.NotExsistSuitibleStationException ex)
                {
                    completedTask.SetException(ex);
                }
            };
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
            workerPl.DoWork += (sender, e) =>
            {
                try
                {
                    ibal.DeliveryParcelByDrone(droneId);
                    completedTask.SetResult();
                }
                catch (BO.XMLFileLoadCreateException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.DeletedExeption ex)
                {
                    completedTask.SetException(ex);
                }
                catch (ArgumentNullException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.InvalidParcelStateException ex)
                {
                    completedTask.SetException(ex);
                }
                catch (BO.NotExsistSuitibleStationException ex)
                {
                    completedTask.SetException(ex);
                }
            };
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

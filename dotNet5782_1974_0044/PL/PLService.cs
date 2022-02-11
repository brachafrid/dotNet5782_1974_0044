using BLApi;
using PL.PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PL
{
    public static class PLService
    {
        private static IBL ibal = BLFactory.GetBL();

        public static string GetAdministorPasssword()
        {
            return ibal.GetAdministorPasssword();
        }

        #region customer
        public static async Task AddCustomer(CustomerAdd customer)
        {
            var workerCompleted = new TaskCompletionSource();

            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, args) => ibal.AddCustomer(PlServiceConvert.ConvertAddCustomer(customer));
            workerPl.RunWorkerCompleted += (sender, e) => workerCompleted.SetResult();
            workerPl.RunWorkerAsync();

            await workerCompleted.Task;
        }
        public static async Task<Customer> GetCustomer(int id)
        {
            var taskCompletionSource = new TaskCompletionSource<Customer>();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => e.Result = PlServiceConvert.ConvertCustomer(ibal.GetCustomer(id));
            workerPl.RunWorkerCompleted += (sender, e) => taskCompletionSource.SetResult(e.Result as Customer);
            workerPl.RunWorkerAsync();
            return await taskCompletionSource.Task;
        }
        public static async Task<IEnumerable<CustomerToList>> GetCustomers()
        {
            var taskCompletionSource = new TaskCompletionSource<IEnumerable<CustomerToList>>();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => e.Result = ibal.GetActiveCustomers().Select(customer => PlServiceConvert.ConvertCustomerToList(customer));
            workerPl.RunWorkerCompleted += (sender, e) => taskCompletionSource.SetResult((IEnumerable<CustomerToList>)e.Result);
            workerPl.RunWorkerAsync();
            return await taskCompletionSource.Task;
        }
        public static async Task UpdateCustomer(int id, string name, string phone)
        {
            var workerCompleted = new TaskCompletionSource();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => ibal.UpdateCustomer(id, name, phone);
            workerPl.RunWorkerCompleted += (sender, e) => workerCompleted.SetResult();
            workerPl.RunWorkerAsync();
            await workerCompleted.Task;
        }
        public static void DeleteCustomer(int id)
        {
            ibal.DeleteCustomer(id);
        }
        public static bool IsNotActiveCustomer(int id) => ibal.IsNotActiveCustomer(id);
        #endregion

        #region station
        public static async Task AddStation(StationAdd station)
        {
            TaskCompletionSource completedTask =new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => ibal.AddStation(PlServiceConvert.ConverBackStationAdd(station));
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult();
            workerPl.RunWorkerAsync();
            await completedTask.Task;
        }
        public static async Task UpdateStation(int id, string name, int chargeSlots)
        {
            TaskCompletionSource completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => ibal.UpdateStation(id, name, chargeSlots);
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult();
            workerPl.RunWorkerAsync();
            await completedTask.Task;
        }
        public static Station GetStation(int id)
        {
            return PlServiceConvert.ConverterStation(ibal.GetStation(id));
        }
        public static void DeleteStation(int id)
        {
            ibal.DeleteStation(id);
        }
        public static bool IsNotActiveStation(int id) => ibal.IsNotActiveStation(id);
        public static IEnumerable<StationToList> GetStations()
        {
            return ibal.GetActiveStations().Select(item => PlServiceConvert.ConverterStationToList(item));
        }
        public static IEnumerable<StationToList> GetStaionsWithEmptyChargeSlots()
        {
            return ibal.GetStaionsWithEmptyChargeSlots((int chargeSlots) => chargeSlots > 0).Select(item => PlServiceConvert.ConverterStationToList(item));
        }
        #endregion

        #region parcel
        public static async Task AddParcel(ParcelAdd parcel)
        {
            TaskCompletionSource completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => ibal.AddParcel(PlServiceConvert.ConvertBackParcelAdd(parcel));
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult();
            workerPl.RunWorkerAsync();
            await completedTask.Task;
        }
        public static void DeleteParcel(int id)
        {
            ibal.DeleteParcel(id);
        }
        public static bool IsNotActiveParcel(int id) => ibal.IsNotActiveParcel(id);
        public static Parcel GetParcel(int id) => PlServiceConvert.ConvertParcel(ibal.GetParcel(id));
        public static async Task< IEnumerable<ParcelToList>> GetParcels()
        {
            var taskCompletionSource = new TaskCompletionSource<IEnumerable<ParcelToList>>();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => e.Result = ibal.GetActiveParcels().Select(parcel => PlServiceConvert.ConvertParcelToList(parcel));
            workerPl.RunWorkerCompleted += (sender, e) => taskCompletionSource.SetResult((IEnumerable<ParcelToList>)e.Result);
            workerPl.RunWorkerAsync();
            return await taskCompletionSource.Task;
           
        }
        public static IEnumerable<ParcelToList> GetParcelsNotAssignedToDrone => ibal.GetParcelsNotAssignedToDrone((int num) => num == 0).Select(parcel => PlServiceConvert.ConvertParcelToList(parcel));
        #endregion

        #region drone
        public static async Task AddDrone(DroneAdd drone)
        {
            TaskCompletionSource completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => ibal.AddDrone(PlServiceConvert.ConvertBackDroneToAdd(drone), drone.StationId);
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult();
            workerPl.RunWorkerAsync();
            await completedTask.Task;
        }
        public static async Task UpdateDrone(int id, string model)
        {
            var workerCompleted = new TaskCompletionSource();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => ibal.UpdateDrone(id, model);
            workerPl.RunWorkerCompleted += (sender, e) => workerCompleted.SetResult();
            workerPl.RunWorkerAsync();
            await workerCompleted.Task;
        }
        public static void SendDroneForCharg(int id)
        {
            ibal.SendDroneForCharg(id);
        }
        public static void ReleaseDroneFromCharging(int id)
        {
            ibal.ReleaseDroneFromCharging(id);
        }
        public static void DeleteDrone(int id)
        {
            ibal.DeleteDrone(id);
        }
        public static bool IsNotActiveDrone(int id) => ibal.IsNotActiveDrone(id);
        public static async Task<Drone> GetDrone(int id)
        {
            try
            {
                TaskCompletionSource<Drone> completedTask = new();
                BackgroundWorker worker = new();
                worker.DoWork += (sender,e)=> e.Result=PlServiceConvert.ConvertDrone(ibal.GetDrone(id));
                worker.RunWorkerCompleted += (sender, e) => completedTask.SetResult(e.Result as Drone);
                worker.RunWorkerAsync();
                return await completedTask.Task;
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }

        }
        public static async Task< IEnumerable<DroneToList>> GetDrones()
        {
            TaskCompletionSource<IEnumerable<DroneToList>> completedTask = new();
            BackgroundWorker worker = new();
            worker.DoWork += (sender, e) => e.Result = ibal.GetActiveDrones().Select(item => PlServiceConvert.ConvertDroneToList(item));
            worker.RunWorkerCompleted += (sender, e) => completedTask.SetResult(e.Result as IEnumerable<DroneToList>);
            worker.RunWorkerAsync();
            return await completedTask.Task;

        }
        public static void AssingParcelToDrone(int droneId)
        {
            ibal.AssingParcelToDrone(droneId);
        }
        public static void ParcelCollectionByDrone(int droneId)
        {
            ibal.ParcelCollectionByDrone(droneId);
        }
        public static void DeliveryParcelByDrone(int droneId)
        {
            ibal.DeliveryParcelByDrone(droneId);
        }
        public static void StartDroneSimulator(int id, Action<int?, int?, int?, int?> update, Func<bool> checkStop)
        {
            ibal.StartDroneSimulator(id, update, checkStop);
        }
        #endregion
    }
}

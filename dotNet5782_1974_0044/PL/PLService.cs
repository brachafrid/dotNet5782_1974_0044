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
        public static async Task DeleteCustomer(int id)
        {
            TaskCompletionSource taskCompletion = new();
            BackgroundWorker worker = new();
            worker.DoWork += (sender, e) => ibal.DeleteCustomer(id);
            worker.RunWorkerCompleted += (sender, e) => taskCompletion.SetResult();
            await taskCompletion.Task;
        }
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
        public static async Task AddStation(StationAdd station)
        {
            TaskCompletionSource completedTask = new();
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
        public static async Task<Station> GetStation(int id)
        {
            TaskCompletionSource<Station> completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => e.Result = PlServiceConvert.ConverterStation(ibal.GetStation(id));
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult(e.Result as Station);
            workerPl.RunWorkerAsync();
            return await completedTask.Task;

        }
        public static async Task DeleteStation(int id)
        {
            TaskCompletionSource taskCompletion = new();
            BackgroundWorker worker = new();
            worker.DoWork += (sender, e) => ibal.DeleteStation(id);
            worker.RunWorkerCompleted += (sender, e) => taskCompletion.SetResult();
            await taskCompletion.Task;

        }
        public static async Task<bool> IsNotActiveStation(int id)
        {
            TaskCompletionSource<bool> completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => e.Result = ibal.IsNotActiveStation(id);
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult((bool)e.Result);
            workerPl.RunWorkerAsync();
            return await completedTask.Task;
        }
        public static async Task<IEnumerable<StationToList>> GetStations()
        {
            var taskCompletionSource = new TaskCompletionSource<IEnumerable<StationToList>>();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => e.Result = ibal.GetActiveStations().Select(item => PlServiceConvert.ConverterStationToList(item));
            workerPl.RunWorkerCompleted += (sender, e) => taskCompletionSource.SetResult((IEnumerable<StationToList>)e.Result);
            workerPl.RunWorkerAsync();
            return await taskCompletionSource.Task;

        }
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
        public static async Task AddParcel(ParcelAdd parcel)
        {
            TaskCompletionSource completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => ibal.AddParcel(PlServiceConvert.ConvertBackParcelAdd(parcel));
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult();
            workerPl.RunWorkerAsync();
            await completedTask.Task;
        }
        public static async Task DeleteParcel(int id)
        {
            TaskCompletionSource taskCompletion = new();
            BackgroundWorker worker = new();
            worker.DoWork += (sender, e) => ibal.DeleteParcel(id); ;
            worker.RunWorkerCompleted += (sender, e) => taskCompletion.SetResult();
            await taskCompletion.Task;

        }
        public static async Task< bool> IsNotActiveParcel(int id)
        {
            TaskCompletionSource<bool> completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => e.Result = ibal.IsNotActiveParcel(id); ;
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult((bool)e.Result);
            workerPl.RunWorkerAsync();
            return await completedTask.Task;
        }
        public static async Task<Parcel> GetParcel(int id)
        {
            TaskCompletionSource<Parcel> completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => e.Result = PlServiceConvert.ConvertParcel(ibal.GetParcel(id));
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult(e.Result as Parcel);
            workerPl.RunWorkerAsync();
            return await completedTask.Task;

        }
        public static async Task<IEnumerable<ParcelToList>> GetParcels()
        {
            var taskCompletionSource = new TaskCompletionSource<IEnumerable<ParcelToList>>();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => e.Result = ibal.GetActiveParcels().Select(parcel => PlServiceConvert.ConvertParcelToList(parcel));
            workerPl.RunWorkerCompleted += (sender, e) => taskCompletionSource.SetResult((IEnumerable<ParcelToList>)e.Result);
            workerPl.RunWorkerAsync();
            return await taskCompletionSource.Task;

        }
        //public static async Task< IEnumerable<ParcelToList>> GetParcelsNotAssignedToDrone()
        //{
        //    var taskCompletionSource = new TaskCompletionSource<IEnumerable<ParcelToList>>();
        //    BackgroundWorker workerPl = new();
        //    workerPl.DoWork += (sender, e) => e.Result = ibal.GetParcelsNotAssignedToDrone((int num) => num == 0).Select(parcel => PlServiceConvert.ConvertParcelToList(parcel));
        //    workerPl.RunWorkerCompleted += (sender, e) => taskCompletionSource.SetResult(e.Result as IEnumerable<ParcelToList>);
        //    workerPl.RunWorkerAsync();
        //    return await taskCompletionSource.Task;
        //}

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
        public static async Task SendDroneForCharg(int id)
        {
            TaskCompletionSource completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) =>  ibal.SendDroneForCharg(id);
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult();
            workerPl.RunWorkerAsync();
            await completedTask.Task;
           
        }
        public static async Task ReleaseDroneFromCharging(int id)
        {
            TaskCompletionSource completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) =>   ibal.ReleaseDroneFromCharging(id);
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult();
            workerPl.RunWorkerAsync();
            await completedTask.Task;
         
        }
        public static async Task DeleteDrone(int id)
        {
            TaskCompletionSource taskCompletion = new();
            BackgroundWorker worker = new();
            worker.DoWork += (sender, e) => ibal.DeleteDrone(id); ;
            worker.RunWorkerCompleted += (sender, e) => taskCompletion.SetResult();
            await taskCompletion.Task;

        }
        public static async Task< bool> IsNotActiveDrone(int id)
        {
            TaskCompletionSource<bool> completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => e.Result = ibal.IsNotActiveDrone(id); 
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult((bool)e.Result);
            workerPl.RunWorkerAsync();
            return await completedTask.Task;
        }
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
        public static async Task<IEnumerable<DroneToList>> GetDrones()
        {
            TaskCompletionSource<IEnumerable<DroneToList>> completedTask = new();
            BackgroundWorker worker = new();
            worker.DoWork += (sender, e) => e.Result = ibal.GetActiveDrones().Select(item => PlServiceConvert.ConvertDroneToList(item));
            worker.RunWorkerCompleted += (sender, e) => completedTask.SetResult(e.Result as IEnumerable<DroneToList>);
            worker.RunWorkerAsync();
            return await completedTask.Task;

        }
        public static async Task AssingParcelToDrone(int droneId)
        {
            TaskCompletionSource completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => ibal.AssingParcelToDrone(droneId);
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult();
            workerPl.RunWorkerAsync();
            await completedTask.Task;
            
        }
        public static async Task ParcelCollectionByDrone(int droneId)
        {
            TaskCompletionSource completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => ibal.ParcelCollectionByDrone(droneId);
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult();
            workerPl.RunWorkerAsync();
            await completedTask.Task;
            
        }
        public static async Task DeliveryParcelByDrone(int droneId)
        {
            TaskCompletionSource completedTask = new();
            BackgroundWorker workerPl = new();
            workerPl.DoWork += (sender, e) => ibal.DeliveryParcelByDrone(droneId);
            workerPl.RunWorkerCompleted += (sender, e) => completedTask.SetResult();
            workerPl.RunWorkerAsync();
            await completedTask.Task;
            
        }
        public static void StartDroneSimulator(int id, Action<int?, int?, int?, int?> update, Func<bool> checkStop)
        {
            ibal.StartDroneSimulator(id, update, checkStop);
        }
        #endregion
    }
}

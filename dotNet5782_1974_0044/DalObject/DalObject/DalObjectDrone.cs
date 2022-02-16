using DLApi;
using System.Collections.Generic;
using System.Linq;
using System;
using DO;
using System.Runtime.CompilerServices;
namespace Dal
{
    public partial class DalObject:IDalDrone
    {


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(int id, string model, WeightCategories MaxWeight)
        {
            if (ExistsIDTaxCheck(DataSorce.Drones, id))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(id);
            DalObjectService.AddEntity(new Drone()
            {
                Id = id,
                Model = model,
                MaxWeight = MaxWeight,
                IsNotActive = false

            });
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int id)
        {
            Drone drone = DalObjectService.GetEntities<Drone>().FirstOrDefault(item => item.Id == id);
            if (drone.Equals(default(Drone)) )
                throw new KeyNotFoundException($"There is not suitable drone in the data , the id {id}");
            return drone;
        }

         [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDrones() => DalObjectService.GetEntities<Drone>();

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(Drone drone,string model)
        {
            DalObjectService.RemoveEntity(drone);
            drone.Model = model;
            DalObjectService.AddEntity(drone);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(int id)
        {
            Drone drone = DalObjectService.GetEntities<Drone>().FirstOrDefault(item => item.Id == id);
            if (drone.Equals(default(Drone)))
                throw new KeyNotFoundException("There is no suitable drone in data so the deleted failed");
            DalObjectService.RemoveEntity(drone);
            drone.IsNotActive = true;
            DalObjectService.AddEntity(drone);
        }
    }
}

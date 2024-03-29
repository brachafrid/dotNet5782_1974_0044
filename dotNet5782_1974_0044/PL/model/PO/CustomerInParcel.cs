﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace PL.PO
    {
    public class CustomerInParcel : NotifyPropertyChangedBase
    {
       
        private int id;
        /// <summary>
        /// customer in parcel key
        /// </summary>
        public int Id
        {
            get => id;
            init => Set(ref id, value);
           
        }
        
        private string name;
        /// <summary>
        /// customer in parcel  name
        /// </summary>
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }
        public override string ToString()
            {
                return this.ToStringProperties();
            }
        }
    }



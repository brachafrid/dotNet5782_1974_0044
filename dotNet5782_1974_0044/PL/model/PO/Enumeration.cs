﻿namespace PL.PO
{
    public enum WeightCategories { LIGHT, MEDIUM, HEAVY }
    public enum Priorities { REGULAR, FAST, EMERGENCY }
    public enum DroneState { AVAILABLE, MAINTENANCE, DELIVERY ,RESCUE}
    public enum PackageModes { DEFINED, ASSOCIATED, COLLECTED, PROVIDED }
    public enum Screen { LOGIN, ADMINISTOR,CUSTOMER}
    public enum LoginState { CLOSE, ADMINISTOR, CUSTOMER, SIGNIN}
    public enum FilterType {STRING,WEGHIT,PIORITY,STATE,PACKEGE,NUMBER }
    public enum ParcelListWindowState { ALL, TO_CUSTOMER, FROM_CUSTOMER}
}


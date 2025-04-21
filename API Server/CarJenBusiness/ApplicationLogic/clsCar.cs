using CarJenData.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsCar
    {
        public int? CarID { get; set; }
        public int? BrandID { get; set; }
        public string Brand { get; set; }
        public int? ModelID { get; set; }
        public string Model { get; set; }
        public int? TrimID { get; set; }
        public string Trim { get; set; }
        public short? FuelType { get; set; }
        public int? Mileage { get; set; }
        public short? TransmissionType { get; set; }
        public short? Year { get; set; }
        public string Color { get; set; }
        public decimal? Price { get; set; }
        public string PlateNumber { get; set; }
        public DateTime? RegistrationExp { get; set; }
        public DateTime? TechInspectionExp { get; set; }
        enum enMode { Add = 0, Update = 1 }
        public enum enFuelType { Gasoline = 0, Diesel = 1, Electric = 2, Hybrid = 3 }
        public enum enTransmissionType { Manual = 0, Automatic = 1 }

        public clsImageCollection imageCollection { get; set; }
        //public string frontView { get; set; }
        //public string rearView { get; set; }
        //public string sideView { get; set; }
        //public string interiorView { get; set; }

        enMode Mode;
        private clsCar(int? carID, int? brandID, string brand, int? modelID, string model, int? trimID,
        string trim, short? fuelType, int? mileage, short? transmissionType, short? year,
        string color, decimal? price, string plateNumber, DateTime? registrationExp,
        DateTime? techInspectionExp)
        {
            CarID = carID;
            BrandID = brandID;
            Brand = brand;
            ModelID = modelID;
            Model = model;
            TrimID = trimID;
            Trim = trim;
            FuelType = fuelType;
            Mileage = mileage;
            TransmissionType = transmissionType;
            Year = year;
            Color = color;
            Price = price;
            PlateNumber = plateNumber;
            RegistrationExp = registrationExp;
            TechInspectionExp = techInspectionExp;
            imageCollection = clsImageCollection.Find(CarID);


            Mode = enMode.Update;
        }

        public clsCar()
        {
            CarID = null;
            BrandID = null;
            Brand = "";
            ModelID = null;
            Model = ""; ;
            TrimID = null;
            Trim = "";
            FuelType = null;
            Mileage = null;
            TransmissionType = null;
            Year = null;
            Color = "";
            Price = null;
            PlateNumber = "";
            RegistrationExp = null;
            TechInspectionExp = null;

            Mode = enMode.Add;
        }
        public static string GetFuelTypeText(enFuelType FuelType)
        {
            switch (FuelType)
            {
                case enFuelType.Gasoline:
                    {
                        return "Gasoline";
                    }
                case enFuelType.Diesel:
                    {
                        return "Diesel";
                    }
                case enFuelType.Electric:
                    {
                        return "Electric";
                    }
                default:
                    {
                        return "Hybrid";
                    }
            }
        }
        public static string GetTransmissionTypeText(enTransmissionType TransmissionType)
        {
            switch (TransmissionType)
            {
                case enTransmissionType.Manual:
                    {
                        return "Manual";
                    }
                default:
                    {
                        return "Automatic";
                    }
            }
        }


        private bool _AddCar()
        {
            this.CarID = CarRepository.AddCar2(TrimID, FuelType, Mileage, TransmissionType, Year);
            return this.CarID != null;
        }
        private bool _UpdateCar()
        {
            return CarRepository.UpdateCar(CarID, TrimID, FuelType, Mileage, TransmissionType, Year, Color, Price, PlateNumber, RegistrationExp, TechInspectionExp);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    if (_AddCar())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateCar();

            }
            return false;
        }
        static public bool Delete(int CarID)
        {
            return CarRepository.Delete(CarID);
        }
        static public clsCar Find(int? CarID)
        {
            int? BrandID = null; string Brand = ""; int? ModelID = null; string Model = "";
            int? TrimID = null; string Trim = ""; short? FuelType = null; int? Mileage = null; short? TransmissionType = null;
            short? Year = null; string Color = ""; decimal? Price = null;
            string PlateNumber = ""; DateTime? RegistrationExp = null; DateTime? TechInspectionExp = null;

            if (CarRepository.GetCarByID(CarID, ref BrandID, ref Brand, ref ModelID, ref Model, ref TrimID,
                ref Trim, ref FuelType, ref Mileage, ref TransmissionType, ref Year, ref Color,
                ref Price, ref PlateNumber, ref RegistrationExp, ref TechInspectionExp))
                return new clsCar(CarID, BrandID, Brand, ModelID, Model, TrimID, Trim, FuelType,
                    Mileage, TransmissionType, Year, Color, Price, PlateNumber, RegistrationExp,
                    TechInspectionExp);
            else
                return null;
        }
        static public clsCar Find(string PlateNumber)
        {
            int? CarID = null; int? BrandID = null; string Brand = ""; int? ModelID = null; string Model = "";
            int? TrimID = null; string Trim = ""; short? FuelType = null; int? Mileage = null; short? TransmissionType = null;
            short? Year = null; string Color = ""; decimal? Price = null; bool? IsNegotiable = null;
            DateTime? RegistrationExp = null; DateTime? TechInspectionExp = null;

            if (CarRepository.GetCarByPlateNumber(PlateNumber, ref CarID, ref BrandID, ref Brand, ref ModelID, ref Model, ref TrimID,
                ref Trim, ref FuelType, ref Mileage, ref TransmissionType, ref Year, ref Color, ref Price, ref IsNegotiable, ref RegistrationExp, ref TechInspectionExp))
                return new clsCar(CarID, BrandID, Brand, ModelID, Model, TrimID, Trim, FuelType,
                    Mileage, TransmissionType, Year, Color, Price, PlateNumber, RegistrationExp,
                    TechInspectionExp);
            else
                return null;
        }

        static public DataTable GetAllCars()
        {
            return CarRepository.GetAllCars();
        }
        public static bool IsCarExist(int CarID)
        {
            return CarRepository.IsCarExist(CarID);
        }
        public static bool IsCarExist(string PlateNumber)
        {
            return CarRepository.IsCarExist(PlateNumber);
        }

    }

}

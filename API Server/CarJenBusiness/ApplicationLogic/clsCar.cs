using CarJenData.Repositories;
using CarJenShared.Dtos.CarDto;
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
        public CarDto ToCarDto
        {
            get
            {
                return new CarDto
                {
                    CarID = this.CarID,
                    Brand = new BrandDto
                    {
                        BrandID = this.BrandID,
                        Brand = this.Brand,
                    },
                    Model = new ModelDto
                    {
                        ModelID = this.ModelID,
                        Model = this.Model,
                        BrandID = this.BrandID
                    },
                    Trim = new TrimDto
                    {
                        TrimID = this.TrimID,
                        Trim = this.Trim,
                        ModelID = this.ModelID
                    },
                    FuelType = this.FuelType,
                    Mileage = this.Mileage,
                    TransmissionType = this.TransmissionType,
                    Year = this.Year,
                    Color = this.Color,
                    Price = this.Price,
                    PlateNumber = this.PlateNumber,
                    RegistrationExp = this.RegistrationExp,
                    TechInspectionExp = this.TechInspectionExp
                };
            }
        }

        enum enMode { Add = 0, Update = 1 }
        public enum enFuelType { Gasoline = 0, Diesel = 1, Electric = 2, Hybrid = 3 }
        public enum enTransmissionType { Manual = 0, Automatic = 1 }

        public clsImageCollection imageCollection { get; set; }

        enMode Mode;
        private clsCar(CarDto carDto)
        {
            CarID = carDto.CarID;
            BrandID = carDto.Brand.BrandID;
            Brand = carDto.Brand.Brand;
            ModelID = carDto.Model.ModelID;
            Model = carDto.Model.Model;
            TrimID = carDto.Trim.TrimID;
            Trim = carDto.Trim.Trim;
            FuelType = carDto.FuelType;
            Mileage = carDto.Mileage;
            TransmissionType = carDto.TransmissionType;
            Year = carDto.Year;
            Color = carDto.Color;
            Price = carDto.Price;
            PlateNumber = carDto.PlateNumber;
            RegistrationExp = carDto.RegistrationExp;
            TechInspectionExp = carDto.TechInspectionExp;
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
            this.CarID = CarRepository.AddCar(this.ToCarDto);
            return this.CarID != null;
        }
        private bool _UpdateCar()
        {
            return CarRepository.UpdateCar(this.ToCarDto);
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
            return CarRepository.DeleteCar(CarID);
        }
        static public clsCar? Find(int? CarID)
        {
            var car = CarRepository.GetCarByID(CarID);

            if(car != null)
                return new clsCar(car);
            else
                return null;
        }
        static public clsCar Find(string PlateNumber)
        {
            var car = CarRepository.GetCarByPlateNumber(PlateNumber);

            if (car != null)
                return new clsCar(car);
            else
                return null;
        }

        static public List<CarDto> GetAllCars()
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

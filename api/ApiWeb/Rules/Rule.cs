using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiWeb.Rules
{
    public partial class Rule
    {

        private string _day;
        private string _request;
        private string _typeVehicle;
        private int _quantity;
        

        private int _checkTypeVehicle;
        private int _occupation;
        private string assignedDay;
        private int quantityAvalible;
        private int _checkQuantity;

        #region "propiedades"
        public string AssignedDay 
        {
            get { return  assignedDay; }
        }

        public int QuantityAvalible
        {
            get { return _quantity - quantityAvalible ; }
        }

        public string TypeVehicle
        {
            get { return _typeVehicle; }
        }

        public string Request
        {
            get { return _request; }
        }

        #endregion

        public Rule(string day, string request, string typeVehicle, int quantity)
        {
            _day = day;
            _request = request;
            _typeVehicle = typeVehicle;
            _quantity = quantity;
        }

        public void order()
        {
            //int high = 0;

            //while (++high * _quantity > 16) 
            //{
            //    _checkTypeVehicle = chechkTypeVehicle(_typeVehicle); // trae el tipo de vehiculo
            //    _occupation = _checkTypeVehicle * _quantity; // calcula el total de horas por orden creada
            //    avalibleDay(_occupation, _day, _checkTypeVehicle); // calcula ocupacion y asigna dia siguiente si es necesario
            //}

            _checkTypeVehicle = chechkTypeVehicle(_typeVehicle); // trae el tipo de vehiculo
            _occupation = _checkTypeVehicle * _quantity; // calcula el total de horas por orden creada
            avalibleDay(_occupation, _day, _checkTypeVehicle); // calcula ocupacion y asigna dia siguiente si es necesario

        }

        private int chechkTypeVehicle(string typeVehicle) 
        {
            if (typeVehicle == "renault")
            {
                return 1;
            }
            else 
            {
                if (typeVehicle == "chevrolet")
                {
                    return 2;
                }
                else 
                {
                    if (typeVehicle == "ford")
                    {
                        return 3;
                    }
                    else 
                    {
                        if (typeVehicle == "toyota")
                        {
                            return 4;
                        }
                    }
                }
            }
            return 0;

        }

        private void avalibleDay(int occupation, string day, int checkType)
        {
            int high = 0;            

            while (occupation <= 84)
            {
                high++;

                if (day == "sabado" && high * checkType > 4)
                {
                    quantityAvalible = --high;
                    assignedDay = checkDay(day);
                    //_checkQuantity = checkQuantity(_quantity, quantityAvalible, _checkTypeVehicle);
                    high = 0;
                    occupation = 90;
                }
                else 
                {
                    if (high * checkType > 16)
                    {
                        quantityAvalible = --high;
                        assignedDay = checkDay(day);
                        //_checkQuantity = checkQuantity(_quantity, quantityAvalible, _checkTypeVehicle);
                        high = 0;
                        occupation = 90;
                    }
                }
            }    
        }

        private string checkDay(string day) 
        {
            string[] dayCheck = new string[6];

            dayCheck[0] = "lunes";
            dayCheck[1] = "martes";
            dayCheck[2] = "miercoles";
            dayCheck[3] = "jueves";
            dayCheck[4] = "viernes";
            dayCheck[5] = "sabado";

            for (int i = 0; i < dayCheck.Length; i++)
            {
                if (day == dayCheck[i]) 
                {
                    if (i == 5) 
                    {
                        return dayCheck[0];
                    }

                    return dayCheck[i + 1];
                }
            }
            return "";
        }


        private int checkQuantity(int quantity, int avalibleQuantity, int checkTypeVehicle)
        {
            if (checkTypeVehicle * quantity >= 16) 
            {
                return avalibleQuantity * checkTypeVehicle ;
            }
            return 0;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class clsDatos
    {
        #region "Atributos"

        private string name;
        private double payment;
        private int workDays;
        private double totalPaymed;
        private string error;

        #endregion


        #region "Constructor"

        public clsDatos()
        {
            this.name = name;
            this.payment = payment;
            this.workDays = workDays;
            this.error = error;
            this.totalPaymed = totalPaymed;
        }

        #endregion

        #region "Propiedades"

        public string setName
        { set { name = value; } }
        public double setPayment
        { set { payment = value; } }
        public int setWorkDays
        { set { workDays = value; } }

        
        public string getName
        { get { return name; } }
        public double getPayment
        { get { return payment; } }
        public double getWorkDays
        { get { return workDays; } }
        public double getTotalPaymed
        { get { return totalPaymed; } }
        public string getError
        { get { return error; } }

        #endregion

        #region "Metodos Privados"

        private bool validate()
        {
            if (this.name == string.Empty) 
            {
                this.error = "El nombre esta vacio, digitar nombre.. ";
            }
            if (this.payment < 0)
            {
                this.error = "El valoe de pago no es valido.. ";
            }
            if (this.workDays < 0)
            {
                this.error = "El valoe de dia no es valido.. ";
            }
            return true;
        }

        #endregion

        #region "Metodos Publicos"

        public bool calculate()
        {
            if (!validate())
            {
                return false;
            }

            try
            {
                this.totalPaymed = workDays * payment;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }


            return true;
        }

        #endregion

    }
}

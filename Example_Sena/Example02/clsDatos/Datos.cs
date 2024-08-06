using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDatos
{
    public class Datos
    {
        #region "Atributos"
         
        private double noteA;    
        private double noteB;
        private double noteC;
        private double noteD;
        private string error;

        //Variables globales
        private double average;
        private double STOCK = 4;
        #endregion


        #region "Constructor"

        public Datos() 
        {
            this.noteA = 0;
            this.noteB = 0;
            this.noteC = 0;
            this.noteD = 0;
            this.error = string.Empty;
        }

        #endregion


        #region "Propiedades"

        //entrada
        public double setNoteA
        { set { noteA = value; } }
        public double setNoteB
        { set { noteB = value; } }
        public double setNoteC
        { set { noteC = value; } }
        public double setNoteD
        { set { noteD = value; } }

        //salida
        public double getNoteA
        { get { return noteA; } }
        public double getNoteB
        { get { return noteB; } }
        public double getNoteC
        { get { return noteC; } }
        public double getNoteD
        { get { return noteD; } }
        public double getAverage
        { get { return average; } }
        public string getError
        { get { return error; } }

        #endregion

        #region "Metodos Privados"

        private bool validate()
        {
            if (this.noteA < 0 && this.noteA > 5)
            {
                this.error = "La nota A es Invalida";
                return false;
            }
            if (this.noteB < 0 && this.noteB > 5)
            {
                this.error = "La nota B es Invalida";
                return false;
            }
            if (this.noteC < 0 && this.noteC > 5)
            {
                this.error = "La nota C es Invalida";
                return false;
            }
            if (this.noteD < 0 && this.noteD > 5)
            {
                this.error = "La nota D es Invalida";
                return false;
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
                this.average = (noteA + noteB + noteC + noteD) / this.STOCK;
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

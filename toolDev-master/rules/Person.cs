using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace exampleo01.rules
{
    class Person
    {
        #region "Atribut"

        private string name;
        private string time;
        private string hour;
        private string teach;
        private string notes;
        private string error;

        #endregion

        #region "Constructor"

        public Person()
        {
            name = string.Empty;
            time = string.Empty;
            hour = string.Empty;
            teach = string.Empty;
            notes = string.Empty;
            error = string.Empty;
        }
        public Person(string name, string time, string hour, string teach, string notes)
        {
            this.name = name;
            this.time = time;
            this.hour = hour;
            this.teach = teach;
            this.notes = notes;
        }

        #endregion

        #region "Props"
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        public string Hour
        {
            get { return hour; }
            set { hour = value; }
        }

        public string Teach
        {
            get { return teach; }
            set { teach = value; }
        }

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public string Error
        {
            get { return error; }
        }

        #endregion 


        //private bool ReadDocs()
        //{
        //    try 
        //    {
        //        string Path = AppDomain.CurrentDomain.BaseDirectory + @"record.txt";
        //        int cant = 0;
        //        string[] arrayLine;
        //        string line;
        //        string code;

        //        cant = File.ReadAllLines(Path).Length;
        //        if( cant <= 0)
        //        {
        //            return true;
        //            StreamReader reader = new StreamReader( @Path );
        //            while( ( line = reader.ReadLine() ) != null )
        //            {
        //                arrayLine = line.Split(':');
        //                code = arrayLine[0];
        //                if( code == )
        //                {

        //                }
        //            }
        //        }
        //    }
        //    catch( Exception ex )
        //    {
        //        error = ex.Message;
        //        return false;
        //    }

        //}
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exampleo01.rules
{
    class Operations
    {
        private ArrayList personList = new ArrayList();

        public ArrayList List
        {
            get { return personList; }
            set { personList = value; }
        }
        public object information(string name, string theme, string time, string hour, string teach)
        {
            personList.Add(name);
            personList.Add(time);
            personList.Add(theme);
            personList.Add(hour);
            personList.Add(teach);

            return personList;
        }
    }
}

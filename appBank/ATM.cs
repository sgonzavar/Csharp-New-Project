using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consoleAppBank
{
    internal class ATM
    {
        public static string card = "", password = "", numc = "";
        public static ArrayList arraytest = new ArrayList();
        static void Main(string[] args)
        {
            People person1 = new People("pepito", "5353", "0000", 50000);
            People person2 = new People("pepito", "5353", "0000", 50000);
            People person3 = new People("pepito", "5353", "0000", 50000);
            People person4 = new People("pepito", "5353", "0000", 50000);
            People person5 = new People("pepito", "5353", "0000", 50000);

            arraytest.Add(person1);
            arraytest.Add(person2);
            arraytest.Add(person3);
            arraytest.Add(person4);
            arraytest.Add(person5);

            foreach (Object obj in arraytest)
            {
                Console.Write("{0}", obj);
                Console.ReadKey();
            }
            Console.WriteLine("test to print {0}", arraytest.ToString());
            Console.ReadKey();

            for (int  index = 0;  index < arraytest.Count;  index++)
            {
                object o = arraytest[index];
                Console.WriteLine("{0}", o.);
            }
                Console.ReadKey();
        }
    }
}

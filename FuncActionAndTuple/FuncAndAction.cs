using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncActionAndTuple
{
    public class FuncAndAction
    {

        public FuncAndAction()
        {
            Action<string> RunAction = saludar;

            RunAction += despedir;

            RunAction += despedir;

            RunAction("Yael");
        }
        public static char Run(string palabra)
        {
            return PrimerRepeditoFunc(palabra);


        }


        public char Run(string palabra, Func<string, char> algortimo)
        {
            return algortimo(palabra);


        }
        public static char PrimerNoRepetidoClassic(string palabra)
        {
            int i, j;
            bool isRepeted = false;
            char[] chars = palabra.ToCharArray();
            for (i = 0; i < chars.Length; i++)
            {
                isRepeted = false;
                for (j = 0; j < chars.Length; j++)
                {
                    if ((i != j) && (chars[i] == chars[j]))
                    {
                        isRepeted = true;
                        break;
                    }
                }
                if (isRepeted == false)
                {
                    return chars[i];
                }
            }
            return ' ';
        }

        static Func<string, char> PrimerRepeditoFunc = PrimerNoRepetidoClassic;

        static void saludar(string nombre)
        {
            Console.WriteLine($"Hola {nombre}");
        }

        static void despedir(string nombre)
        {
            Console.WriteLine($"Adios {nombre}");
        }

        Predicate<string> stringPredicate = (s) => s == "h";

        /*= (s) => 
    s.ToArray()
    .GroupBy(x=> x)
    .Where(x=> x.Count() == 1)
    .Select(x => x.Key)
    .FirstOrDefault();*/
    }
}

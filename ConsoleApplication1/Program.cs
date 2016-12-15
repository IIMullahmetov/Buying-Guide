using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var database = new Database())
            {
                database.Users.Add(new Users {ID = 1, NAME = "dfdfds", AGE = 54});
                Test test = new Test();
                MethodReflectInfo(test);
                Console.ReadKey();  
            }
        }

        public static void MethodReflectInfo<T>(T obj) where T : class
        {
            Type type = typeof(T);
            MethodInfo[] MArr = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            foreach (var method in MArr)
            {
                Console.WriteLine(method.ReturnType + "   " + method.Name);
            }
        }
    }
}

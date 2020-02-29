using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rito;
using System.Reflection;

namespace RitoCSharpLibrary.Study
{
    // 200229 토


    public class ReflectionStudy : StudyBase
    {
        public override void Run()
        {
            base.Run();
        }

        protected override void Method()
        {
            var thisType = typeof(ReflectionStudy);

            var allMembers = thisType.GetMembers( BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic); ;
            var allPublicMembers = thisType.GetMembers(BindingFlags.FlattenHierarchy); // 띠요오오오옹?????????
            var allNonPublicMembers = thisType.GetMembers();




            var allInstanceMembers = thisType.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var allStaticMembers   = thisType.GetMembers(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            var publicMembers      = thisType.GetMembers();
            var nonPublicMembers   = thisType.GetMembers(BindingFlags.Instance | BindingFlags.NonPublic);

            var methods = thisType.GetMethods();
            var fields = thisType.GetFields(BindingFlags.Public | BindingFlags.NonPublic);

            var publicMethod = thisType.GetMethod("PublicMethod");
            var protectedMethod = thisType.GetMethod("ProtectedMethod");
            var privateMethod = thisType.GetMethod("PrivateMethod");

            foreach (var item in allPublicMembers)
            {
                Console.WriteLine(item);
            }

            0.Ex_Enter();
            0.Ex_PrintLine(60, true);
            Console.WriteLine(publicMethod);
            publicMethod.Invoke(this, null);
        }

        // =============================================================

        public int       _intField = 1;
        protected string _stringField = "String 1";
        private bool     _boolField = true;

        public static int       static_intField = 2;
        protected static string static_stringField = "Static String 1";
        private static bool     static_boolField = true;

        public void PublicMethod()
        {
            Console.WriteLine("Call : public void PublicMethod()");
        }

        protected int ProtectedMethod()
        {
            Console.WriteLine("Call : protected int ProtectedMethod()");
            return 0;
        }

        private bool PrivateMethod()
        {
            Console.WriteLine("Call : private bool PrivateMethod()");
            return false;
        }

        public static void Static_PublicMethod()
        {
            Console.WriteLine("Call : [Static] public void Static_PublicMethod()");
        }

        protected static int Static_ProtectedMethod()
        {
            Console.WriteLine("Call : [Static] protected int Static_ProtectedMethod()");
            return 0;
        }

        private static bool Static_PrivateMethod()
        {
            Console.WriteLine("Call : [Static] private bool Static_PrivateMethod()");
            return false;
        }
    }
}

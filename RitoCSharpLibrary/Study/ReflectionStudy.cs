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

            // 1. Members

            Console.WriteLine("====== Ex_Rf_GetAllMembers ======");
            foreach (var item in thisType.Ex_Rf_GetAllMembers()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetAllPublicMembers ======");
            foreach (var item in thisType.Ex_Rf_GetAllPublicMembers()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetPublicInstanceMembers ======");
            foreach (var item in thisType.Ex_Rf_GetPublicInstanceMembers()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetPublicStaticMembers ======");
            foreach (var item in thisType.Ex_Rf_GetPublicStaticMembers()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetAllNonPublicMembers ======");
            foreach (var item in thisType.Ex_Rf_GetAllNonPublicMembers()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetNonPublicInstanceMembers ======");
            foreach (var item in thisType.Ex_Rf_GetNonPublicInstanceMembers()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetNonPublicStaticMembers ======");
            foreach (var item in thisType.Ex_Rf_GetNonPublicStaticMembers()) Console.WriteLine(item);
            0.Ex_Enter(4); 0.Ex_PrintDooubleLine(60, true);

            // 2. Fields

            Console.WriteLine("====== Ex_Rf_GetAllFields ======");
            foreach (var item in thisType.Ex_Rf_GetAllFields()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetAllPublicFields ======");
            foreach (var item in thisType.Ex_Rf_GetAllPublicFields()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetPublicInstanceFields ======");
            foreach (var item in thisType.Ex_Rf_GetPublicInstanceFields()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetPublicStaticFields ======");
            foreach (var item in thisType.Ex_Rf_GetPublicStaticFields()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetAllNonPublicFields ======");
            foreach (var item in thisType.Ex_Rf_GetAllNonPublicFields()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetNonPublicInstanceFields ======");
            foreach (var item in thisType.Ex_Rf_GetNonPublicInstanceFields()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetNonPublicStaticFields ======");
            foreach (var item in thisType.Ex_Rf_GetNonPublicStaticFields()) Console.WriteLine(item);
            0.Ex_Enter(4); 0.Ex_PrintDooubleLine(60, true);

            // 3. Methods

            Console.WriteLine("====== Ex_Rf_GetAllMethods ======");
            foreach (var item in thisType.Ex_Rf_GetAllMethods()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetAllPublicMethods ======");
            foreach (var item in thisType.Ex_Rf_GetAllPublicMethods()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetPublicInstanceMethods ======");
            foreach (var item in thisType.Ex_Rf_GetPublicInstanceMethods()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetPublicStaticMethods ======");
            foreach (var item in thisType.Ex_Rf_GetPublicStaticMethods()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetAllNonPublicMethods ======");
            foreach (var item in thisType.Ex_Rf_GetAllNonPublicMethods()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetNonPublicInstanceMethods ======");
            foreach (var item in thisType.Ex_Rf_GetNonPublicInstanceMethods()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetNonPublicStaticMethods ======");
            foreach (var item in thisType.Ex_Rf_GetNonPublicStaticMethods()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            // 4. Properties

            Console.WriteLine("====== Ex_Rf_GetAllProperties ======");
            foreach (var item in thisType.Ex_Rf_GetAllProperties()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetAllPublicProperties ======");
            foreach (var item in thisType.Ex_Rf_GetAllPublicProperties()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetPublicInstanceProperties ======");
            foreach (var item in thisType.Ex_Rf_GetPublicInstanceProperties()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetPublicStaticProperties ======");
            foreach (var item in thisType.Ex_Rf_GetPublicStaticProperties()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetAllNonPublicProperties ======");
            foreach (var item in thisType.Ex_Rf_GetAllNonPublicProperties()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetNonPublicInstanceProperties ======");
            foreach (var item in thisType.Ex_Rf_GetNonPublicInstanceProperties()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);

            Console.WriteLine("====== Ex_Rf_GetNonPublicStaticProperties ======");
            foreach (var item in thisType.Ex_Rf_GetNonPublicStaticProperties()) Console.WriteLine(item);
            0.Ex_Enter(); 0.Ex_PrintLine(60, true);
        }

        // =============================================================

        // 1. 필드
        public int       _intField = 1;
        protected string _stringField = "String 1";
        private bool     _boolField = true;

        public static int       static_intField = 2;
        protected static string static_stringField = "Static String 1";
        private static bool     static_boolField = true;

        // 2. 프로퍼티
        public int _intProperty { get; } = 1;
        protected string _stringProperty { get; } = "String 1";
        private bool _boolProperty { get; } = true;

        public static int static_intProperty { get; } = 2;
        protected static string static_stringProperty { get; } = "Static String 1";
        private static bool static_boolProperty { get; } = true;


        // 3. 메소드
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

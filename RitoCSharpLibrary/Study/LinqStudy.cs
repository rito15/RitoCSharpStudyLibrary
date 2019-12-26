using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RitoCSharpLibrary.Study
{
    // 191118,
    class LinqStudy : StudyClass
    {
        public override void Run()
        {
            // Method();
        }

        protected override void Method()
        {
            int[] intArr = { 0, 1, 2, 3, 4, 5, 6 };
            IEnumerable<int> intIE = new int[] { 1, 2, 3 };

            var smallArr = from integer in intArr
                           where integer % 3 == 1
                           select integer;

            var smallArr2 = from integer in intIE
                            where integer <= 2
                            select integer;

            foreach(var a in smallArr)
            {
                Console.Write($"{a} ");
            }
            Rito.RitoConsole.Enter();
            Rito.RitoConsole.PrintLine(20);

            foreach (var a in smallArr2)
            {
                Console.Write($"{a} ");
            }
            Rito.RitoConsole.Enter();
            Rito.RitoConsole.PrintLine(20, true);

            var intList = smallArr.ToList();
            var intArray = smallArr.ToArray();
            var intArray2 = GetMyNumbers().ToArray();


            var smallArr3 = from a in intArr
                            from b in intIE
                            from c in GetMyNumbers()
                            where a > 0 || b < 0 || c < 0
                            select c;

            Rito.RitoConsole.PrintArray(smallArr3);
        }

        IEnumerable<int> GetMyNumbers()
        {
            for (int i = 0; i < 10; i++)
                yield return i * 2;
        }
    }
}

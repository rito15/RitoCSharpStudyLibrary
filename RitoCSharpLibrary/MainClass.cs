﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rito;

namespace RitoCSharpLibrary
{
    class MainClass
    {
        public static void Main()
        {
            Study.StudyClass[] sc =
            {
                new Study.TupleStudy(),         // 191108 금
                new Study.StringStudy(),        // 191110 일
                new Study.IteratorStudy(),      // 191111 월 ~
                new Study.LinqStudy(),          // 191118 월 ~
                new Study.IndexerStudy(),       // 191216 월
                new Study.PatternMatching(),    // 191218 수
            };

            foreach (var s in sc)
                s.Run();
        }

        public static void Main_1(string[] args)
        {
            

            RitoConsole.PrintLine(80); RitoConsole.Enter(3);

            var strList1 = "apapapaaweweajwa".Split('a');

            List<string> strList2 = new List<string>();

            RitoString.Tokenize(ref strList2, "apapapaaweweajwa", 'a');

            RitoConsole.PrintArray(strList1, 1, 1, 5, true);
            RitoConsole.PrintArray(strList2, 4, 3, 5, true);
            
            Console.WriteLine(RitoString.Substring("apodwjapwdj", -22, 3));
        }

        // 191129_0228 - for문, foreach문, RitoSmarts.Foreach, RitoSmarts.ForeachRef 시간 측정 테스트
        // 결과 : 유의미한 시간 차이는 없음
        // 장점 1 : foreach와 같은 효과이지만 foreach처럼 IEnumerator로 인한 쓰레기 메모리 잡아먹지 않음
        // 장점 2 : 따라서 가비지 콜렉터 호출을 줄임
        // 장점 3★ : foreach는 원래 읽기 전용으로 엘리먼트를 참조하지만
        //            RitoSmarts.ForeachRef를 이용해 수정 가능!!!
        public static void Main_2()
        {
            IEnumerable<int> intEnum = new int[] { 1, 2, 3, 4, 5 };
            int[] intArray = { 5, 4, 3, 2, 1 };
            List<int> intList = new List<int>{ 6, 7, 8, 9, 10 };

            //RitoSmarts.Foreach(intEnum, a => { Console.WriteLine($"1 : {a}"); });
            //RitoSmarts.Foreach(intArray, a => { Console.WriteLine($"2 : {a}"); });
            //RitoSmarts.Foreach(intList, a => { Console.WriteLine($"3 : {a}"); });

            int[] intArr1 = new int[50000]; int index = 0;
            RitoSmarts.ForeachRef(intArr1, (ref int a) => a = index++);

            const int SWITCH = 4;

            if (SWITCH == 1)
            {
                RitoTester.TimeCheckStart();
                RitoSmarts.Foreach(intArr1, a => Console.WriteLine(a));
                RitoTester.TimeCheckStop(true);
            }
            if (SWITCH == 2)
            {
                RitoTester.TimeCheckStart();
                foreach (var a in intArr1) Console.WriteLine(a);
                RitoTester.TimeCheckStop(true);
            }
            if (SWITCH == 3)
            {
                RitoTester.TimeCheckStart();
                RitoSmarts.ForeachRef(intArr1, (ref int a) => Console.WriteLine(a));
                RitoTester.TimeCheckStop(true);
            }

            var intArray2 = intList.ToArray();

            // foreach로 각 요소를 변경시킬 수는 없음
            RitoSmarts.Foreach(intArray2, a => a = 1);
            RitoConsole.PrintArray(intArray2);

            // ★ForeachRef를 사용하면 가능!
            RitoSmarts.ForeachRef(intArray2, (ref int a) => a = 2);
            RitoConsole.PrintArray(intArray2);

            // List<int> 역시 foreach로 각 요소를 변경시킬 수는 없음
            RitoSmarts.Foreach(intList, a => a = 3);
            RitoConsole.PrintArray(intList);

            // 이미 List<T>에는 메소드 형태의 Foreach가 타입스크립트처럼 존재
            intList.ForEach(a => a = 4);
            RitoConsole.PrintArray(intList);

            // 그런데 List<T>는 ref로 참조 못함
            // ★따라서 RitoSmart에서 List를 Array로 변경하여 우회, ref로 참조 가능케 함!
            RitoSmarts.ForeachRef(ref intList, (ref int a) => a = 3);
            RitoConsole.PrintArray(intList);

            var arrrr = RitoSmarts.ForeachRef((ref int a) => a = a + 5, 2, 3, 4, 5, 6);
            RitoConsole.PrintArray(arrrr);
        }

        // 191215 - 정규식
        public static void Main_3()
        {
            Console.WriteLine(RitoRegex.IsOnlyAlphabet("AbceD"));
            Console.WriteLine(RitoRegex.IsOnlyAlphaDigit("Abce62Da"));
            Console.WriteLine(RitoRegex.IsOnlyDigit("1234"));
            Console.WriteLine(RitoRegex.IsOnlyLowerCase("avca"));

            Console.WriteLine(RitoRegex.CheckFormat("-122.0", RitoRegex._FloatNumberFormat));
            Console.WriteLine(RitoRegex.CheckFormat("010-2123-3414", RitoRegex._PhoneNumberFormat));
            Console.WriteLine(RitoRegex.CheckFormat("AAAB", new System.Text.RegularExpressions.Regex(@"^A*B$")));

            Console.WriteLine(RitoRegex.IsEmail("tkals22.22@naver.com"));
            Console.WriteLine(RitoRegex.IsEmail("tkals22.22_a@naver.com"));

            Console.WriteLine(RitoRegex.IsNumber("-202"));
            Console.WriteLine(RitoRegex.IsFloatNumber("-2.02"));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rito;

namespace RitoCSharpLibrary
{
    class MainClass
    {
        // .cs 속성 - 빌드작업 [컴파일] / [없음] 택1
        public static void Main()
        {
            //Study.StudyBase[] sc =
            //{
            //    new Study.TupleStudy(),             // 191108 금
            //    new Study.StringStudy(),            // 191110 일
            //    new Study.IteratorStudy(),          // 191111 월 ~
            //    new Study.LinqStudy(),              // 191118 월 ~
            //    new Study.IndexerStudy(),           // 191216 월
            //    new Study.PatternMatchingStudy(),   // 191218 수
            //    new Study.EnumStudy(),              // 200217 월
            //    new Study.ReflectionStudy(),        // 200229 토
            //};

            //foreach (var s in sc)
            //{
            //    s.Run();
            //}

            //Main_7();
            //Main_2020_01_21_ForeachExtension();
            //Main_2020_01_25_Extensions();
            //Main_2020_01_26_Extensions();

            var studyList = typeof(RitoStudyClassAttribute).Ex_GetAttributeUsedClasses("Study");

            foreach (var type in studyList)
            {
                object instance = Activator.CreateInstance(type);
                var attrs = type.GetCustomAttributes(typeof(RitoStudyClassAttribute), false);

                foreach (var attr in attrs)
                {
                    (attr as RitoStudyClassAttribute)?.PrintTitle();
                }

                foreach (var method in type.Ex_GetAttributeUsedMethods(typeof(RitoStudyMethodAttribute)))
                {
                    Console.WriteLine($"<{method.methodInfo.Name}>");
                    method.methodInfo.Invoke(instance, new object[] { });

                    Console.WriteLine("");
                }
            }
        }

        #region Previous Main Methods

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
            List<int> intList = new List<int> { 6, 7, 8, 9, 10 };

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

            Console.WriteLine(RitoRegex.CheckFormat("-122.0", RitoRegex._RealNumberFormat));
            Console.WriteLine(RitoRegex.CheckFormat("010-2123-3414", RitoRegex._PhoneNumberFormat));
            Console.WriteLine(RitoRegex.CheckFormat("AAAB", new System.Text.RegularExpressions.Regex(@"^A*B$")));

            Console.WriteLine(RitoRegex.IsEmail("tkals22.22@naver.com"));
            Console.WriteLine(RitoRegex.IsEmail("tkals22.22_a@naver.com"));

            Console.WriteLine(RitoRegex.IsNumber("-202"));
            Console.WriteLine(RitoRegex.IsFloatNumber("-2.02"));
        }

        // 200117 - 정규식
        public static void Main_4()
        {
            Console.WriteLine(RitoRegex.Substring(@"Samin\AAAA\BBBB\Tester.cs", @"\\", ".cs", @"\\"));
            Console.WriteLine(RitoRegex.Substring(@"SaminAAAABBBBTester.cs", @"B", ".cs"));

            Console.WriteLine(RitoRegex.GetDirectoryPath(@"Samin\A0001\B1234\C Sh arp.cs"));
            Console.WriteLine(RitoRegex.GetDirectoryPath(@"Samin\A0001\B1234\C Sh arp.cs", true));

            Console.WriteLine(RitoRegex.GetFileName(@"Samin\A0001\B1234\C Sh arp.cs"));

            Console.WriteLine(RitoRegex.GetExtension(@"Samin\A0001\B1234\C Sh arp.cs"));
            Console.WriteLine(RitoRegex.GetExtension(@"Samin\A0001\B1234\C Sh arp.cs", true));

            Console.WriteLine(RitoRegex.GetFileNameExtension(@"Samin\A0001\B1234\C Sh arp.cs"));
        }

        // 200119 - 정규식 Find
        // 결론 - string.Contains()가 Regex.Find()보다 성능이 좋다(패턴이 아닌 정확한 문자열에 한해서)
        public static void Main_5()
        {
            int count = 5000;
            var org = @"Samin\A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A"
                    + @"001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A"
                    + @"001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A"
                    + @"001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A"
                    + @"001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A"
                    + @"001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A"
                    + @"001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A"
                    + @"001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A"
                    + @"0001\B1234\C Sh arp.cs";

            var org2 = "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "apple monkey rito regex lineapple monkey rito regex lineapple monkey rito regex lineapple monkey rito regex lineapple monkey rito regex line"
                     + "apple monkey rito regex lineapple monkey rito regex lineapple monkey rito regex lineapple monkey rito regex lineapple monkey rito regex line";

            RitoTester.TimeCheckStart();
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(RitoRegex.Find(org2, "rito") + " - true");
                Console.WriteLine(RitoRegex.Find(org2, new string[] { "rito", "mon" }) + " - true");
                Console.WriteLine(RitoRegex.Find(org2, new string[] { "rito", "mown" }) + " - false");

                Console.WriteLine(RitoRegex.Find(org2, new string[] { "rito", "lin", "ap", "key", "re", "gex", "ne"}) + " - true");


                //Console.WriteLine(RitoRegex.GetFileName(org));
            }
            var test1 = RitoTester.TimeCheckStop();

            RitoTester.TimeCheckStart();
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(org2.Contains("rito") + " - true");
                Console.WriteLine((org2.Contains("rito") && org2.Contains("mon")) + " - true");
                Console.WriteLine((org2.Contains("rito") && org2.Contains("mown")) + " - false");

                Console.WriteLine((
                    org2.Contains("rito") &&
                    org2.Contains("lin") &&
                    org2.Contains("ap") &&
                    org2.Contains("key") &&
                    org2.Contains("re") &&
                    org2.Contains("gex") &&
                    org2.Contains("ne")) + " - true");

                //string aa = org;
                //int start = aa.LastIndexOf(@"\");
                //int end = aa.IndexOf(@".");

                //Console.WriteLine(aa.Substring(start + 1, end - start - 1));
            }
            var test2 = RitoTester.TimeCheckStop();

            Console.WriteLine("\n" + $"[1] {test1}, [2] {test2}");
        }

        // 200119 - 정규식 Replace - 성능 측정
        public static void Main_6()
        {
            int count = 5000;
            var org = @"Samin\A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A"
                    + @"001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A"
                    + @"001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A"
                    + @"001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A"
                    + @"001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A"
                    + @"001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A"
                    + @"001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A"
                    + @"001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A0001A"
                    + @"0001\B1234\C Sh arp.cs";

            var org2 = "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "apple monkey rito regex lineapple monkey rito regex lineapple monkey rito regex lineapple monkey rito regex lineapple monkey rito regex line"
                     + "apple monkey rito regex lineapple monkey rito regex lineapple monkey rito regex lineapple monkey rito regex lineapple monkey rito regex line";

            var org3 = "adaw90duaw09faw9fwa09faw09hfaw09hfa09hfaw09fahw09fawh09fawh09afwh09afwhf0a9hfa0hawf09hwaf09awhfa0w9hfwa09fhaw09fhaw09hfawdasdasfafwg"
                     + "apple monkey rito regex lineapple monkey rito regex lineapple monkey rito regex lineapple monkey rito regex lineapple monkey rito regex line";

            RitoTester.TimeCheckStart();
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(org2.Replace("90", "**").Replace("monkey", "***").Replace("rito", "***"));
                //Console.WriteLine(org2.Replace("90", "**").Replace("aw","**"));
            }
            var test1 = RitoTester.TimeCheckStop();

            RitoTester.TimeCheckStart();
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(System.Text.RegularExpressions.Regex.Replace(org2, "90", "**"));
                //Console.WriteLine(RitoRegex.Replace(org2, new string[] { "90", "aw" }, "**"));
            }
            var test2 = RitoTester.TimeCheckStop();

            Console.WriteLine("\n" + $"[1] {test1}, [2] {test2}");
        }

        // 200119 - 정규식 Find, Replace - 대소문자 구분 테스트
        public static void Main_7()
        {
            string org1 = "ritoRitorItoriToritO tori roti";

            Console.WriteLine(RitoRegex.Find(org1, "RITO", true));
            Console.WriteLine(RitoRegex.Find(org1, "RITO", false));

            Console.WriteLine(RitoRegex.Replace(org1, "rito", "****", true));
            Console.WriteLine(RitoRegex.Replace(org1, "rito", "****", false));

            Console.WriteLine(RitoRegex.Replace(org1, "rito", '*', true));
            Console.WriteLine(RitoRegex.Replace(org1, "rito", '*', false));

            Console.WriteLine(RitoRegex.Replace(org1, new string[] { "ri", "o" }, "--", true));
            Console.WriteLine(RitoRegex.Replace(org1, new string[] { "ri", "o" }, "--", false));

            Console.WriteLine(RitoRegex.Replace(org1, new string[] { "ri", "o" }, '*', true));
            Console.WriteLine(RitoRegex.Replace(org1, new string[] { "ri", "o" }, '*', false));
        }

        public static void Main_2020_01_21_ForeachExtension()
        {
            int[] arr = { 0, 1, 2, 3, 4, 5 };

            arr.Ex_Foreach((a) => { a++; });
            RitoConsole.PrintArray(arr);

            arr.Ex_ForeachRef((ref int a) => { a++; });
            RitoConsole.PrintArray(arr);
        }

        public static void Main_2020_01_25_Extensions()
        {
            Console.WriteLine("========================Clamp========================");
            int    i10 = 10, i15 = 15, i20 = 20;
            float  f10 = 10, f15 = 15, f20 = 20;
            double d10 = 10, d15 = 15, d20 = 20;

            // 1. Clamp 동작 확인
            TestWrite(i10.Ex_Clamp(15,20), 15, false);
            TestWrite(i15.Ex_Clamp(0, 10), 10, false);
            TestWrite(f10.Ex_Clamp(15,20), 15, false);
            TestWrite(f15.Ex_Clamp(0, 10), 10, false);
            TestWrite(d10.Ex_Clamp(15,20), 15, false);
            TestWrite(d15.Ex_Clamp(0, 10), 10, false);

            // 2. ref로 값 변경되는지 확인
            TestWrite(i10, 15, false);
            TestWrite(f10, 15, false);
            TestWrite(d10, 15, false);

            Console.WriteLine("========================POW========================");
            int a = 5; float b = 6f; double c = 7.0;

            TestWrite(a.Ex_Pow(3), 125, false);
            TestWrite(b.Ex_Pow(3), 216, false);
            TestWrite(c.Ex_Pow(2), 49, false);

            Console.WriteLine("========================Digitalize========================");
            int    i_10 = -10, i0 = 0; i10 = 10;
            float  f_10 = -10, f0 = 0; f10 = 10;
            double d_10 = -10, d0 = 0; d10 = 10;

            TestWrite(i_10.Ex_Digitalize(), 1, false);
            TestWrite(i0.Ex_Digitalize(),   0, false);
            TestWrite(i10.Ex_Digitalize(),  1, false);

            TestWrite(f_10.Ex_Digitalize(), 1, false);
            TestWrite(f0.Ex_Digitalize(),   0, false);
            TestWrite(f10.Ex_Digitalize(),  1, false);

            TestWrite(d_10.Ex_Digitalize(), 1, false);
            TestWrite(d0.Ex_Digitalize(),   0, false);
            TestWrite(d10.Ex_Digitalize(),  1, false);

            TestWrite(i_10.Ex_SignedDigitalize(), -1, false);
            TestWrite(i0.Ex_SignedDigitalize(),   0, false);
            TestWrite(i10.Ex_SignedDigitalize(),  1, false);

            TestWrite(f_10.Ex_SignedDigitalize(), -1, false);
            TestWrite(f0.Ex_SignedDigitalize(),   0, false);
            TestWrite(f10.Ex_SignedDigitalize(),  1, false);

            TestWrite(d_10.Ex_SignedDigitalize(), -1, false);
            TestWrite(d0.Ex_SignedDigitalize(),   0, false);
            TestWrite(d10.Ex_SignedDigitalize(),  1, false);


            Console.WriteLine("========================String Format Checkers========================");
            
            // 알파벳 대소문자
            TestWrite("ABCabc".Ex_IsOnlyAlphabet(), true, false);
            TestWrite("ABCABC".Ex_IsOnlyAlphabet(), true, false);
            TestWrite("abcabc".Ex_IsOnlyAlphabet(), true, false);
            TestWrite("ABC123".Ex_IsOnlyAlphabet(), false, false);
            TestWrite("abc123".Ex_IsOnlyAlphabet(), false, false);
            TestWrite("123123".Ex_IsOnlyAlphabet(), false, false);
            TestWrite("ABab_+".Ex_IsOnlyAlphabet(), false, false);


            // 대문자
            TestWrite("ABCABC".Ex_IsOnlyUpperAlphabet(), true, false);
            TestWrite("ABCabc".Ex_IsOnlyUpperAlphabet(), false, false);
            TestWrite("abcabc".Ex_IsOnlyUpperAlphabet(), false, false);
            TestWrite("ABC123".Ex_IsOnlyUpperAlphabet(), false, false);
            TestWrite("abc123".Ex_IsOnlyUpperAlphabet(), false, false);
            TestWrite("ABab_+".Ex_IsOnlyUpperAlphabet(), false, false);


            // 소문자
            TestWrite("abcabc".Ex_IsOnlyLowerAlphabet(), true, false);
            TestWrite("ABCABC".Ex_IsOnlyLowerAlphabet(), false, false);
            TestWrite("ABCabc".Ex_IsOnlyLowerAlphabet(), false, false);
            TestWrite("ABC123".Ex_IsOnlyLowerAlphabet(), false, false);
            TestWrite("abc123".Ex_IsOnlyLowerAlphabet(), false, false);
            TestWrite("ABab_+".Ex_IsOnlyLowerAlphabet(), false, false);


            // 알파벳 대소문자, 정수
            TestWrite("abcabc".Ex_IsOnlyAlphaDigit(), true, false);
            TestWrite("ABCABC".Ex_IsOnlyAlphaDigit(), true, false);
            TestWrite("ABCabc".Ex_IsOnlyAlphaDigit(), true, false);
            TestWrite("ABC123".Ex_IsOnlyAlphaDigit(), true, false);
            TestWrite("abc123".Ex_IsOnlyAlphaDigit(), true, false);
            TestWrite("123123".Ex_IsOnlyAlphaDigit(), true, false);
            TestWrite("ABab_+".Ex_IsOnlyAlphaDigit(), false, false);


            // 양의 정수(0~9)
            TestWrite("123123".Ex_IsOnlyDigit(), true, false);
            TestWrite("123.23".Ex_IsOnlyDigit(), false, false);
            TestWrite("abcabc".Ex_IsOnlyDigit(), false, false);
            TestWrite("ABCABC".Ex_IsOnlyDigit(), false, false);
            TestWrite("ABCabc".Ex_IsOnlyDigit(), false, false);
            TestWrite("ABC123".Ex_IsOnlyDigit(), false, false);
            TestWrite("abc123".Ex_IsOnlyDigit(), false, false);
            TestWrite("ABab_+".Ex_IsOnlyDigit(), false, false);
            TestWrite("-23123".Ex_IsOnlyDigit(), false, false);


            // 양, 음의 정수
            TestWrite("123123".Ex_IsOnlyInteger(), true, false);
            TestWrite("-23123".Ex_IsOnlyInteger(), true, false);
            TestWrite("+23123".Ex_IsOnlyInteger(), true, false);
            TestWrite("23-123".Ex_IsOnlyInteger(), false, false);
            TestWrite("23.123".Ex_IsOnlyInteger(), false, false);
            TestWrite("-3.123".Ex_IsOnlyInteger(), false, false);
            TestWrite("abcabc".Ex_IsOnlyInteger(), false, false);
            TestWrite("ABCABC".Ex_IsOnlyInteger(), false, false);
            TestWrite("ABCabc".Ex_IsOnlyInteger(), false, false);
            TestWrite("ABC123".Ex_IsOnlyInteger(), false, false);
            TestWrite("abc123".Ex_IsOnlyInteger(), false, false);
            TestWrite("ABab_+".Ex_IsOnlyInteger(), false, false);


            // 양, 음의 실수
            TestWrite("123.123".Ex_IsOnlyRealNumber(), true, false);
            TestWrite("+23.123".Ex_IsOnlyRealNumber(), true, false);
            TestWrite("-23.123".Ex_IsOnlyRealNumber(), true, false);
            TestWrite("1231231".Ex_IsOnlyRealNumber(), false, false);
            TestWrite("-231231".Ex_IsOnlyRealNumber(), false, false);
            //Console.WriteLine("");

            TestWrite("+231231".Ex_IsOnlyRealNumber(), false, false);
            TestWrite(".231231".Ex_IsOnlyRealNumber(), false, false);
            TestWrite("123123.".Ex_IsOnlyRealNumber(), false, false);
            TestWrite("123+231".Ex_IsOnlyRealNumber(), false, false);
            TestWrite("23-1231".Ex_IsOnlyRealNumber(), false, false);
            //Console.WriteLine("");

            TestWrite("abcabc".Ex_IsOnlyRealNumber(), false, false);
            TestWrite("ABCABC".Ex_IsOnlyRealNumber(), false, false);
            TestWrite("ABCabc".Ex_IsOnlyRealNumber(), false, false);
            TestWrite("ABC123".Ex_IsOnlyRealNumber(), false, false);
            TestWrite("abc123".Ex_IsOnlyRealNumber(), false, false);
            TestWrite("ABab_+".Ex_IsOnlyRealNumber(), false, false);
            //Console.WriteLine("");


            // 양, 음의 정수, 실수
            TestWrite("123.123".Ex_IsOnlyNumber(), true, false);
            TestWrite("+23.123".Ex_IsOnlyNumber(), true, false);
            TestWrite("-23.123".Ex_IsOnlyNumber(), true, false);
            TestWrite("1231231".Ex_IsOnlyNumber(), true, false);
            TestWrite("-231231".Ex_IsOnlyNumber(), true, false);
            //Console.WriteLine("");

            TestWrite("+23123".Ex_IsOnlyNumber(), true, false);
            TestWrite(".23123".Ex_IsOnlyNumber(), false, false);
            TestWrite("12312.".Ex_IsOnlyNumber(), false, false);
            TestWrite("123+23".Ex_IsOnlyNumber(), false, false);
            TestWrite("23-123".Ex_IsOnlyNumber(), false, false);
            //Console.WriteLine("");

            TestWrite("abcabc".Ex_IsOnlyNumber(), false, false);
            TestWrite("ABCABC".Ex_IsOnlyNumber(), false, false);
            TestWrite("ABCabc".Ex_IsOnlyNumber(), false, false);
            TestWrite("ABC123".Ex_IsOnlyNumber(), false, false);
            TestWrite("abc123".Ex_IsOnlyNumber(), false, false);
            TestWrite("ABab_+".Ex_IsOnlyNumber(), false, false);
            //Console.WriteLine("");


            // 양의 정수, 실수
            TestWrite("123.123".Ex_IsOnlyPositiveNumber(), true, false);
            TestWrite("+23.123".Ex_IsOnlyPositiveNumber(), true, false);
            TestWrite("1231231".Ex_IsOnlyPositiveNumber(), true, false);
            TestWrite("+231231".Ex_IsOnlyPositiveNumber(), true, false);
            TestWrite("+23123.".Ex_IsOnlyPositiveNumber(), false, false);
            TestWrite("-23.123".Ex_IsOnlyPositiveNumber(), false, false);
            //Console.WriteLine("");

            TestWrite("-23123".Ex_IsOnlyPositiveNumber(), false, false);
            TestWrite(".23123".Ex_IsOnlyPositiveNumber(), false, false);
            TestWrite("12312.".Ex_IsOnlyPositiveNumber(), false, false);
            TestWrite("123+23".Ex_IsOnlyPositiveNumber(), false, false);
            TestWrite("23-123".Ex_IsOnlyPositiveNumber(), false, false);
            //Console.WriteLine("");

            TestWrite("abcabc".Ex_IsOnlyPositiveNumber(), false, false);
            TestWrite("ABCABC".Ex_IsOnlyPositiveNumber(), false, false);
            TestWrite("ABCabc".Ex_IsOnlyPositiveNumber(), false, false);
            TestWrite("ABC123".Ex_IsOnlyPositiveNumber(), false, false);
            TestWrite("abc123".Ex_IsOnlyPositiveNumber(), false, false);
            TestWrite("ABab_+".Ex_IsOnlyPositiveNumber(), false, false);
            //Console.WriteLine("");


            // 음의 정수, 실수
            TestWrite("-23.123".Ex_IsOnlyNegativeNumber(), true, false);
            TestWrite("-231231".Ex_IsOnlyNegativeNumber(), true, false);
            TestWrite("-1".Ex_IsOnlyNegativeNumber(),      true, false);
            TestWrite("-1.0".Ex_IsOnlyNegativeNumber(),    true, false);
            TestWrite("-2.3123".Ex_IsOnlyNegativeNumber(), true, false);
            //Console.WriteLine("");

            TestWrite("-1.2.2".Ex_IsOnlyNegativeNumber(), false, false);
            TestWrite("+23123".Ex_IsOnlyNegativeNumber(), false, false);
            TestWrite(".23123".Ex_IsOnlyNegativeNumber(), false, false);
            TestWrite("12312.".Ex_IsOnlyNegativeNumber(), false, false);
            TestWrite("123+23".Ex_IsOnlyNegativeNumber(), false, false);
            TestWrite("23-123".Ex_IsOnlyNegativeNumber(), false, false);
            //Console.WriteLine("");

            TestWrite("abcabc".Ex_IsOnlyNegativeNumber(), false, false);
            TestWrite("ABCABC".Ex_IsOnlyNegativeNumber(), false, false);
            TestWrite("ABCabc".Ex_IsOnlyNegativeNumber(), false, false);
            TestWrite("ABC123".Ex_IsOnlyNegativeNumber(), false, false);
            TestWrite("abc123".Ex_IsOnlyNegativeNumber(), false, false);
            TestWrite("ABab_+".Ex_IsOnlyNegativeNumber(), false, false);
            //Console.WriteLine("");


            Console.WriteLine("========================Equals, Contains========================");

            TestWrite("aBcDEfghiJkL".Ex_Equals("aBcDEfghiJkL", true), true, false);
            TestWrite("aBcDEfghiJkL".Ex_Equals("ABCDEFGHIJKL", true), false, false);
            TestWrite("aBcDEfghiJkL".Ex_Equals("abcdefghijkl", true), false, false);
            //Console.WriteLine("");

            TestWrite("aBcDEfghiJkL".Ex_Equals("aBcDEfghiJkL"), true, false);
            TestWrite("aBcDEfghiJkL".Ex_Equals("ABCDEFGHIJKL"), true, false);
            TestWrite("aBcDEfghiJkL".Ex_Equals("abcdefghijkl"), true, false);
            TestWrite("aBcDEfghiJkL".Ex_Equals("abcde"),  false, false);
            TestWrite("aBcDEfghiJkL".Ex_Equals("ghijkl"), false, false);
            TestWrite("aBcDEfghiJkL".Ex_Equals("ABCDE"),  false, false);
            TestWrite("aBcDEfghiJkL".Ex_Equals("JKL"),    false, false);
            //Console.WriteLine("");

            TestWrite("aBcDEfghiJkL".Ex_Contains("cDEfghi", true), true, false);
            TestWrite("aBcDEfghiJkL".Ex_Contains("CDEFGHI", true), false, false);
            TestWrite("aBcDEfghiJkL".Ex_Contains("cdefghi", true), false, false);
            //Console.WriteLine("");

            TestWrite("aBcDEfghiJkL".Ex_Contains("cDEfghi"), true, false);
            TestWrite("aBcDEfghiJkL".Ex_Contains("CDEFGHI"), true, false);
            TestWrite("aBcDEfghiJkL".Ex_Contains("cdefghi"), true, false);
            TestWrite("aBcDEfghiJkL".Ex_Contains("abc"), true, false);
            TestWrite("aBcDEfghiJkL".Ex_Contains("jkl"), true, false);
            //Console.WriteLine("");


            Console.WriteLine("========================Equals, Contains========================");

            Console.WriteLine("Abcd aBaAcd".Replace("a", "_"));
            Console.WriteLine("");

            Console.WriteLine("ABCDA AbCda aBcDa abcdA".Ex_Replace("a", "__"));
            Console.WriteLine("ABCDA AbCda aBcDa abcdA".Ex_Replace("a", "__", true));
            Console.WriteLine("");

            Console.WriteLine("ABCDA AbCda aBcDa abcdA".Ex_Replace("abcd", ""));
            Console.WriteLine("ABCDA AbCda aBcDa abcdA".Ex_Replace("abcd", "", true));
            Console.WriteLine("");

            Console.WriteLine("ABCDA AbCda aBcDa abcdA".Ex_Replace("ab", '_'));
            Console.WriteLine("ABCDA AbCda aBcDa abcdA".Ex_Replace("ab", '_', true));
            Console.WriteLine("");

            Console.WriteLine("ABCDA AbCda aBcDa abcdA".Ex_Replace(new string[] {"ab", "da"}, '_'));
            Console.WriteLine("ABCDA AbCda aBcDa abcdA".Ex_Replace(new string[] {"ab", "da"}, '_', true));
        }

        public static void Main_2020_01_26_Extensions()
        {
            var intArray1 = new int[] { 0, 1, 2, 3, 4, 5 };
            var intList1 = new List<int> { 0, 1, 2, 3, 4, 5 };
            RitoConsole.PrintArray(intList1);

            intList1.ForEach(a => a++);
            RitoConsole.PrintArray(intList1);

            Console.WriteLine("===");
            RitoConsole.PrintArray(intArray1);

            intArray1.Ex_Foreach(a => a++);
            RitoConsole.PrintArray(intArray1);

            intArray1.Ex_ForeachRef((ref int a) => a++);
            RitoConsole.PrintArray(intArray1);


            int i1 = 1;
            TestWrite(i1.Ex_Range(-1, 0), false);
            TestWrite(i1.Ex_Range(-1, 1), true);
            TestWrite(i1.Ex_Range(1, 1), true);
            TestWrite(i1.Ex_Range(1, 4), true);
            TestWrite(i1.Ex_Range(2, 4), false);
        }

        #endregion // ==========================================================


        /// <summary>
        /// 테스트 콘솔 출력 메소드
        /// <para/> 입력 : 테스트 대상, 원하는 결과
        /// <para/> 출력 : 출력 결과 + 테스트 성공 여부 (콘솔)
        /// <para/> 
        /// </summary>
        public static void TestWrite<T>(T test, T result, bool print = true)
        {
            if(print)
                Console.WriteLine($"[Test]   {test, -8} ===> {test.Equals(result)}");
        }
    }
}

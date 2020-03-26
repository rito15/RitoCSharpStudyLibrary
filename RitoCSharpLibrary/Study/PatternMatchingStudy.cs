using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rito;

namespace RitoCSharpLibrary.Study
{
    // 191218_0052 : 패턴 매칭(상수, 타입)
    // https://docs.microsoft.com/ko-kr/dotnet/csharp/pattern-matching
    // http://www.csharpstudy.com/latest/CS7-pattern.aspx
    // https://happybono.wordpress.com/2019/05/21/c-pattern-matching-패턴-매칭에-대해-알아보자/
    
    //[RitoStudyClass("패턴 매칭(상수, 타입)")]
    class PatternMatchingStudy : StudyBase
    {
        public override void Run()
        {
            // base.Run();
        }

        [RitoStudyMethod]
        protected override void Method()
        {
            CheckMethod(null);
            CheckMethod(111);
        }

        private void CheckMethod(object o)
        {
            string msg = "";

            if(o is null)
            {
                msg = "NULL";
            }
            if(o is int i)
            {
                // 타입 매칭을 통해 '패턴 변수' i를 선언할 경우,
                // 블록 내에서 정수 타입의 i를 사용할 수 있다 !!

                // 그냥 (o is int) 라고 쓰는 것도 가능

                msg = "INT : " + i;
            }

            switch(o)
            {
                case var _ when o is null:
                    Console.WriteLine("Nullllllll");
                    break;

                case var _o when (o is int):
                    Console.WriteLine($"[O] : {o}, {_o}");
                    break;
            }

            Console.WriteLine("[Pattern Check] " + msg);
        }
    }
}

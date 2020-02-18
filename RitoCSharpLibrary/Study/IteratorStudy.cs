using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RitoCSharpLibrary.Study
{
    // 191111 월 : 열거자
    // https://docs.microsoft.com/ko-kr/dotnet/csharp/iterators
    class IteratorStudy : StudyClass
    {
        public override void Run()
        {
            // base.Run();
        }

        ///<summary>설명</summary>
        protected override void Method()
        {
            /*
             * https://kwangyulseo.com/2016/10/16/ienumerator-ienumerable의-의미/
             * 
             * 1. IEnumerator<out T>
             *  : bool MoveNext() 메소드, 
             *    Reset() 메소드,
             *    T Current {get;} 프로퍼티를 포함하는 인터페이스
             *    
             *  : 열거자(Getter)의 역할 수행
             *  
             *  
             *    => 게터
             *  
             *  
             * 2. IEnumerator
             *  : IEnumerator<T> GetEnumerator() 메소드를 포함하는 인터페이스
             *  : Getter인 IEnumerator<T>를 리턴한다.
             *  
             *  
             *    => 게터를 리턴하는 게터
             * 
             */

            IEnumerable<int> iAble1 = GetSingleDigit();
            IEnumerable<int> iAble2 = GetSingleDigit2();
            IEnumerator<int> iAtor  = GetSingleDigit3();

            foreach(var i in iAble1)
                Console.WriteLine(i);

            foreach (var i in iAble2)
                Console.WriteLine(i);

            // 에러 : 열거자(Getter) 자체를 열거시킬 수 없음
            //foreach (var i in iAtor)
            //    Console.WriteLine(i);
        }

        // 1. 반복기 메소드
        public IEnumerable<int> GetSingleDigit()
        {
            yield return 0;
            yield return 1;
            yield return 2;
            yield return 3;
            yield return 4;
            yield return 5;
        }
        
        // 2. 반복기 메소드 단순화(반복문 사용)
        public IEnumerable<int> GetSingleDigit2()
        {
            int index = 0;

            while (index < 6)
                yield return index++;
        }

        // 3. 반복자 메소드
        public IEnumerator<int> GetSingleDigit3()
        {
            int index = 0;

            while (index < 6)
                yield return index++;
        }
    }
}

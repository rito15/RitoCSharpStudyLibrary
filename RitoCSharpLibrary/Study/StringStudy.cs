using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RitoCSharpLibrary.Study
{
    // 191110 일
    // https://docs.microsoft.com/ko-kr/dotnet/csharp/language-reference/tokens/interpolated
    class StringStudy : StudyBase
    {
        public override void Run()
        {
            // base.Run();
        }

        protected override void Method()
        {
            // 1. 스트링 포맷팅 방법 1
            int intValue = 100;
            var date = DateTime.Now;
            Console.WriteLine("Value : {0}, It's {1} Now", intValue, date);

            // 2. 스트링 포맷팅 방법 2 (개꿀)
            Console.WriteLine($"Value : {intValue}, Now : {date}");

            // 3. 스트링 자리 확보 + 좌측 정렬 / 우측 정렬
            Console.WriteLine($"Value : {intValue,-10}, Now : {date,30} !!!");

            // 4. ?: 사용  =>  {( )} 형태로 써야 함
            Console.WriteLine($"Grade : {(intValue > 100 ? 1 : 2)}");

            // 5. 복합 형식 지정
            // https://docs.microsoft.com/ko-kr/dotnet/standard/base-types/composite-formatting
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rito
{
    // 2020. 02. 18. (화) 최초 작성
    // 2020. 02. 18. (화) Test_CheckOnConsole() 작성

    /// <summary>
    /// 콘솔 테스팅을 위한 간편 확장 메소드 제공
    /// <para/> 메소드 네이밍 규칙 : Test_
    /// </summary>
    public static class TesterExtension
    {
        /// <summary>
        /// <para/> * 제네릭 확장
        /// <para/> 테스트 콘솔 출력 메소드
        /// <para/> ---------------------------------------------
        /// <para/> [파라미터]
        /// <para/> goalValue : 예상 결괏값
        /// <para/> memo : 추가 메모(맨 우측에 출력됨)
        /// <para/> ---------------------------------------------
        /// <para/> 출력(콘솔) : 출력 결과 + 테스트 성공 여부 
        /// <para/> 
        /// </summary>
        public static void Test_CheckOnConsole<T>(this T target, in T goalValue,
            in string memo = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int lineNumber = 0)
            where T : struct
        {
            Console.WriteLine($"[Test : {lineNumber, 3}]   {target, -8}" +
                $" ===>   {(target.Equals(goalValue) ? "OK" : "X"), -15}" +
                $"// {memo}");
        }
    }
}

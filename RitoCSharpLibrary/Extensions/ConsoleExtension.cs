using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rito
{
    // 2020. 02. 18. (화) 최초 작성
    // 2020. 02. 18. (화) Enter(), PrintLine() 작성

    /// <summary>
    /// 편리한 콘솔 사용을 위한 확장 메소드 제공
    /// </summary>
    public static class ConsoleExtension
    {
        /// <summary> 
        /// 콘솔 개행 출력 메소드
        /// <para/> -------------------------------------------------
        /// <para/> * (파라미터) count : 개행 횟수
        /// <para/> -------------------------------------------------
        /// <para/> [사용 예시]
        /// <para/> 0.Enter();
        /// </summary>
        public static void Ex_Enter<T>(this T target, in int count = 1)
        {
            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < count; i++)
                sb.Append("\n");

            Console.Write(sb);
        }

        /// <summary> 
        /// 콘솔 라인 출력 메소드
        /// <para/> -------------------------------------------------
        /// <para/> * (파라미터) len : 라인 길이
        /// <para/> -------------------------------------------------
        /// <para/> [사용 예시]
        /// <para/> 0.PrintLine();
        /// <para/> 0.PrintLine(20);
        /// <para/> 0.PrintLine(20, true);
        /// </summary>
        public static void Ex_PrintLine<T>(this T target, in int len = 10, in bool newLine = false)
        {
            StringBuilder sb = new StringBuilder("");

            for (int i = 0; i < len; i++) sb.Append("-");
            if (newLine)                  sb.Append("\n");

            Console.Write(sb);
        }

        /// <summary> 
        /// 콘솔 더블 라인 출력 메소드
        /// <para/> -------------------------------------------------
        /// <para/> * (파라미터) len : 라인 길이
        /// <para/> -------------------------------------------------
        /// <para/> [사용 예시]
        /// <para/> 0.PrintDooubleLine();
        /// <para/> 0.PrintDooubleLine(20);
        /// <para/> 0.PrintDooubleLine(20, true);
        /// </summary>
        public static void Ex_PrintDooubleLine<T>(this T target, in int len = 10, in bool newLine = false)
        {
            StringBuilder sb = new StringBuilder("");

            for (int i = 0; i < len; i++) sb.Append("=");
            if (newLine)                  sb.Append("\n");

            Console.Write(sb);
        }
    }
}

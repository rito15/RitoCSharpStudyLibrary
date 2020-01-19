using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Rito
{
    /// <summary>
    /// 테스팅, 디버깅, 시간 체크를 위한 클래스
    /// </summary>
    class RitoTester
    {
        private static Stopwatch sw = null;
        private static int index = 0;

        /// <summary>
        /// 스톱워치 시간 측정 시작
        /// </summary>
        public static void TimeCheckStart()
        {
            if (sw == null)
                sw = new Stopwatch();

            sw.Restart();
        }

        /// <summary>
        /// 스톱워치 시간 측정 중단<para/>
        /// <param name="printResultToConsole">파라미터 : 콘솔에 결과 출력 여부<para/></param>
        /// <returns>리턴 : 측정 시간</returns>
        /// </summary>
        public static long TimeCheckStop(bool printResultToConsole=false)
        {
            if (sw == null)
                return 0U;

            sw.Stop();
            long result = sw.ElapsedMilliseconds;

            if (printResultToConsole)
            {
                RitoConsole.PrintLine(50, true);
                Console.WriteLine($"[TIME CHECK[{index++}] : {result}");
                RitoConsole.PrintLine(50, true);
            }

            return result;
        }

        /// <summary>
        /// 지정된 횟수로 해당 메소드를 실행하고 경과 시간 리턴
        /// </summary>
        public static long TimeCheck(in int count, Action action)
        {
            TimeCheckStart();

            for (int i = 0; i < count; i++)
                action();

            return TimeCheckStop();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rito
{
    /// <summary>
    /// 편리한 콘솔 사용을 위한 정적 클래스
    /// </summary>
    static class RitoConsole
    {
        /// <summary>
        /// 단순히 개행만 하는 메소드<para/>
        /// <param name="repeat"></param>
        /// </summary>
        public static void Enter(int repeat = 1)
        {
            string L = "";

            for (int i = 0; i < repeat; i++)
                L += "\n";

            Console.Write(L);
        }

        /// <summary>
        /// 콘솔에 한 줄 출력<para/>
        /// <param name="length">length : 줄 길이</param><para/>
        /// <param name="doubleLine">doubleLine : 한 줄(-) 대신 두 줄짜리(=) 출력할지 여부</param><para/>
        /// <param name="enter">enter : 줄 끝나고 개행할지 여부</param>
        /// </summary>
        public static void PrintLine(int length, bool doubleLine = false, bool enter = true)
        {
            if (length < 0)
                return;

            string L = "";
            for (int i = 0; i < length; i++)
                L += (doubleLine ? "=" :"-");

            Console.Write(L + (enter ? "\n" : ""));
        }

        /// <summary>
        /// 배열 출력
        /// <typeparam name="T"></typeparam><para/>
        /// <param name="array">array : 대상 배열</param><para/>
        /// <param name="blank">blank : 요소 사이의 공백 크기</param><para/>
        /// <param name="numPerLine">numPerLine : 한 줄에 출력할 요소 개수(0을 입력할 경우 모두 한 줄에 출력)</param><para/>
        /// <param name="formatSize">formatSize : 각 요소를 포맷팅하여, 요소마다 확보할 너비 지정(기본 우측정렬, 음수 : 좌측 정렬)</param><para/>
        /// <param name="useIndex">useIndex : [0] 꼴로 요소 왼쪽에 써줄지 여부</param><para/>
        /// <param name="useBox">useBox : 배열을 위아래로 감쌀지 여부</param>
        /// </summary>
        public static void PrintArray<T>(T[] array, int blank = 1, int numPerLine = 0, int formatSize = 0, 
            bool useIndex = false, bool useBox = true)
        {
            string B = "";
            for (int i = 0; i < blank; i++)
                B += " ";

            List<string> F = new List<string>();

            // formatSize의 절댓값
            int absFormatSize = (formatSize > 0 ? formatSize : -formatSize);

            if(absFormatSize > 0)
                for(int i = 0; i < array.Length; i++)
                {
                    F.Add("");
                    for(int j = 0; j < absFormatSize - array[i].ToString().Length; j++)
                    {
                        F[i] += " ";
                    }
                }

            if (useBox)
                Console.WriteLine($"======================= [Array] =======================");

            for (int i = 0; i < array.Length; i++)
            {
                string indexString = "[" + i + "]";

                Console.Write($"{(useIndex ? indexString : "")}{(formatSize > 0 ? F[i] : "")}{ array[i] }" +
                              $"{(formatSize < 0 ? F[i] : "")}{ B }");

                if ((numPerLine > 0) &&
                    ((i + 1) % numPerLine == 0))
                    Enter();
            }

            if (useBox)
                Console.WriteLine($"\n=======================================================");
        }
        /// <summary>
        /// 배열 출력
        /// <typeparam name="T"></typeparam><para/>
        /// <param name="array">array : 대상 배열</param><para/>
        /// <param name="blank">blank : 요소 사이의 공백 크기</param><para/>
        /// <param name="numPerLine">numPerLine : 한 줄에 출력할 요소 개수(0을 입력할 경우 모두 한 줄에 출력)</param><para/>
        /// <param name="formatSize">formatSize : 각 요소를 포맷팅하여, 요소마다 확보할 너비 지정(기본 우측정렬, 음수 : 좌측 정렬)</param><para/>
        /// <param name="useIndex">useIndex : [0] 꼴로 요소 왼쪽에 써줄지 여부</param><para/>
        /// <param name="useBox">useBox : 배열을 위아래로 감쌀지 여부</param>
        /// </summary>
        public static void PrintArray<T>(IEnumerable<T> array, int blank = 1, int numPerLine = 0, int formatSize = 0,
            bool useIndex = false, bool useBox = true)
        {
            PrintArray(array.ToArray(), blank, numPerLine, formatSize, useIndex, useBox);
        }
    }
}

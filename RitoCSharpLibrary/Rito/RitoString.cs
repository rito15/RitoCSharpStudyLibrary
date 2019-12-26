using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rito
{
    class RitoString
    {
        /// <summary>
        /// 시작 문자(열)부터 끝 문자(열)까지 자르기<para/>
        /// 자를 위치 시작, 끝 각각 보정 가능(왼쪽은 -1, 오른쪽은 +1)<para/>
        /// <param name="srcString">srcString : 초기화 반드시 필요</param>
        /// </summary>
        public static string Substring(in string srcString, in string start, in string end,
                                        int startIndexMove = 0, int endIndexMove = 0)
        {
            int stringLength = srcString.Length;
            int lastIndex    = stringLength - 1;

            if (stringLength <= start.Length) return srcString;
            if (stringLength <= end.Length)   return srcString;

            int startIndex = srcString.IndexOf(start);
            int endIndex   = srcString.IndexOf(end);

            if (startIndex < 0) return srcString;
            if (endIndex < 0)   return srcString;

            // move 보정
            startIndex += startIndexMove;
            endIndex   += endIndexMove;

            RitoMath.Clamp(0, ref startIndex, lastIndex);
            RitoMath.Clamp(0, ref endIndex,   lastIndex);

            int subLength = endIndex - startIndex + 1; // 서브스트링의 길이

            return srcString.Substring(startIndex, subLength);
        }

        /// <summary>
        /// string.Substring(startIndex, length)와는 달리, start~end를 모두 인덱스로 지정<para/>
        /// * 인덱스 예외도 모두 처리
        /// </summary>
        public static string Substring(in string srcString, int startIndex, int endIndex)
        {
            int lastIndex = srcString.Length - 1;

            if (lastIndex < 0) return srcString;

            RitoMath.Clamp(0, ref startIndex, lastIndex);
            RitoMath.Clamp(0, ref endIndex,   lastIndex);
            RitoMath.Clamp(startIndex, ref endIndex, lastIndex);

            return srcString.Substring(startIndex, endIndex - startIndex + 1);
        }

        /// <summary> 
        /// 스트링을 특정 토큰으로 분리하여 스트링 리스트에 추가 <para/>
        /// <param name="destList">destList : 목표 스트링 리스트</param><para/>
        /// <param name="srcString">srcString : 원본 스트링 </param><para/>
        /// <param name="token">token : 분리의 기준이 될 토큰</param><para/>
        /// <param name="ignoreNullString">ignoreNullString : 길이 0짜리 문자를 무시할지 여부</param><para/>
        /// </summary>
        public static void Tokenize(ref List<string> destList, in string srcString, in char token, 
            bool ignoreNullString = true)
        {
            int index = 0;
            int length = srcString.Length;

            while (index < length && index != -1)
            {
                // 토큰 지나가기
                while (srcString[index] == token)
                {
                    index++;
                    if (index >= length) break;
                }

                int wordEnd = srcString.IndexOf(token, index);

                // 이번 회차에 분리하여 추가할 스트링, 분리 성공 여부
                (string sub, bool success) = ("", false);

                // 끝지점에 도달해서 더이상 토큰이 없을 경우, 마지막 꼬리부분을 모두 추가
                if (wordEnd == -1) sub = srcString.Substring(index);

                // 기본 : 스트링 중간에서 토큰을 만나 서브스트링 추가(index ~ wordEnd)
                else sub = srcString.Substring(index, wordEnd - index);

                if (sub.Length > 0) destList.Add(sub);
                if (sub.Length == 0 && !ignoreNullString) destList.Add(sub);

                index = wordEnd;
            }
        }
    }
}

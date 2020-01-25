using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;   // Regex

namespace Rito
{
    // 2019. 12. 15. 최초 작성
    // 2020. 01. 17. Substring, 디렉토리 및 파일명, 확장자 추출 메소드 추가
    // 2020. 01. 19. Find, Replace 메소드 추가
    // 2020. 01. 25. 정규식 포맷 필드 수정( . -> \. )

    /// <summary>
    /// <para/> [정규식 문법]
    /// <para/> 문자열의 처음 : ^
    /// <para/> 문자열의 끝   : $
    /// <para/> ==============================================================================
    /// <para/> . : 하나의 문자를 의미(모든 문자를 가리킴)
    /// <para/>     예제 : A.B 패턴은 AAB(일치), A1B(일치), AB(불일치), AAB(불일치)
    /// <para/> -------------------------------------------------------------------------------
    /// <para/> ? : ? 앞에 있는 문자가 0개 또는 1개 나타남을 의미
    /// <para/>     예제 : A?B 패턴은 AAB(일치), AB(일치), AAA(불일치), AAAB(불일치)
    /// <para/> -------------------------------------------------------------------------------
    /// <para/> * : * 앞에 있는 문자가 0개 이상 반복됨을 의미
    /// <para/>     예제 : A*B 패턴은 AB(일치), AAAAAAAAAB(일치),
    /// <para/>    A123B(일치), AAAAAAAA(불일치)
    /// <para/> -------------------------------------------------------------------------------
    /// <para/> + : + 앞에 있는 문자가 1개 이상 반복됨을 의미
    /// <para/>     예제 : AB+ 패턴은 AB(일치), ABBBB(일치), 
    /// <para/>    ABC(불일치), A(불일치)
    /// <para/> -------------------------------------------------------------------------------
    /// <para/> [] : [ ] 사이에 있는 형식이 일치하는 것을 의미
    /// <para/> 즉 [AB] 는 A, B만일치, [A-Z] 는 A부터 Z중 하나의 문자와 일치
    /// <para/>     예제 : [A-C] 패턴은 A(일치), B(일치), AB(불일치), BC(불일치)
    /// <para/> ... => [01234] 이렇게만 쓰면 한글자로 0, 1, 2, 3, 4 중 하나가 와야 한다는 의미
    /// <para/> ... => [1234]* 이렇게 쓰면 모든 문자열이 1, 2, 3, 4로 구성되어야 한다는 의미 (0글자 허용)
    /// <para/> ... => [1234]+ 이렇게 쓰면 모든 문자열이 1, 2, 3, 4로 구성되어야 한다는 의미 (반드시 1글자 이상)
    /// <para/> -------------------------------------------------------------------------------
    /// <para/> [^] : ^ 다음에 나오는 문자들을 제외한다는 것을 의미
    /// <para/>     예제 : [^A-C]D 패턴은 DD(일치), AD(불일치), DDD(불일치), D(불일치)
    /// <para/> -------------------------------------------------------------------------------
    /// <para/> () : ( ) 사이에 문자열이 하나의 묶음이 된다.
    /// <para/> 즉 (ab)+ 는 abababab 와 일치
    /// <para/>     예제 : (ab) 패턴은 ab(일치), ba(불일치)
    /// <para/> -------------------------------------------------------------------------------
    /// <para/> {} : { } 사이에 숫자를 쓰면 그 숫자 만큼 패턴이 반복됨을 의미
    /// <para/> [A-C]{1,4}는 A,B,C 를 1개에서 4개 조합하면 된다.
    /// <para/> AAAA도 가능하고 A, BA, ABC, ACBA도 가능
    /// <para/>     예제 : [A-C]{1,3} 패턴은 AA(일치), CBC(일치), ADA(불일치), ACCC(불일치)
    /// <para/> -------------------------------------------------------------------------------
    /// <para/> 문자열 내의 . 가리키기 : "\."
    /// <para/> 문자열 내의 \ 가리키기 : "\\"
    /// <para/> 
    /// <para/> 영숫자 : \w  ==  [A-Za-z0-9]
    /// <para/> 영숫자 제외 모든 문자 : \W
    /// <para/> 
    /// <para/> 숫자 : \d  ==  [0-9]
    /// <para/> 숫자 제외 모든 문자 : \D
    /// <para/> 
    /// <para/> 공백과 탭 : \s
    /// <para/> 공백 제외 모든 문자 : \S
    /// <para/> 
    /// </summary>
    static class RitoRegex
    {
        // https://ko.wikipedia.org/wiki/정규_표현식
        // https://docs.microsoft.com/ko-kr/dotnet/standard/base-types/regular-expressions
        // https://docs.microsoft.com/ko-kr/dotnet/standard/base-types/regular-expression-language-quick-reference
        // https://docs.microsoft.com/ko-kr/dotnet/standard/base-types/regular-expression-examples

        #region 정규식 포맷 필드

        public static readonly Regex _onlyAlphabet = new Regex(@"^[A-Za-z]+$");       // 알파벳만 포함
        public static readonly Regex _onlyAlphabetUpperCase = new Regex(@"^[A-Z]+$"); // 알파벳 대문자만 포함
        public static readonly Regex _onlyAlphabetLowerCase = new Regex(@"^[a-z]+$"); // 알파벳 소문자만 포함
        public static readonly Regex _onlyAlphaDigit = new Regex(@"^[A-Za-z0-9]+$");  // 알파벳 + 정수만 포함
        public static readonly Regex _onlyDigit = new Regex(@"^[0-9]+$");             // 정수만 포함

        public static readonly Regex _NumberFormat = new Regex(@"^(-|\+)?[0-9]+$");                          // 정수(양수, 음수) 형식
        public static readonly Regex _RealNumberFormat = new Regex(@"^(-|\+)?(\d)+\.\d+$");                  // 실수 표현 형식(정수, 음수 포함)
        public static readonly Regex _PhoneNumberFormat = new Regex(@"^01[016-9]-[0-9]{4}-[0-9]{4}$");       // 휴대폰 번호 형식 : 010-1234-5678
        public static readonly Regex _EmailFormat = new Regex(@"^[A-Za-z0-9]([-_\.]?[0-9a-zA-Z])*@[A-Za-z0-9]+\.[A-Za-z]{2,3}$");   // 메일 형식 : abc1234@site.com

        // 추가 예정 :

        // [1] 필드
        //  - URL 형식(홈페이지) : URL + 포트
        //  - 시간
        //  - 날짜
        //  - 날짜 및 시간
        //    => 날짜, 시간 : 모두 표준 형식들로


        // [2] 메소드
        //  - 튜플로 추출하는 메소드
        //    : 날짜, 시간, URL, 이메일, 실수, 휴대폰번호 등등
        //    => 날짜에서는 연 월 일, 실수에서는 정수부와 실수부, URL 에서는 주소와 포트, 파라미터 등을 튜플로 리턴

        #endregion

        #region 스트링 포맷 검사 메소드

        // 스트링과 포맷을 직접 검사
        public static bool CheckFormat(in string prmStr, in string prmFormatStr)
        {
            Regex regex = new Regex(prmFormatStr);
            return regex.IsMatch(prmStr);
        }
        public static bool CheckFormat(in string prmStr, in Regex regex)
        {
            return regex.IsMatch(prmStr);
        }

        #endregion

        #region 특정 포맷 검사 메소드

        // 문자열 길이 검사
        public static bool Length(string prmStr, int prmLength)
        {
            if (prmLength < 0)
                prmLength = 0;

            return prmStr.Length == prmLength;
        }

        // 알파벳만 있는지 검사
        public static bool IsOnlyAlphabet(string prmStr)
        {
            return _onlyAlphabet.IsMatch(prmStr);
        }

        // 대문자만 있는지 검사
        public static bool IsOnlyUpperCase(string prmStr)
        {
            return _onlyAlphabetLowerCase.IsMatch(prmStr);
        }

        // 소문자만 있는지 검사
        public static bool IsOnlyLowerCase(string prmStr)
        {
            return _onlyAlphabetLowerCase.IsMatch(prmStr);
        }

        // 영문자, 숫자만 있는지 검사
        public static bool IsOnlyAlphaDigit(string prmStr)
        {
            return _onlyAlphaDigit.IsMatch(prmStr);
        }

        // 숫자만 있는지 검사
        public static bool IsOnlyDigit(string prmStr)
        {
            return _onlyDigit.IsMatch(prmStr);
        }

        // 정수인지 검사
        public static bool IsNumber(string prmStr)
        {
            return _NumberFormat.IsMatch(prmStr);
        }

        // 실수인지 검사
        public static bool IsFloatNumber(string prmStr)
        {
            return _RealNumberFormat.IsMatch(prmStr);
        }

        // 핸드폰번호 형식인지 검사
        public static bool IsPhoneNumber(string prmStr)
        {
            return _PhoneNumberFormat.IsMatch(prmStr);
        }

        // 이메일 형식인지 검사
        public static bool IsEmail(string prmStr)
        {
            return _EmailFormat.IsMatch(prmStr);
        }

        #endregion

        #region 문자열 추출 메소드

        /// <summary>
        /// start ~ end 사이의 부분 문자열을 추출하여 리턴
        /// <para/> ----------------------------------------------
        /// <para/> [파라미터]
        /// <para/> source : 원본 문자열
        /// <para/> start : 서브 스트링 시작 문자열
        /// <para/> end : 서브 스트링 끝 문자열
        /// <para/> exception : 해당 문자열에서 예외시킬 문자들 (기본 : "")
        /// <para/> containsStart : 서브스트링에서 start 문자열을 포함할지 여부 (기본 : false)
        /// <para/> containsEnd : 서브스트링에서 end 문자열을 포함할지 여부 (기본 : false)
        /// </summary>
        public static string Substring(in string source, in string start, in string end,
            in string exception = "", in bool containsStart = false, in bool containsEnd = false)
        {
            string pattern =
                start +
                (exception.Equals("") ?
                    @".+" :
                    @"[^" + exception + @"]*"
                ) +
                end;

            string result = Regex.Match(source, pattern).Value;

            if (!containsStart) result = Regex.Replace(result, start, string.Empty);
            if (!containsEnd) result = Regex.Replace(result, end, string.Empty);

            return result;
        }

        /// <summary>
        /// <para/> 디렉토리\파일.확장자 꼴에서 디렉토리 경로들만 추출하기(AAA\BBB\CCC)
        /// <para/> ----------------------------------------------
        /// <para/> [파라미터]
        /// <para/> source : 원본 경로 문자열(AAA\BBB\CCC\file name.ext)
        /// <para/> containsLastChar : 디렉토리 경로에서 마지막 \ 포함할지 여부 (기본 : false)
        /// </summary>
        public static string GetDirectoryPath(in string source, bool containsLastChar = false)
        {
            string result = Regex.Match(source, @".*\\").Value;

            if (!containsLastChar)
                result = Regex.Replace(result, @"\\$", string.Empty);

            return result;
        }

        // 2. 디렉토리\파일.확장자 꼴에서 파일 이름만 추출하기 (fileName)
        /// <summary>
        /// <para/> 디렉토리\파일.확장자 꼴에서 파일 이름만 추출하기(file name)
        /// <para/> ----------------------------------------------
        /// <para/> [파라미터]
        /// <para/> source : 원본 경로 문자열(AAA\BBB\CCC\file name.ext)
        /// </summary>
        public static string GetFileName(in string source)
        {
            string result = Regex.Replace(source, @".*\\", string.Empty);
            result = Regex.Replace(result, @"\..*", string.Empty);

            return result;
        }

        /// <summary>
        /// <para/> 디렉토리\파일.확장자 꼴에서 확장자만 추출하기(ext)
        /// <para/> ----------------------------------------------
        /// <para/> [파라미터]
        /// <para/> source : 원본 경로 문자열(AAA\BBB\CCC\file name.ext)
        /// <para/> containsDot : .ext 꼴에서 . 포함할지 여부(기본 : false)
        /// </summary>
        public static string GetExtension(in string source, bool containsDot = false)
        {
            string result = Regex.Match(source, @"\..+$").Value;

            if (!containsDot)
                result = Regex.Replace(result, @"^\.", string.Empty);

            return result;
        }

        /// <summary>
        /// <para/> 디렉토리\파일.확장자 꼴에서 파일.확장자만 추출하기(file name.ext)
        /// <para/> ----------------------------------------------
        /// <para/> [파라미터]
        /// <para/> source : 원본 경로 문자열(AAA\BBB\CCC\file name.ext)
        /// </summary>
        public static string GetFileNameExtension(in string source)
        {
            string result = Regex.Replace(source, @".*\\", string.Empty);

            return result;
        }

        #endregion

        #region 문자열 검사 메소드

        /// <summary>
        /// 문자열이 타겟 문자열을 포함하고 있는지 검사
        /// <para/> ---------------------------------------------------
        /// <para/> [파라미터]
        /// <para/> source : 원본 문자열
        /// <para/> target : 부분 문자열
        /// <para/> isCaseSensitive : 대소문자 구분 여부(기본 true)
        /// <para/> ---------------------------------------------------
        /// <para/> * 패턴이 아닌 정확한 문자열 검사의 경우, Regex보다 string의 성능이 더 좋다.
        /// </summary>
        public static bool Find(in string source, in string target, bool isCaseSensitive = true)
        {
            // 1. 정확한 문자열 검사 - string
            if (isCaseSensitive)
            {
                return source.Contains(target);
            }
            // 2. 패턴 검사 - Regex
            else
            {
                return Regex.IsMatch(source, $"(?i){target}");
            }
        }

        /// <summary>
        /// 문자열이 타겟 문자열들을 모두 포함하고 있는지 검사
        /// <para/> ---------------------------------------------------
        /// <para/> [파라미터]
        /// <para/> source : 원본 문자열
        /// <para/> target : 부분 문자열 리스트
        /// <para/> isCaseSensitive : 대소문자 구분 여부(기본 true)
        /// <para/> ---------------------------------------------------
        /// <para/> * 패턴이 아닌 정확한 문자열 검사의 경우, Regex보다 string의 성능이 더 좋다.
        /// </summary>
        public static bool Find(in string source, in string[] target, bool isCaseSensitive = true)
        {
            // 1. 정확한 문자열 검사 - string
            if (isCaseSensitive)
            {
                for (int i = 0; i < target.Length; i++)
                {
                    if (source.Contains(target[i]) == false)
                        return false;
                }
                return true;
            }
            // 2. 패턴 검사 - Regex
            else
            {
                for (int i = 0; i < target.Length; i++)
                {
                    if (Regex.IsMatch(source, $"(?i){target[i]}") == false)
                        return false;
                }
                return true;
            }
        }

        #endregion // ==========================================================

        #region 문자열 교체 메소드

        // * Replace는 string보다 Regex의 성능이 좋다

        /// <summary>
        /// 문자열 내의 부분 문자열을 다른 문자열로 변경하여 리턴
        /// <para/> ---------------------------------------------------
        /// <para/> [파라미터]
        /// <para/> source : 원본 문자열
        /// <para/> target : 변경될 부분 문자열
        /// <para/> replacement : 대체 문자열
        /// <para/> isCaseSensitive : 대소문자 구분 여부(기본 true)
        /// <para/> ---------------------------------------------------
        /// <para/> [리턴]
        /// <para/> source에서 target->replacement로 교체한 결과 문자열
        /// </summary>
        public static string Replace(in string source, string target, in string replacement, bool isCaseSensitive = true)
        {
            if (isCaseSensitive == false)
                target = $"(?i){target}";

            return Regex.Replace(source, target, replacement);
        }

        /// <summary>
        /// 문자열 내의 부분 문자열을 다른 문자열로 변경하여 리턴
        /// <para/> ---------------------------------------------------
        /// <para/> [파라미터]
        /// <para/> source : 원본 문자열
        /// <para/> target : 변경될 부분 문자열
        /// <para/> replacement : 대체 문자열
        /// <para/> isCaseSensitive : 대소문자 구분 여부(기본 true)
        /// <para/> ---------------------------------------------------
        /// <para/> [리턴]
        /// <para/> source에서 target->replacement로 교체한 결과 문자열
        /// </summary>
        public static string Replace(string source, in string[] target, in string replacement, bool isCaseSensitive = true)
        {
            string targetString = string.Join("|", target);

            if (isCaseSensitive == false)
                targetString = $"(?i){targetString}";

            return Regex.Replace(source, targetString, replacement);
        }

        /// <summary>
        /// 문자열 내의 부분 문자열을 모두 특정 한가지 문자로 변경하여 리턴
        /// <para/> ---------------------------------------------------
        /// <para/> [파라미터]
        /// <para/> source : 원본 문자열
        /// <para/> target : 변경될 부분 문자열
        /// <para/> replacement : 대체 문자
        /// <para/> isCaseSensitive : 대소문자 구분 여부(기본 true)
        /// <para/> ---------------------------------------------------
        /// <para/> [리턴]
        /// <para/> source에서 target 문자들을 replacement로 교체한 결과 문자열
        /// <para/> ---------------------------------------------------
        /// <para/> [예시]
        /// <para/> Replace("AbcABCdeAbCdEf", "cde", '*', false)
        /// <para/> "AbcABCdeAbCdEf" -> "AbcAB***Ab***f"
        /// </summary>
        public static string Replace(string source, string target, in char replacement, bool isCaseSensitive = true)
        {
            string replaceString = "";

            for (int j = 0; j < target.Length; j++)
                replaceString += replacement;

            if (isCaseSensitive == false)
                target = $"(?i){target}";

            source = Regex.Replace(source, target, replaceString);
            return source;
        }

        /// <summary>
        /// 문자열 내의 부분 문자열을 모두 특정 한가지 문자로 변경하여 리턴
        /// <para/> ---------------------------------------------------
        /// <para/> [파라미터]
        /// <para/> source : 원본 문자열
        /// <para/> target : 변경될 부분 문자열들
        /// <para/> replacement : 대체 문자
        /// <para/> isCaseSensitive : 대소문자 구분 여부(기본 true)
        /// <para/> ---------------------------------------------------
        /// <para/> [리턴]
        /// <para/> source에서 target 문자들을 replacement로 교체한 결과 문자열
        /// <para/> ---------------------------------------------------
        /// <para/> [예시]
        /// <para/> Replace("AbcABCdeAbCdEf", new string[]{"a", "cde"}, '*', false)
        /// <para/> "AbcABCdeAbCdEf" -> "*bc*B****b***f"
        /// </summary>
        public static string Replace(string source, in string[] target, in char replacement, bool isCaseSensitive = true)
        {
            string replaceString;

            for (int i = 0; i < target.Length; i++)
            {
                replaceString = "";
                for (int j = 0; j < target[i].Length; j++)
                    replaceString += replacement;

                if (isCaseSensitive == false)
                    target[i] = $"(?i){target[i]}";

                source = Regex.Replace(source, target[i], replaceString);
            }
            return source;
        }

        #endregion // ==========================================================

    }
}

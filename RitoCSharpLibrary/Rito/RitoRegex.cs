using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;   // Regex

namespace Rito
{
    /// <summary>
    /// 간편 정규식 제공 클래스<para/>
    /// 191215
    /// </summary>
    static class RitoRegex
    {
        // https://ko.wikipedia.org/wiki/정규_표현식
        // https://docs.microsoft.com/ko-kr/dotnet/standard/base-types/regular-expressions
        // https://docs.microsoft.com/ko-kr/dotnet/standard/base-types/regular-expression-language-quick-reference
        // https://docs.microsoft.com/ko-kr/dotnet/standard/base-types/regular-expression-examples
        /*  [정규식 문법]
         
            . : 하나의 문자를 의미
            예제 : A.B 패턴은 AAB(일치), A1B(일치), AB(불일치), AAB(불일치)


            ? : ? 앞에 있는 문자가 0개 또는 1개 나타남을 의미
            예제 : A?B 패턴은 AAB(일치), AB(일치), AAA(불일치), AAAB(불일치)


            * : * 앞에 있는 문자가 0개 이상 반복됨을 의미
            예제 : A*B 패턴은 AB(일치), AAAAAAAAAB(일치),
            A123B(일치), AAAAAAAA(불일치)


            + : + 앞에 있는 문자가 1개 이상 반복됨을 의미
            예제 : AB+ 패턴은 AB(일치), ABBBB(일치), 
            ABC(불일치), A(불일치)


            [] : [ ] 사이에 있는 형식이 일치하는 것을 의미
            즉 [AB] 는 A, B만일치, [A-Z] 는 A부터 Z중 하나의 문자와 일치
            예제 : [A-C] 패턴은 A(일치), B(일치), AB(불일치), BC(불일치)

              => [01234] 이렇게만 쓰면 한글자로 0, 1, 2, 3, 4 중 하나가 와야 한다는 의미
              => [1234]* 이렇게 쓰면 모든 문자열이 1, 2, 3, 4로 구성되어야 한다는 의미 (0글자 허용)
              => [1234]+ 이렇게 쓰면 모든 문자열이 1, 2, 3, 4로 구성되어야 한다는 의미 (반드시 1글자 이상)


            [^] : ^다음에 문자를 쓰면 해당 문자를 제외한다는 것을 의미
            예제 : [^A-C]D 패턴은 DD(일치), AD(불일치), DDD(불일치), D(불일치)


            () : ( ) 사이에 문자가 하나의 묶음이 된다.
            즉 (ab)+ 는 abababab 와 일치
            예제 : (ab) 패턴은 ab(일치), ba(불일치)


            {} : { } 사이에 숫자를 쓰면 그 숫자 만큼 패턴이 반복됨을 의미
            [A-C]{1,4}는 A,B,C 를 1개에서 4개 조합하면 된다.
            AAAA도 가능하고 A, BA, ABC, ACBA도 가능

            예제 : [A-C]{1,3} 패턴은 AA(일치), CBC(일치),
            ADA(불일치), ACCC(불일치)
         */

        #region Regex Format Fields

        public static Regex _onlyAlphabet           = new Regex(@"^[A-Za-z]+$");         // 알파벳만 포함
        public static Regex _onlyAlphabetUpperCase = new Regex(@"^[A-Z]+$");            // 알파벳 대문자만 포함
        public static Regex _onlyAlphabetLowerCase = new Regex(@"^[a-z]+$");            // 알파벳 소문자만 포함
        public static Regex _onlyAlphaDigit         = new Regex(@"^[A-Za-z0-9]+$");      // 알파벳 + 숫자만 포함
        public static Regex _onlyDigit               = new Regex(@"^[0-9]+$");            // 숫자만 포함

        public static Regex _NumberFormat       = new Regex(@"^-?[0-9]+$");                          // 정수(양수, 음수) 형식
        public static Regex _FloatNumberFormat = new Regex(@"^-?[0-9]+(.[0-9])?[0-9]*$");           // 실수 표현 형식(정수, 음수 포함)
        public static Regex _PhoneNumberFormat = new Regex(@"^01[016-9]-[0-9]{4}-[0-9]{4}$");       // 휴대폰 번호 형식 : 010-1234-5678
        public static Regex _EmailFormat        = new Regex(@"^[A-Za-z0-9]([-_.]?[0-9a-zA-Z])*@[A-Za-z0-9]+.[A-Za-z]{2,3}$");   // 메일 형식 : abc1234@site.com

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

        #region Default Checker Methods

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

        #region Specific Checker Methods

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
            return _FloatNumberFormat.IsMatch(prmStr);
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
    }
}

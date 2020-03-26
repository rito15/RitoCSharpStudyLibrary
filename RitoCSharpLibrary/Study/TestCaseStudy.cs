using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rito;
using NUnit.Framework;

namespace RitoCSharpLibrary.Study
{
    // 2020. 03. 26. (목)
    // NUnit 테스트 공부
    // [프로젝트] - [NuGet 패키지 관리]
    // [찾아보기] - "NUnit"
    // [NUnit], [NUnitLite] 설치

    // NUnitLite를 설치할 경우 Program.cs가 자동생성됨
    // 테스트할 소스에 using NUnit.Framework; 추가
    // 테스트할 클래스에 [TestFixture] 지정

    [TestFixture]
    class TestCaseStudy
    {
        public int field = 5;

        [TestCase(5, 1, 3, ExpectedResult = 3)]
        [TestCase(0, 1, 3, ExpectedResult = 1)]
        [TestCase(0, 1, 3, ExpectedResult = 1)]
        public int Clamp(ref int value, in int min, in int max)
        {
            if (value <= min) value = min;
            if (value >= max) value = max;
            return value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RitoCSharpLibrary.Study
{
    // 191108 금
    // https://docs.microsoft.com/ko-kr/dotnet/csharp/tuples
    class TupleStudy : StudyClass
    {
        public override void Run()
        {
            // base.Run();
        }

        protected override void Method()
        {
            // 1. 튜플의 기본 : 이름 없는 튜플 & 이름 있는 튜플
            var unnamedTuple = (100, 'a');
            var namedTuple   = (first: "string", second: 2.5f);

            int    value1 = unnamedTuple.Item1;    // 이름 없는 튜플의 요소 참조
            char   value2 = unnamedTuple.Item2;
            string value3 = namedTuple.first;
            float  value4 = namedTuple.second;


            //===============================================================//
            // 2. 튜플의 타입을 명시적으로 지정
            (int, string) intStringTuple;


            //===============================================================//
            // 3. 튜플의 타입 및 이름을 명시적으로 지정
            (int x, int y) integerDuo;
            (int a, int b) integerDuo2; // 값의 이름은 다르지만, 위와 정확히 동일


            //===============================================================//
            // 4. Nullable 타입의 튜플
            //    => 튜플은 기본적으로 null 값을 허용하지 않음
            (char, float)  charFloatTuple;
            (char, float)? charFloatNullableTuple;

            charFloatTuple         = ('a', 0);
            charFloatNullableTuple = ('a', 0);

            // 기본 타입과 Nullable 타입은 값이 같다면 동일
            Console.WriteLine(charFloatTuple == charFloatNullableTuple);


            //===============================================================//
            // 5. 튜플의 분해
            (float x, float y, float z) vector3 = (1.0f, 2.0f, 3.0f);

            // 튜플을 세 개의 변수에 초기화
            (float xValue, float yValue, float zValue) = vector3;

            // * 이렇게는 안됨
            float xVal, yVal;
            //(xVal, yVal, float zVal) = vector3;
            // => 분해에서는 변수 사용과 선언 혼용 불가

            // * 이건 가능 + 무시 항목 사용
            (xVal, yVal, _) = vector3;


            // 5-1. 두 지역변수의 선언과 초기화를 튜플의 형식으로 작성
            (int xx, int yy) = (2, 3);

            Console.WriteLine($"{xx}, {yy}");


            //===============================================================//
            // 6. 튜플을 리턴하는 메소드
            var tutuple = ReturnTupleMethod(1, 2);


            //===============================================================//
            // 7. 분해자 메소드 암시적 호출하기
            DeconstructableClass dc = new DeconstructableClass
            {
                first = 1, second = 2, third = 3
            };
            var threeValueTuple = dc;   // 알맞은 Deconstruct() 메소드가 호출됨
        }

        // 6. 튜플을 리턴하는 메소드
        public static (int x, int y) ReturnTupleMethod(int a, int b)
        {
            return (a, b);
            //return (x: a, y: b);    => 여기서 이름 x, y 지정 의미 없음
        }
    }

    class DeconstructableClass
    {
        public int first;
        public int second;
        public int third;

        // 7. 분해자 메소드 : out 파라미터들을 사용하는 형태
        // 객체를 튜플에 초기화 했을 때 암시적 호출됨
        public void Deconstruct(out int one, out int two, out int three)
        {
            one = first;
            two = second;
            three = third;
        }
    }
}

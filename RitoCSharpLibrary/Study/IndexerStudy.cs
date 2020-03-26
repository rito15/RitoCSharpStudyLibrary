using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rito;

namespace RitoCSharpLibrary.Study
{
    // 191216 월
    //[RitoStudyClass("인덱서")]
    class IndexerStudy : StudyBase
    {
        public override void Run()
        {
            // base.Run();
        }

        [RitoStudyMethod]
        protected override void Method()
        {
            DataArrayClass dac = new DataArrayClass();

            dac[0] = 5;
            dac[2] = 3;
            dac[-1] = 8;

            RitoConsole.PrintArray(dac.Array);
        }
    }

    // 배열을 필드로 저장하는 클래스 - 인덱서 사용 대상
    class DataArrayClass
    {
        private int[] array = { 0, 1, 2, 3, 4, 5 };
        
        // Property
        public int Length
        {
            get
            {
                return array.Length;
            }
        }

        public int[] Array
        {
            get
            {
                return array;
            }
        }

        // Indexer
        public int this[int index]
        {
            get
            {
                return array[index];
            }
            set
            {
                // Exception Handling
                if(index < 0 || index >= Length)
                {
                    Console.WriteLine($"Warining : 잘못된 인덱스 [{index}]에 접근하려 했습니다.");
                    return;
                }

                array[index] = value;
                Console.WriteLine($"값 저장 : [{index}] <- {value}");
            }
        }
    }
}

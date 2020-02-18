using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rito;

namespace RitoCSharpLibrary.Study
{
    // 공부용 공통 클래스
    public class StudyClass
    {
        public static bool boolValue = true;

        // 실행 진입점(실제로 호출하는 부분)
        // 메소드의 실행 여부를 결정
        public virtual void Run()
        {
            Console.Write("\n========== ");
            Console.Write(this.GetType().ToString().Ex_SubstringRight(".", false));
            Console.WriteLine(" ==========");
            Method();
        }

        // 공부한 로직들 작성
        protected virtual void Method()
        {

        }
    }
}

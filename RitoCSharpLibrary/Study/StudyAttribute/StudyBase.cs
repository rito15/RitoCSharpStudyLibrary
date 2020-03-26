using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rito
{
    // 방법 1. 클래스 상속

    /// <summary>
    /// 공부용 공통 클래스
    /// <para/> 자식에서 오버라이드 할 메소드들
    /// <para/> 1. public Run() : 오버라이드 후 base.Run() 호출 또는 비워놓기(실행 여부)
    /// <para/> 2. protected Method() : 공부 내용 작성
    /// </summary>
    public class StudyBase
    {
        /// <summary>
        /// 실행 진입점(실제로 호출하는 부분)
        /// <para/> 메소드의 실행 여부를 결정
        /// <para/> -----------------------------------------------
        /// <para/> 자식에서 base.Run() 할 경우 실행
        /// </summary>
        public virtual void Run()
        {
            Console.Write("\n========== ");
            Console.Write(this.GetType().ToString().Ex_SubstringRight(".", false));
            Console.WriteLine(" ==========");
            Method();
        }

        /// <summary>
        /// 공부한 로직들 작성
        /// </summary>
        protected virtual void Method()
        {

        }
    }


    // 방법 2. 애트리뷰트 사용

    /// <summary> 공부용 클래스임을 명시 </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RitoStudyClassAttribute : Attribute
    {
        public RitoStudyClassAttribute(string t) => Title = t;

        public string Title { get; set; }

        public void PrintTitle()
            => Console.WriteLine(
                "\n===========================================\n" +
                "     Study : " + Title + 
                "\n===========================================\n"
                );
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class RitoStudyMethodAttribute : Attribute
    {
        public RitoStudyMethodAttribute() { }
    }
}

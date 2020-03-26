using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rito;

namespace RitoCSharpLibrary.Study
{
    // 2020. 03. 26. (목)
    // 커스텀 애트리뷰트

    //[RitoStudyClass("커스텀 애트리뷰트")]
    class AttributeStudy
    {
        [RitoStudyMethod]
        public void Method()
        {
            Console.WriteLine("오예");
        }
    }

    
}

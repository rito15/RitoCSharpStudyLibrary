using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rito
{
    // 작성 : 2020. 04. 14
    public static class DelegateExtension
    {
        /// <summary> 델리게이트 또는 이벤트 변수가 해당 메소드를 갖고 있는지 여부 검사 </summary>
        public static bool Ex_Contains<T>(this T @delegate, in T method) where T : Delegate
        {
            if (@delegate == null || method == null) return false;
            return @delegate.GetInvocationList().Contains(method);
        }

        /// <summary> 델리게이트 또는 이벤트 변수가 갖고 있는 메소드 개수 </summary>
        public static int Ex_MethodCount<T>(this T @delegate) where T : Delegate
        {
            if (@delegate == null) return 0;
            return @delegate.GetInvocationList().Length;
        }

        /// <summary> 델리게이트 또는 이벤트 변수가 해당 메소드를 몇 개나 갖고 있는지 검사하여 리턴 </summary>
        public static int Ex_CheckSpecificMethodCount<T>(this T @delegate, in T method) where T : Delegate
        {
            if (@delegate == null || method == null) return 0;

            int count = 0;
            var methods = @delegate.GetInvocationList();
            for (int i = 0; i < methods.Length; i++)
            {
                if (methods[i] == method)
                    count++;
            }

            return count;
        }
    }
}

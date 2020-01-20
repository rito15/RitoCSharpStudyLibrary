using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rito
{
    /// <summary>
    /// 2020. 01. 21.
    /// <para/> 모든 제네릭 컬렉션에 대해 for문을 foreach처럼 바로 쓸 수 있게 해주는 개꿀확장
    /// </summary>
    public static class ForeachExtention
    {

        #region Declarations
        public delegate void refCallBack<T>(ref T param);
        #endregion

        #region Foreach<T>

        /// <summary>
        /// 간편한 Foreach 순회<para/>
        /// <param name="array"></param>
        /// <param name="method">method : 파라미터 하나짜리 메소드 또는 람다<para/></param>
        /// 리턴 : array
        /// </summary>
        public static T[] Ex_Foreach<T>(this T[] array, in Action<T> method)
        {
            int len = array.Length;

            for (int i = 0; i < len; i++)
                method(array[i]);

            return array;
        }
        /// <summary>
        /// 간편한 Foreach 순회<para/>
        /// <param name="array"></param>
        /// <param name="method">method : 파라미터 하나짜리 메소드 또는 람다</param>
        /// </summary>
        public static IEnumerable<T> Ex_Foreach<T>(this IEnumerable<T> enumList, in Action<T> method)
        {
            var array = enumList.ToArray();

            for (int i = 0; i < array.Length; i++)
                method(array[i]);

            return enumList;
        }

        #endregion //====================================================================================================

        #region ForeachRef<T>

        /// <summary>
        /// 간편한 Foreach 순회<para/>
        /// ★ Value Type을 순회할 때 값을 변경할 수 없는 한계점 보완<para/>
        /// .<para/>
        /// [사용 예시]<para/>
        /// int[] intArr1 = new int[10]; int index = 0;<para/>
        /// intArr1.Ex_ForeachRef((ref int a) => a = index++);<para/>
        /// => 원래는 각 배열 요소에 초기화가 안됨, ref로는 가능<para/>
        /// .<para/>
        /// 리턴 : array
        /// </summary>
        /// ★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
        public static T[] Ex_Foreach<T>(this T[] array, in refCallBack<T> method)
        {
            int len = array.Length;

            for (int i = 0; i < len; i++)
                method(ref array[i]);

            return array;
        }

        #endregion
    }
}

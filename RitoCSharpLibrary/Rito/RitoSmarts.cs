using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rito
{
    /// <summary>
    /// Smarts : 효율적이고 간결하고 간편한 문법 제공
    /// </summary>
    public static class RitoSmarts
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
        public static T[] Foreach<T>(T[] array, Action<T> method)
        {
            int len = array.Length;

            for (int i = 0; i < len; i++)
                method(array[i]);

            return array;
        }

        [Obsolete("List<T>에는 이미 Foreach가 있으니까 뻘짓 ㄴ")]
        public static List<T> Foreach<T>(List<T> list, Action<T> method)
        {
            int len = list.Count;

            for (int i = 0; i < len; i++)
                method(list[i]);

            return list;
        }

        /// <summary>
        /// 간편한 Foreach 순회<para/>
        /// <param name="array"></param>
        /// <param name="method">method : 파라미터 하나짜리 메소드 또는 람다</param>
        /// </summary>
        public static IEnumerable<T> Foreach<T>(IEnumerable<T> enumList, Action<T> method)
        {
            var array = enumList.ToArray();
            Foreach(array, method);

            return enumList;
        }

        /// <summary>
        /// 가변 파라미터 배열을 foreach로 즉시 순회<para/>
        /// 리턴 : array
        /// </summary>
        public static T[] Foreach<T>(Action<T> method, params T[] array)
        {
            int len = array.Length;

            for (int i = 0; i < len; i++)
                method(array[i]);

            return array;
        }

        #endregion //====================================================================================================

        #region ForeachRef<T>

        /// <summary>
        /// 간편한 Foreach 순회<para/>
        /// ★ Value Type을 순회할 때 값을 변경할 수 없는 한계점 보완<para/>
        /// .<para/>
        /// [사용 예시]<para/>
        /// int[] intArr1 = new int[10]; int index = 0;<para/>
        /// RitoSmarts.ForeachRef(intArr1, (ref int a) => a = index++);<para/>
        /// => 원래는 각 배열 요소에 초기화가 안됨, ref로는 가능<para/>
        /// .<para/>
        /// 리턴 : array
        /// </summary>
        /// ★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
        public static T[] ForeachRef<T>(T[] array, refCallBack<T> method)
        {
            int len = array.Length;

            for (int i = 0; i < len; i++)
                method(ref array[i]);

            return array;
        }

        /// <summary>
        /// 간편한 Foreach 순회<para/>
        /// ★ Value Type을 순회할 때 값을 변경할 수 없는 한계점 보완<para/>
        /// .<para/>
        /// 사용 예시 : RitoSmarts.ForeachRef(ref intList, (ref int a) => a = 3);<para/>
        /// 리턴 : list
        /// </summary>
        public static void ForeachRef<T>(ref List<T> list, refCallBack<T> method)
        {
            var array = list.ToArray();
            int len = array.Length;

            for (int i = 0; i < len; i++)
                method(ref array[i]);

            list = new List<T>(array);
        }

        /// <summary>
        /// 가변 파라미터 배열을 foreach로 즉시 순회<para/>
        /// 가변 파라미터로 입력한 배열을 콜백으로 수정하고 리턴할 수 있음<para/>
        /// 리턴 : array
        /// </summary>
        public static T[] ForeachRef<T>(refCallBack<T> method, params T[] array)
        {
            int len = array.Length;

            for (int i = 0; i < len; i++)
                method(ref array[i]);

            return array;
        }

        #endregion

    }
}

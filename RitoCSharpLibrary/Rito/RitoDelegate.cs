using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rito
{
    /// <summary>
    /// 델리게이트, 이벤트 제공
    /// </summary>
    class RitoDelegate
    {
        // ★ Action<T>, Action<T1, T2>, ..., T16까지 있으니 뻘짓 ㄴㄴ
        //public delegate void sendOneParamType<T>(T param);
        //public delegate void sendTwoParamType<T, U>(T param1, U param2);
        //public delegate void sendThreeParamType<T, U, V>(T param1, U param2, V param3);

        public delegate void sendOneRefParamType<T>(ref T param);
    }
}

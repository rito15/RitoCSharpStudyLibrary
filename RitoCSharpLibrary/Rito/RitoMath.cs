using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rito
{
    class RitoMath
    {
        #region 1. Digitalize

        /// <summary> 정수를 0, 1 값으로 디지털화하여 리턴 </summary>
        public static int Digitalize(int intValue)
        {
            if (intValue.Equals(0)) return 0;
            return 1;
        }

        /// <summary> 실수를 0, 1 값으로 디지털화하여 리턴 </summary>
        public static float Digitalize(float floatValue)
        {
            if (floatValue.Equals(0f)) return 0f;
            return 1f;
        }

        /// <summary> 정수를 -1, 0, 1 값으로 변환하여 리턴 </summary>
        public static int SignedDigitalize(int intValue)
        {
            if (intValue < 0) return -1;
            if (intValue > 0) return 1;
            return 0;
        }

        /// <summary> 실수를 -1, 0, 1 값으로 변환하여 리턴 </summary>
        public static float SignedDigitalize(float floatValue)
        {
            if (floatValue < 0f) return -1f;
            if (floatValue > 0f) return 1f;
            return 0f;
        }

        #endregion

        #region 2. Clamp

        /// <summary> 정수 범위 제한(min, max : 닫힌 범위) </summary>
        public static float Clamp(int min, ref int variable, int max)
        {
            if (variable < min) variable = min;
            if (variable > max) variable = max;
            return variable;
        }
        /// <summary> 정수 범위 제한(min : 닫힌 범위) </summary>
        public static float Clamp(int min, ref int variable)
        {
            if (variable < min) variable = min;
            return variable;
        }
        /// <summary> 정수 범위 제한(max : 닫힌 범위) </summary>
        public static float Clamp(ref int variable, int max)
        {
            if (variable > max) variable = max;
            return variable;
        }

        /// <summary> 실수 범위 제한(min, max : 닫힌 범위) </summary>
        public static float Clamp(float min, ref float variable, float max)
        {
            if (variable < min) variable = min;
            if (variable > max) variable = max;
            return variable;
        }
        /// <summary> 실수 범위 제한(min : 닫힌 범위) </summary>
        public static float Clamp(float min, ref float variable)
        {
            if (variable < min) variable = min;
            return variable;
        }
        /// <summary> 실수 범위 제한(max : 닫힌 범위) </summary>
        public static float Clamp(ref float variable, float max)
        {
            if (variable > max) variable = max;
            return variable;
        }

        #endregion

        #region 3. Check Range

        /// <summary>
        /// 정수 변수의 값이 min 이상, max 이하인지 검사
        /// </summary>
        public static bool InRange(int min, ref int variable, int max)
        {
            return (min <= variable && variable <= max);
        }
        /// <summary>
        /// 실수 변수의 값이 min 이상, max 이하인지 검사
        /// </summary>
        public static bool InRange(float min, ref float variable, float max)
        {
            return (min <= variable && variable <= max);
        }

        #endregion
    }
}

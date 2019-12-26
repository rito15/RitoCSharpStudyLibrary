using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rito
{
    class RitoRandom
    {
        private static int GetSeed()
        {
            var rand = System.Security.Cryptography.RandomNumberGenerator.Create();
            byte[] data = new byte[100];

            rand.GetBytes(data);

            return System.Math.Abs(BitConverter.ToInt32(data, 4));
        }

        /// <summary> 랜덤 정수(min ~ max - 1) 리턴 </summary>
        public static int GetRandomInt(int min, int max)
        {
            if (min < int.MinValue) min = int.MinValue;
            if (max > int.MaxValue) max = int.MaxValue;

            return new System.Random(GetSeed()).Next(min, max);
        }

        /// <summary> 랜덤 정수(min ~ max - 1) 리턴 </summary>
        public static int GetRandomInt(float min, float max)
        {
            return GetRandomInt((int)min, (int)max);
        }

        /// <summary> 제비뽑기 : 퍼센트(0~100) 입력 </summary>
        public static bool DrawLots(int percent)
        {
            if (percent < 0) percent = 0;
            if (percent > 100) percent = 100;

            int randomNumber = GetRandomInt(1, 101);

            return (randomNumber <= percent);
        }
    }
}

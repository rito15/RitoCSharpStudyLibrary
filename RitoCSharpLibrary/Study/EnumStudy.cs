using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rito;

namespace RitoCSharpLibrary.Study
{
    // 2020. 02. 17. 월
    // enum [Flags] 공부
    class EnumStudy : StudyBase
    {
        public override void Run()
        {
            //base.Run();
        }

        enum Status
        {
            Idle,  Moving, Jumping, Eating
        }

        [Flags]
        enum FlagStatus1
        {
            Idle,       // 0
            Moving,     // 1
            Jumping,    // 2
            Eating      // 3 = 1(Moving) + 2(Jumping)
        }

        [Flags]
        enum FlagStatus2
        {
            Idle    = 0,
            Moving  = 1, 
            Jumping = 2, 
            Eating  = 4,

            Mix1 = 3,   // 1(Moving) + 2(Jumping)
            Mix2 = 5,   // 1(Moving) + 4(Eating)
        }
        
        protected override void Method()
        {
            Status stat = Status.Moving | Status.Jumping;
            FlagStatus1 fStat1 = FlagStatus1.Moving | FlagStatus1.Jumping;
            FlagStatus2 fStat2 = FlagStatus2.Moving | FlagStatus2.Jumping;

            Console.WriteLine("[1. Enum]");
            Console.WriteLine(stat);
            Console.WriteLine("Has Moving  : " + stat.HasFlag(Status.Moving));
            Console.WriteLine("Has Jumping : " + stat.HasFlag(Status.Jumping));
            Console.WriteLine("Has Eating  : " + stat.HasFlag(Status.Eating));
            Console.WriteLine("Jumping | Eating : " + (Status.Jumping | Status.Eating));

            Console.WriteLine("\n[2. Flags Enum 1]");
            Console.WriteLine(fStat1);
            Console.WriteLine("Has Moving  : " + fStat1.HasFlag(FlagStatus1.Moving));
            Console.WriteLine("Has Jumping : " + fStat1.HasFlag(FlagStatus1.Jumping));
            Console.WriteLine("Has Eating  : " + fStat1.HasFlag(FlagStatus1.Eating));
            Console.WriteLine("Jumping | Eating : " + (FlagStatus1.Jumping | FlagStatus1.Eating));

            Console.WriteLine("\n[3. Flags Enum 2]");
            Console.WriteLine(fStat2);
            Console.WriteLine("Has Moving  : " + fStat2.HasFlag(FlagStatus2.Moving));
            Console.WriteLine("Has Jumping : " + fStat2.HasFlag(FlagStatus2.Jumping));
            Console.WriteLine("Has Eating  : " + fStat2.HasFlag(FlagStatus2.Eating));
            Console.WriteLine("Has Mix1    : " + fStat2.HasFlag(FlagStatus2.Mix1));0.Ex_Enter();
            Console.WriteLine("Jumping | Eating : " + (FlagStatus2.Jumping | FlagStatus2.Eating));

            0.Ex_Enter();
            0.Ex_PrintLine(80, true);
            0.Ex_PrintDooubleLine(80, true);
            fStat2.HasFlag(FlagStatus2.Mix1).Test_CheckOnConsole(true, "fStat2.HasFlag(FlagStatus2.Mix1)");
        }
    }
}

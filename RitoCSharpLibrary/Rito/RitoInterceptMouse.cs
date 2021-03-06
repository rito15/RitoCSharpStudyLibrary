﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;   // 말썽부려서 그냥 넣어놨음

namespace Rito
{
    // 출처 https://outshine90.tistory.com/entry/global-mouse-hooking-및-block

    /// <summary>
    /// 마우스 클릭 차단/허용<para/>
    /// 차단 : InterceptMouse.Start();<para/>
    /// 허용 : InterceptMouse.End();
    /// </summary>
    class RitoInterceptMouse
    {
        private static LowLevelMouseProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        public static void Start()
        {
            _hookID = SetHook(_proc);
        }

        public static void End()
        {
            UnhookWindowsHookEx(_hookID);
        }

        private static IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (System.Diagnostics.Process curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (System.Diagnostics.ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 &&
                (MouseMessages.WM_LBUTTONDOWN == (MouseMessages)wParam) || MouseMessages.WM_RBUTTONDOWN == (MouseMessages)wParam ||
                (MouseMessages.WM_LBUTTONUP == (MouseMessages)wParam) || MouseMessages.WM_RBUTTONUP == (MouseMessages)wParam ||
                 MouseMessages.WM_MOUSEWHEEL == (MouseMessages)wParam || MouseMessages.WM_MBUTTONDOWN == (MouseMessages)wParam ||
                 MouseMessages.WM_MOUSEMOVE == (MouseMessages)wParam
                )
            {
                return _hookID;
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private const int WH_MOUSE_LL = 14;

        private enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,

            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,

            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205,

            WM_MBUTTONDOWN = 0x0207 // 휠 버튼 클릭
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}

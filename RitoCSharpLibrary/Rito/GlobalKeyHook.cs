using System;
using System.Linq;
using System.Windows.Forms;

namespace Rito
{
    /// <summary> 
    /// <para/> 2020. 04. 14. 최초 작성
    /// <para/> .
    /// <para/> [기능]
    /// <para/> - 키 누름, 키 뗌 이벤트 글로벌 후킹
    /// <para/> .
    /// <para/> [메소드]
    /// <para/> - 후킹 시작 : Hook() 또는 Start()
    /// <para/> - 후킹 종료 : UnHook() 또는 Stop()
    /// <para/> - 핸들러 추가 : AddKeyDownHandler(메소드), AddKeyUpHandler(메소드)
    /// <para/> ==> 메소드가 중복되지 않게 이벤트에 핸들러로 추가
    /// <para/> - 이벤트 변수 비우기 : ResetKeyDownEvent(), ResetKeyUpEvent()
    /// <para/> .
    /// <para/> [참고]
    /// <para/> - KeyDown, KeyUp 이벤트 변수에 핸들러를 추가하여 사용
    /// <para/> - 후킹 중인 동안에도 핸들러 추가/제거 가능
    /// <para/> - UnHook(), Stop() 호출해도 이벤트 변수들이 리셋되지는 않음
    /// <para/> 
    /// <para/> 
    /// </summary>
    class GlobalKeyHook
    {
        #region _DLL Import

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, keyboardHookProc callback, IntPtr hInstance, uint threadId);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref keyboardHookStruct IParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern short GetKeyState(int nCode);
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string IpFileName);   // 라이브러리 등록

        #endregion // ==========================================================

        #region _Hide

        // callback Delegate
        private delegate int keyboardHookProc(int code, int wParam, ref keyboardHookStruct IParam);

        // keyboardHookStruct 구조체 정의
        private struct keyboardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        // 정의 되어 있는 상수 값
        const int VK_SHIFT = 0x10;
        const int VK_CONTROL = 0x11;
        const int VK_MENU = 0x12;

        const int WH_KEYBOARD_LL = 13;
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;
        const int WM_SYSKEYDOWN = 0x104;
        const int WM_SYSKEYUP = 0x105;

        private keyboardHookProc khp;
        private IntPtr hhook = IntPtr.Zero;

        /// <summary> 현재 후킹 진행 중인지 여부 </summary>
        private bool _isHooking = false;

        /// <summary> 락! </summary>
        private object _lock = new object();

        public GlobalKeyHook()
        {
            khp = new keyboardHookProc(HookProc);
        }

        private int HookProc(int code, int wParam, ref keyboardHookStruct IParam)
        {
            if (code >= 0)
            {
                Keys key = (Keys)IParam.vkCode;
                if ((GetKeyState(VK_CONTROL) & 0x80) != 0)
                    key |= Keys.Control;
                if ((GetKeyState(VK_MENU) & 0x80) != 0)
                    key |= Keys.Alt;
                if ((GetKeyState(VK_SHIFT) & 0x80) != 0)
                    key |= Keys.Shift;

                KeyEventArgs kea = new KeyEventArgs(key);
                if ((wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN) && (KeyDown != null))
                {
                    KeyDown(this, kea);
                }
                else if ((wParam == WM_KEYUP || wParam == WM_SYSKEYUP) && (KeyUp != null))
                {
                    KeyUp(this, kea);
                }
                if (kea.Handled)
                    return 1;

            }

            return CallNextHookEx(hhook, code, wParam, ref IParam);
        }

        // 후킹 시작
        public void Hook()
        {
            lock (_lock)
            {
                if (_isHooking) return;
                else _isHooking = true;
            }

            IntPtr hInstance = LoadLibrary("User32");
            hhook = SetWindowsHookEx(WH_KEYBOARD_LL, khp, hInstance, 0);
        }

        // 후킹 종료
        public void UnHook()
        {
            lock (_lock)
            {
                if (!_isHooking) return;
                else _isHooking = false;
            }

            UnhookWindowsHookEx(hhook);
        }

        #endregion // ==========================================================

        #region Event Fields

        /// <summary> 키 누름 이벤트 함수 (+= 메소드로 핸들러 추가) </summary>
        public event KeyEventHandler KeyDown;

        /// <summary> 키 뗌 이벤트 함수 (+= 메소드로 핸들러 추가) </summary>
        public event KeyEventHandler KeyUp;

        #endregion // ==========================================================

        /// <summary> KeyDown 이벤트에 중복되지 않게 핸들러 추가 </summary>
        public void AddKeyDownHandler(KeyEventHandler handler)
        {
            if (KeyDown != null && (KeyDown.GetInvocationList()?.Contains(handler) ?? false))
                return;

            KeyDown += handler;
        }

        /// <summary> KeyDown 이벤트에 중복되지 않게 핸들러 추가 </summary>
        public void AddKeyUpHandler(KeyEventHandler handler)
        {
            if (KeyUp != null && (KeyUp.GetInvocationList()?.Contains(handler) ?? false))
                return;

            KeyUp += handler;
        }

        /// <summary> 키 누름 이벤트 초기화 </summary>
        public void ResetKeyDownEvent()
        {
            KeyDown = null;
        }

        /// <summary> 키 뗌 이벤트 초기화 </summary>
        public void ResetKeyUpEvent()
        {
            KeyUp = null;
        }

        /// <summary> 후킹 시작 </summary>
        public void Start() => Hook();
        /// <summary> 후킹 종료 </summary>
        public void Stop() => UnHook();
    }
}
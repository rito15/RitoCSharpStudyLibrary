using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Diagnostics;
using System.Windows.Forms;

namespace Rito
{
    // 출처 http://www.gisdeveloper.co.kr/?p=4755
    /// <summary> 
    /// 관리자 권한으로 프로그램 실행, 작업 관리자 제한/허용 <para/>
    /// <typeparam name="FormType"> FormType : Form을 상속받는 윈폼 클래스 </typeparam>
    /// </summary>
    class RitoAdmin<FormType> where FormType : Form, new()
    {
        /// <summary> 현재 관리자 권한으로 프로세스가 실행되고 있는지 여부 리턴 </summary>
        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();

            if (identity != null)
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            return false;
        }

        /// <summary>
        /// 윈폼을 관리자 권한으로 실행시키기<para/>
        /// Program.cs에서 Main() 내부를 모두 지우고 Admin.StartWithAdmin()만 해주면 됨
        /// </summary>
        public static void StartWithAdmin()
        {
            if (IsAdministrator() == false) // 관리자 권한으로 실행되지 않는 경우라면
            {
                try
                {
                    ProcessStartInfo procInfo = new ProcessStartInfo();
                    procInfo.UseShellExecute = true;
                    procInfo.FileName = Application.ExecutablePath;
                    procInfo.WorkingDirectory = Environment.CurrentDirectory;
                    procInfo.Verb = "runas";
                    Process.Start(procInfo);
                }
                catch (Exception ex)
                {
                    // 사용자가 프로그램을 관리자 권한으로 실행하기를 원하지 않을 경우에 대한 처리
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            else
            { // 처음부터 프로그램은 관리자 권한으로 실행되고 있는 경우라면 ..
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormType());
            }
        }
    }

    /// <summary> 
    /// 작업 관리자 제한/허용
    /// </summary>
    class RitoAdmin
    {
        /// <summary> 작업관리자 제한 </summary>
        public static void DisableTaskMgr()
        {
            Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            reg.SetValue("DisableTaskMgr", 1, Microsoft.Win32.RegistryValueKind.DWord);
        }

        /// <summary> 작업관리자 허용 </summary>
        public static void EnableTaskMgr()
        {
            Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            reg.SetValue("DisableTaskMgr", 0, Microsoft.Win32.RegistryValueKind.DWord);
        }
    }
}

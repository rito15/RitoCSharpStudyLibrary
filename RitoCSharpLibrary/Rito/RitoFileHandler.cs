using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rito
{
    /// <summary> 파일 입출력 담당 </summary>
    class RitoFileHandler
    {
        #region Checkers

        /// <summary> 폴더 존재 여부 확인 </summary>
        public static bool CheckFolderExists(string path)
        {
            return new System.IO.DirectoryInfo(path).Exists;
        }

        /// <summary> 파일 존재 여부 확인 </summary>
        public static bool CheckFileExists(string path)
        {
            return System.IO.File.Exists(path);
        }

        /// <summary> 경로에서 폴더 경로만 가져오기(마지막 \ 미포함) </summary>
        public static string GetFolderPath(string fullPath)
        {
            int lastIndex = fullPath.LastIndexOf('\\');
            if (lastIndex < 0) return "";

            return fullPath.Substring(0, lastIndex);
        }

        /// <summary> 경로에서 파일 이름+확장자만 가져오기 </summary>
        public static string GetFileName(string fullPath)
        {
            int lastIndex = fullPath.LastIndexOf('\\');
            if (lastIndex < 0) return "";

            return fullPath.Substring(lastIndex + 1);
        }

        #endregion // ------------------------------------------------------------------------------------

        #region Creators

        /// <summary> 폴더 존재여부 확인하여, 없으면 생성 </summary>
        public static void CreateFolder(string path)
        {
            //폴더 존재하지 않으면 폴더 생성
            if (!CheckFolderExists(path))
                new System.IO.DirectoryInfo(path).Create();
        }

        /// <summary> 파일 존재여부 확인하여, 없으면 생성 및 내용 추가 </summary>
        public static void CreateFile(string path, string contents = "")
        {
            // 폴더 존재여부 확인하여, 없으면 생성
            string folderPath = path.Substring(0, path.LastIndexOf('\\'));
            CreateFolder(folderPath);

            // 파일이 이미 존재하는 경우 종료
            if (CheckFileExists(path)) return;

            System.IO.FileStream fs = new System.IO.FileStream(@path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);

            sw.WriteLine(contents);

            sw.Close();
            fs.Close();
        }

        #endregion // ------------------------------------------------------------------------------------

        #region File Writers

        /// <summary> 파일에 한 줄 덮어쓰기 (true 추가 입력 시 추가하기) </summary>
        public static void WriteLineInFile(string path, string line, bool append = false)
        {
            // 폴더 존재여부 확인하여, 없으면 생성
            int lastIndex = path.LastIndexOf('\\');
            if (lastIndex < 0) return;

            string folderPath = path.Substring(0, lastIndex);
            CreateFolder(folderPath);

            System.IO.FileMode fm;
            if (append)  // 추가하기
                fm = System.IO.FileMode.Append;
            else // 기본 모드 = 덮어쓰기
                fm = System.IO.FileMode.Create;

            // 파일에 쓰기
            System.IO.FileStream fs = new System.IO.FileStream(@path, fm, System.IO.FileAccess.Write);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);

            sw.WriteLine(line);

            sw.Close();
            fs.Close();
        }

        /// <summary> 파일에 여러 줄 덮어쓰기 (true 추가 입력 시 추가하기) </summary>
        public static void WriteMultiLinesInFile(string path, List<string> lines, bool append = false)
        {
            // 폴더 존재여부 확인하여, 없으면 생성
            int lastIndex = path.LastIndexOf('\\');
            if (lastIndex < 0) return;

            string folderPath = path.Substring(0, lastIndex);
            CreateFolder(folderPath);

            // 기본 모드 = 덮어쓰기
            System.IO.FileMode fm = System.IO.FileMode.Create;

            if (append)  // 추가하기
                fm = System.IO.FileMode.Append;

            // 파일에 쓰기
            System.IO.FileStream fs = new System.IO.FileStream(@path, fm, System.IO.FileAccess.Write);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);

            foreach (string str in lines)
                sw.WriteLine(str);

            sw.Close();
            fs.Close();
        }
        /// <summary> 파일에 여러 줄 덮어쓰기 (true 추가 입력 시 추가하기) </summary>
        public static void WriteMultiLinesInFile(string path, string[] lines, bool append = false)
        {
            List<string> strList = new List<string>(lines);
            WriteMultiLinesInFile(path, strList, append);
        }

        #endregion // ------------------------------------------------------------------------------------

        #region File Readers

        /// <summary> 파일에서 첫 줄 읽어오기 </summary>
        public static string ReadLineFromFile(string path)
        {
            if (!CheckFileExists(path))
                return "";

            System.IO.FileStream fs = new System.IO.FileStream(@path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.StreamReader sw = new System.IO.StreamReader(fs);

            string line = sw.ReadLine();

            fs.Close();
            sw.Close();

            return line;
        }

        /// <summary> 파일 모두 읽어오기 </summary>
        public static string[] ReadFile(string path)
        {
            if (!CheckFileExists(path))
                return null;

            return System.IO.File.ReadAllLines(path);
        }

        /// <summary> 파일 내용 모두 콘솔에 출력하기 </summary>
        public static void PrintFile(string path)
        {
            if (!CheckFileExists(path))
                return;

            string[] contents = System.IO.File.ReadAllLines(path);

            foreach (var line in contents)
                Console.WriteLine(line);
        }

        #endregion // ------------------------------------------------------------------------------------
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;

namespace RitoCSharpLibrary.SocketLib
{
    /// <summary> TCP/IP 소켓통신 서버 </summary>
    class SocketServer
    {
        public bool IsRunning { get; private set; }
        //public int IpAddress {get; private set;}
        public int PortNumber { get; private set; }

        private Socket _socket;
        private Socket _clientSocket;
        private Thread _thread;
        private string NL => Environment.NewLine;

        public SocketServer(int portNumber = 50001)
        {
            IsRunning = false;
            PortNumber = portNumber;
        }

        #region Public Methods

        /// <summary> 서버 구동 </summary>
        public void Run()
        {
            if (IsRunning || (_thread != null && _thread.IsAlive))
            {
                RecordText($"[Warning] Socket Thread Is Already Running.");
                return;
            }
            IsRunning = true;

            _thread = new Thread(ThreadBody);
            _thread.Start();

            RecordText($"* Run : Port [{PortNumber}]");
        }

        /// <summary> 서버 종료 </summary>
        public void Close()
        {
            if (!IsRunning)
            {
                RecordText($"[Warning] Already Closed");
                return;
            }
            IsRunning = false;
            RecordText($"* Close ");

            if (_clientSocket != null && _clientSocket.Connected)
                _clientSocket.Close();
            _socket?.Close();
            _thread?.Abort();
        }

        /// <summary> 서버 재시작 </summary>
        public void Restart()
        {
            Close();
            Run();
        }

        /// <summary> 연결된 클라이언트에 메시지 전송 </summary>
        public void SendToClient(in string strContent)
        {
            if (!IsRunning || _clientSocket == null || !_clientSocket.Connected)
            {
                RecordText("ERROR : Send To Client - Failed");
                return;
            }

            byte[] bData = Encoding.UTF8.GetBytes(strContent);
            _clientSocket.Send(bData, 0, bData.Length, SocketFlags.None);

            RecordText($"Send To Client : {strContent}");
        }

        /// <summary> 포트번호 설정 </summary>
        public void SetPortNumber(int n)
        {
            PortNumber = n;
        }

        #endregion // ==========================================================

        #region Private Methods

        /// <summary> 클라이언트에서 받은 데이터 처리 </summary>
        private void ProcessClientData(string strData)
        {
            strData = strData.ToUpper();
            RecordText($"Process : {strData}");

            // 클라이언트 연결 종료 => 서버 재시작
            if (strData.Equals("DISCONNECT"))
            {
                Close();
                Run();
            }

            // Do Something
            // ............
        }

        /// <summary> 서버 동작 기록 </summary>
        private void RecordText(in string text)
        {
            Console.WriteLine(text + NL);
            //_form.RecordText(text);
        }

        #endregion // ==========================================================

        #region Thread Body

        private void ThreadBody()
        {
            IsRunning = true;
            RecordText($"* Thread Begin");

            // (1) 소켓 객체 생성 (TCP 소켓)
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // (2) 포트에 바인드
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, PortNumber);
            _socket.Bind(ep);

            // (3) 포트 Listening 시작
            _socket.Listen(10);

            RecordText($"* Listening...");
            // (4) 연결을 받아들여 새 소켓 생성 (하나의 연결만 받아들임)
            _clientSocket = _socket.Accept();
            RecordText($"* Client Accepted");

            byte[] buff = new byte[8192];
            while (IsRunning)
            {
                int n = 0;

                // 소켓 수신
                try
                {
                    n = _clientSocket.Receive(buff);
                }
                catch (Exception e)
                {
                    RecordText($"[Error] {e.Message}");
                }
                
                // 받은 데이터가 있을 경우 처리
                if (n > 0)
                {
                    string strData = Encoding.UTF8.GetString(buff, 0, n);
                    RecordText($"Received Data : {strData}");
                    ProcessClientData(strData);
                }
            }

            // 소켓 닫기
            Close();
        }

        #endregion // ==========================================================
    }
}

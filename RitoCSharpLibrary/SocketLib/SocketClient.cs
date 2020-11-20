using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RitoCSharpLibrary.SocketLib
{
    /// <summary> TCP/IP 소켓통신 클라이언트 </summary>
    class SocketClient
    {
        public bool IsRunning { get; private set; }
        public string IpAddr { get; private set; }
        public int PortNumber { get; private set; }

        private Socket _socket;
        private Thread _thread;

        private string NL => Environment.NewLine;

        public SocketClient(string ip = "127.0.0.1", int portNumber = 50001)
        {
            IpAddr = ip;
            PortNumber = portNumber;
        }

        #region Public Methods

        /// <summary> 소켓 클라이언트 구동 </summary>
        public void Run()
        {
            if (IsRunning || (_thread != null && _thread.IsAlive))
            {
                RecordText($"[Warning] Already Running");
                return;
            }
            IsRunning = true;

            _thread = new Thread(ThreadBody);
            _thread.Start();

            RecordText($"* Run");
        }

        /// <summary> 소켓 클라이언트 종료 </summary>
        public void Close(bool sendMsgToServer = true)
        {
            if (!IsRunning)
            {
                RecordText($"[Warning] Already Closed");
                return;
            }

            // 서버에 종료 메시지 전송
            if (sendMsgToServer)
                SendToServer("Disconnect");

            IsRunning = false;

            if (_socket != null && _socket.Connected)
                _socket.Disconnect(false);
            _socket?.Close();
            _thread?.Abort();

            RecordText("* Close");
        }

        /// <summary> 재시작 </summary>
        public void Restart()
        {
            Close();
            Run();
        }

        /// <summary> 서버에 메시지 전송 </summary>
        public void SendToServer(in string strContent)
        {
            RecordText($"Send To Server : {strContent}");

            if (!IsRunning)
            {
                RecordText($"=> Failed : Socket Closed");
                return;
            }
            try
            {
                byte[] bData = Encoding.UTF8.GetBytes(strContent);
                _socket?.Send(bData, 0, bData.Length, SocketFlags.None);
            }
            catch (Exception e)
            {
                RecordText($"=> Failed : {e.Message}");
            }
        }

        /// <summary> 포트번호 설정 </summary>
        public void SetPortNumber(int n)
        {
            PortNumber = n;
        }

        #endregion // ==========================================================

        #region Private Methods

        /// <summary> 동작 기록 </summary>
        private void RecordText(in string text)
        {
            Console.WriteLine(text + NL);
            //_form.RecordText(text + NL);
        }

        #endregion // ==========================================================

        #region Body

        private void ThreadBody()
        {
            RecordText($"* Thread Body Run");

            // (1) 소켓 객체 생성 (TCP 소켓)
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // (2) 서버에 연결
            var ep = new IPEndPoint(IPAddress.Parse(IpAddr), PortNumber);

            RecordText($"* Trying To Connect");
            try
            {
                _socket.Connect(ep);
            }
            catch (Exception e)
            {
                RecordText($"[Error] Server Not Found");
                RecordText("* Close");
                Close(false);
                return;
            }
            RecordText($"* Connected To Server");

            byte[] receiverBuff = new byte[8192];

            SendToServer("Server Hi!");

            IsRunning = true;
            while (IsRunning)
            {
                int n = 0;
                try
                {
                    n = _socket.Receive(receiverBuff);
                }
                catch (SocketException se)
                {
                    // 서버에 의해 강제로 연결 종료
                    RecordText($"* Disconnected By Server");
                    Close(false);
                    return;
                }

                // 서버에서 받은 데이터가 있는 경우
                if (n > 0)
                {
                    string data = Encoding.UTF8.GetString(receiverBuff, 0, n);
                    RecordText($"Server Data : {data}");
                }
            }

            // 소켓 종료
            Close();
        }

        #endregion // ==========================================================

    }
}

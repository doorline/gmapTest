using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//serial 통신
using System.IO.Ports;
//ntrip tcp/ip 통신
using System.Net.Sockets;
using System.Net;

namespace GIS_ex_210910 {
    public partial class RTK :Form {

        #region ntrip 소켓통신 field
        //ntrip data receive buffer
        byte[ ] ntripDatabuffer = new byte[255];
        //ntrip data receive를 위한 비동기통신
        IAsyncResult ntripReceived;
        AsyncCallback ntripCallback;
        public Socket ntripSoc;
        //ntrip 서버 정보
        string id;
        string pw;
        string ntripCaster;
        int ntripPort;
        string ntripMoutpoint;
        string rdata;
        //ntrip 전송용 gga Data
        string gga;

        //ntrip 켜짐여부
        int ntripCheck;
        #endregion

        #region gps 시리얼 통신 field
        byte[ ] gpsDataBuffer;
        #endregion

        Form1 mapForm = new Form1();

        public RTK() {
            InitializeComponent();
            this.btn_serialConn.Click += Btn_serialConn_Click;            
            }
        private void RTK_Load(object sender, EventArgs e) {
            //폼 로드시 사용가능한 포트번호 가져오기
            combx_port.DataSource = SerialPort.GetPortNames();
            ntripCheck = 0;
            }

        #region GPS serial 통신
        private void Btn_serialConn_Click(object sender, EventArgs e) {

            if(!serialPort1.IsOpen)  //시리얼포트가 열려 있지 않으면
            {   
                //serialPortbox가 null일 경우에 처리 방법 추가
                serialPort1.PortName = combx_port.Text;  //콤보박스의 선택된 COM포트명을 시리얼포트명으로 지정
                serialPort1.BaudRate = 115200;  //보레이트 변경이 필요하면 숫자 변경하기
                serialPort1.DataBits = 8;
                serialPort1.StopBits = StopBits.One;
                serialPort1.Parity = Parity.None;
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived); //이것이 꼭 필요하다

                serialPort1.Open();  //시리얼포트 열기

                MessageBox.Show("포트가 열렸습니다.");
                combx_port.Enabled = false;  //COM포트설정 콤보박스 비활성화
                
                //map 로드...이걸 initialize 쪽에 넣으면 rtk 폼이 로드되기 전에 map이 먼저떠서 오류가 날 수 있음            
                this.AddOwnedForm(mapForm);
                mapForm.Show();
                } else  //시리얼포트가 열려 있으면
                  {                
                MessageBox.Show("포트가 이미 열려 있습니다.");
                }
            }
        #endregion
        #region 시리얼포트 데이터 얻기
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            //메인 쓰레드와 수신 쓰레드의 충돌 방지를 위해 Invoke 사용. MySerialReceived로 이동하여 추가 작업 실행.
            this.Invoke(new EventHandler(MySerialReceived));  
            }

        private void MySerialReceived(object s, EventArgs e){
            //여기에서 수신 데이타를 사용자의 용도에 따라 처리한다.
            //시리얼 버퍼에 수신된 데이타를 ReceiveData 읽어오기
             
            int nGpsBytes = serialPort1.BytesToRead;
            gpsDataBuffer = new byte[nGpsBytes];
            serialPort1.Read(gpsDataBuffer,0,gpsDataBuffer.Length);
            rdata = byte2string(gpsDataBuffer);
            //데이터 확인
            richTextBox_received.AppendText(rdata + "\r\n");

            //GGA 구분을 위한 분할
            //$로 분할
            string[] splitData1 = rdata.Split(new char[ ] {'$'});
            // , 로 분할
            for(int i=0 ;i<splitData1.Length ; i++) {
                string[] splitData2 = splitData1[i].Split(',');
                if(splitData2[0] == "GNGGA") {
                    gga = splitData1[i];
                    Form1.RTK_GGAdata = gga;                
                    mapForm.addRtkMarker(gga);
                    txt_checkPoint.AppendText(gga);
                    if(ntripCheck == 1) {
                        sendGGA(gga);
                        }
                    }
                }
            }

        private void btn_serialDisconn_Click(object sender, EventArgs e) {
            if(serialPort1.IsOpen)  //시리얼포트가 열려 있으면
            {
                serialPort1.Close();  //시리얼포트 닫기
                
                MessageBox.Show("포트가 닫혔습니다.");
                combx_port.Enabled = true;  //COM포트설정 콤보박스 활성화
                } else  //시리얼포트가 닫혀 있으면
                  {                
                MessageBox.Show("포트가 이미 닫혀 있습니다.");
                }
            }
        #endregion

        #region ntrip tcp/ip 통신
        //ntrip 연결
        private void btn_ntripConn_Click(object sender, EventArgs e) {
            //ntrip 연결에 필요한 정보
            //string id; string pw; string ntripCaster; int ntripPort; string ntripMoutpoint;
            ntripCaster = "fkp.ngii.go.kr";
            ntripPort = 2201;
            id = "kms931116";
            pw = "ngii";
            ntripMoutpoint = "VRS_V32";
            //ntripCaster = "gnssdata.or.kr";
            //ntripPort = 2101;
            //id = "kms931116@gmail.com";
            //pw = "gnss";
            //ntripMoutpoint = "SEJN-RTCM32";

            //소켓생성
            ntripSoc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //도메인 주소 ip 변환
            IPAddress ntripIp = Dns.GetHostAddresses(ntripCaster)[0];
            IPEndPoint ipEnd = new IPEndPoint(ntripIp, ntripPort);
            //유저 정보 변환
            string auth = ToBase64(id + ":" + pw);
            //연결
            ntripSoc.Connect(ipEnd);
            //로그인 정보 전달
            if(ntripSoc.Connected) {
                MessageBox.Show("ntrip 연결");
                string msg = "GET /" + ntripMoutpoint + " HTTP/1.1" + "\r\n";
                msg += "Ntrip-Version: Ntrip/2.0\r\n";
                msg += "Accept:*/*\r\n";
                msg += "Authorization: Basic"+auth+"\r\n";                
                msg += "\r\n";
                //소켓에 전송
                ntripSoc.Send(Encoding.ASCII.GetBytes(msg));
                } else MessageBox.Show("ntrip 연결 실패");
            ntripSoc.Blocking = false;
            waitForData(ntripSoc);
            }

        private void btn_ntripStart_Click(object sender, EventArgs e) {
            //gga ex $GPGGA,052158,4158.7333,N,09147.4277,W,2,08,3.1,260.4,M,-32.6,M,,*79               
            sendGGA(this.gga);
            ntripCheck = 1;
            }

        private void sendGGA(string gga) {
            string msg = "$" + gga;
            if(ntripSoc.Connected) {
                ntripSoc.Send(Encoding.ASCII.GetBytes(msg));
                } else {
                MessageBox.Show("NTRIP 소켓 연결이 끊겼습니다.");
                }
            }

        

        #endregion

        #region 유저 정보 변환
        private string ToBase64(string str) {
            Encoding asciiEncoding = Encoding.ASCII;
            byte[ ] byteArray = new byte[asciiEncoding.GetByteCount(str)];
            byteArray = asciiEncoding.GetBytes(str);
            return Convert.ToBase64String(byteArray, 0, byteArray.Length);
            }
        #endregion

        #region ntrip 데이터 수신부
        public void waitForData(Socket soc) {
            try {
                if(ntripCallback == null) {
                    ntripCallback = new AsyncCallback(OnDataReceived);
                    }
                }catch(Exception e2) {
                
                }
            soc.BeginReceive(ntripDatabuffer, 0, ntripDatabuffer.Length, SocketFlags.None, ntripCallback, soc);
            }

        private void OnDataReceived(IAsyncResult ar) {
            Socket soc = (Socket) ar.AsyncState;
            int iRx = soc.EndReceive(ar);
            if(iRx > 0) //if we received at least one byte
            {
                try {
                    if(ntripSoc.Connected) {
                        if(serialPort1.IsOpen) {
                            //Send RTCM data to GPS. We assume the data is valid RTCM
                            serialPort1.Write(ntripDatabuffer, 0, ntripDatabuffer.Length);
                            }
                        richTextBox_received.Invoke((MethodInvoker) delegate ( ) {
                            richTextBox_received.AppendText(byte2string(ntripDatabuffer)+"\r\n");
                            richTextBox_received.AppendText("Ntrip Data received \r\n");
                            });
                        }
                    } catch(System.Exception ex) {
                    this.Close();
                    throw (new System.Exception("Error sending RTCM data to device:" + ex.Message));
                    }
                }
            waitForData(soc);
            }

        #region ntrip 연결 끊기
        private void btn_ntripDisconn_Click(object sender, EventArgs e) {
            if(ntripSoc != null) {
                ntripSoc.Close();
                ntripSoc = null;
                }
            }
        #endregion

        private void btn_ntripStop_Click(object sender, EventArgs e) {

            }


        #endregion

        #region 데이터 변환
        public string byte2string(byte[ ] recvData) {
            //string[ ] arr_resData = new string[len];
            string resData = string.Empty;

            resData = Encoding.Default.GetString(recvData);            
            return resData;
            }

        #endregion

        #region 메세지 박스 clear
        private void btn_clear_Click(object sender, EventArgs e) {
            richTextBox_received.Clear();
            }
        #endregion

        
        }//form end
    }

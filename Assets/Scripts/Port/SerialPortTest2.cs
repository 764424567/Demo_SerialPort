using UnityEngine;
using System.IO.Ports;
using System.Text;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Threading;
using System.Net;

public class SerialPortTest2 : MonoBehaviour
{
    //Socket
    private Socket ClientServer;

    //接收
    Thread t;
    private string message;

    // Use this for initialization
    void Start()
    {
        ConnectedToServer();
    }

    // Update is called once per frame
    void Update()
    {
    }


    /// <summary>
    /// 连接服务器
    /// </summary>
    public void ConnectedToServer()
    {
        ClientServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //声明IP地址和端口
        IPAddress ServerAddress = IPAddress.Parse("127.0.0.1");
        EndPoint ServerPoint = new IPEndPoint(ServerAddress, 6000);
        //开始连接
        ClientServer.Connect(ServerPoint);
        t = new Thread(ReceiveMSG);
        t.Start();
    }


    /// <summary>
    /// 接收消息
    /// </summary>
    /// <returns>“string”</returns>
    void ReceiveMSG()
    {
        while (true)
        {
            if (ClientServer.Connected == false)
            {
                break;
            }
            byte[] data = new byte[1024];
            int length = ClientServer.Receive(data);
            message = Encoding.UTF8.GetString(data, 0, length);
            Debug.Log(message);
        }
    }

    /// <summary>
    /// 发送string类型数据
    /// </summary>
    /// <param name="input"></param>
    public void SendMSG(string input)
    {
        byte[] data = Encoding.UTF8.GetBytes(input);
        ClientServer.Send(data);
    }
}

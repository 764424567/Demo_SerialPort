using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class NetworkClient : MonoBehaviour
{

    private Socket tcpSocket;
    private string message;
    public Thread t;

    void Start()
    {
        OnBtnConnect();
    }

    public void OnBtnConnect()
    {
        //创建socket
        tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //连接服务器
        tcpSocket.Connect(IPAddress.Parse("127.0.0.1"), 6000);
        Debug.Log("连接服务器");

        ////接收消息
        //t = new Thread(ReceiveMsg);
        //t.Start();

        //接收消息
        byte[] bt = new byte[1024];
        int messgeLength = tcpSocket.Receive(bt);
        Debug.Log(ASCIIEncoding.UTF8.GetString(bt));

        //发送消息
        tcpSocket.Send(ASCIIEncoding.UTF8.GetBytes("我有个问题"));
    }

    void ReceiveMsg()
    {
        while (true)
        {
            if (tcpSocket.Connected == false)
            {
                break;
            }
            byte[] data = new byte[1024];
            int length = tcpSocket.Receive(data);
            message = Encoding.UTF8.GetString(data, 0, length);
            //Debug.Log("有消息进来");
        }
    }
}

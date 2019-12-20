using UnityEngine;
using System.IO.Ports;
using System.Text;
using UnityEngine.UI;

public class SerialPortTest1 : MonoBehaviour
{
    //定义一个串口端口资源
    private SerialPort sp = new SerialPort();
    //显示数据的text
    public Text m_TextShowData;
    //COM 的text
    public Text m_TextCOM;
    //显示帮助信息 
    public Text m_TextCOMHelp;

    //初始化串口类
    public void Init()
    {
        //使用指定的端口名称、波特率、奇偶校验位、数据位和停止位初始化 System.IO.Ports.SerialPort 类的新实例。
        //绑定端口
        sp = new SerialPort(m_TextCOM.text, 9600, Parity.None, 8, StopBits.None);
        m_TextCOMHelp.text = "串口打开成功";
        //订阅委托
        sp.DataReceived += new SerialDataReceivedEventHandler(Data_Received);
    }

    //接收数据
    private void Data_Received(object sender, SerialDataReceivedEventArgs e)
    {
        byte[] ReDatas = new byte[sp.BytesToRead];
        sp.Read(ReDatas, 0, ReDatas.Length);//读取数据
        this.Data_Show(ReDatas);//显示数据
    }

    /// <summary>
    /// 显示数据
    /// </summary>
    /// <param name="data">字节数组</param>
    public void Data_Show(byte[] data)
    {
        StringBuilder stringb = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            stringb.AppendFormat("{0:x2}" + "", data[i]);
        }
        Debug.Log(stringb.ToString());
        m_TextShowData.text = stringb.ToString();
    }

    //发送数据
    public void Data_Send(string _parameter)
    {
        Debug.Log(_parameter);
        sp.Open();
        sp.WriteLine(_parameter);
        sp.Close();
    }
}

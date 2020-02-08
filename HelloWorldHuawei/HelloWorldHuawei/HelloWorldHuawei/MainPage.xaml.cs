//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xamarin.Forms;

//namespace HelloWorldHuawei
//{
//    // Learn more about making custom code visible in the Xamarin.Forms previewer
//    // by visiting https://aka.ms/xamarinforms-previewer
//    [DesignTimeVisible(false)]
//    public partial class MainPage : ContentPage
//    {
//        public MainPage()
//        {
//            InitializeComponent();
//        }
//    }
//}

using System;
using System.IO;
using Xamarin.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Net;

namespace HelloWorldHuawei
{
    public partial class MainPage : ContentPage
    {
        string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "notes.txt");
        string _mqttMSG = string.Empty;
        string _topic = string.Empty;
        MqttClient _client;

        public MainPage()
        {
            InitializeComponent();

            if (File.Exists(_fileName))
            {
                RecvLabel.Text = File.ReadAllText(_fileName);
            }
            // create client instance
            string MQTT_BROKER_ADDRESS = "192.168.9.105";
            _client = new MqttClient(IPAddress.Parse(MQTT_BROKER_ADDRESS));

            // register to message received 
            _client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            string clientId = Guid.NewGuid().ToString();
            _client.Connect(clientId);

            // subscribe to the topic "/home/temperature" with QoS 2 
            //_client.Subscribe(new string[] { "topic/every10secMsg" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            
  
 
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
                // handle message received 
                string x = "message";
                _mqttMSG = System.Text.Encoding.UTF8.GetString(e.Message);
                //RecvLabel.Text = result;
        }
        }

        void OnSendButtonClicked(object sender, EventArgs e)
        {
            File.WriteAllText(_fileName, SendEditor.Text);
            SendEditor.Text = "Message sent";
        }

        void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if(btn.Text == "Delete1")
            {
                RecvLabel.Text = string.Empty;
            }
            else if (btn.Text == "Delete2")
            {
                SendEditor.Text = string.Empty;
            }
        }
        void OnShowMsgButtonClicked(object sender, EventArgs e)
        {
            //File.WriteAllText(_fileName, RecvLabel.Text);
            //RecvLabel.Text = "Save button clicked";
            RecvLabel.Text = _mqttMSG;

        }
        void OnSubscribeButtonClicked(object sender, EventArgs e)
        {
            _topic = TopicEditor.Text;
            _client.Subscribe(new string[] { _topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            RecvLabel.Text = "Topic: '" + _topic + "' is now subscribed";

        }
        void OnUnsubscribeButtonClicked(object sender, EventArgs e)
        {
            _topic = TopicEditor.Text;
            _client.Unsubscribe(new string[] { _topic });
            RecvLabel.Text = "Topic: '" + _topic + "' is now unsubscribed";
        }

    }
}
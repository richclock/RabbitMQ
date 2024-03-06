using System.Text;

using RabbitMQ.Client;

namespace RabbitMQTest
{
    public partial class Form1 : Form
    {
        string _host = "localhost";
        int _port = 5672;
        string _userName = "root";
        string _password = "itriD200";
        string _exchange = "MES.Exchange.Job";
        string _queue = "MES.Queue.Job";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //初始化連線資訊
            var factory = new ConnectionFactory();
            //設定 RabbitMQ 位置
            factory.HostName = _host;
            //設定 RabbitMQ port
            factory.Port = _port;
            //設定連線 RabbitMQ username
            factory.UserName = _userName;
            //設定 RabbitMQ password
            factory.Password = _password;
            //開啟連線
            using (var connection = factory.CreateConnection())
            //開啟 channel
            using (var channel = connection.CreateModel())
            {
                //宣告 exchanges，RabbitMQ提供了四種Exchange模式：fanout,direct,topic,header
                channel.ExchangeDeclare(_exchange, ExchangeType.Fanout,true);
                //宣告 queues
                channel.QueueDeclare(_queue, true, false, false, null);
                //將 exchnage、queue 依 route rule 綁定
                channel.QueueBind(_queue, _exchange, "", null);
                string message = $"Hello World-{Guid.NewGuid()}";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(_exchange, _queue, null, body);
                Console.WriteLine(" set {0}", message);
            }
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {

        }
    }
}
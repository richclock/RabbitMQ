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
            //��l�Ƴs�u��T
            var factory = new ConnectionFactory();
            //�]�w RabbitMQ ��m
            factory.HostName = _host;
            //�]�w RabbitMQ port
            factory.Port = _port;
            //�]�w�s�u RabbitMQ username
            factory.UserName = _userName;
            //�]�w RabbitMQ password
            factory.Password = _password;
            //�}�ҳs�u
            using (var connection = factory.CreateConnection())
            //�}�� channel
            using (var channel = connection.CreateModel())
            {
                //�ŧi exchanges�ARabbitMQ���ѤF�|��Exchange�Ҧ��Gfanout,direct,topic,header
                channel.ExchangeDeclare(_exchange, ExchangeType.Fanout,true);
                //�ŧi queues
                channel.QueueDeclare(_queue, true, false, false, null);
                //�N exchnage�Bqueue �� route rule �j�w
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
using System.Reflection;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory=new ConnectionFactory();
factory.HostName="localhost";
var connection=factory.CreateConnection();

var chanel=connection.CreateModel();

var queueName=chanel.QueueDeclare().QueueName;

chanel.QueueBind(queue:queueName,exchange:"logs",routingKey:string.Empty);

var consumer=new EventingBasicConsumer(chanel);

consumer.Received+=(model,ea)=>{
    var body=ea.Body.ToArray();
    var message=Encoding.UTF8.GetString(body);
    Console.WriteLine("Read the message : "+message);
};


chanel.BasicConsume(queue:queueName,autoAck:false,consumer:consumer);


Console.ReadKey();

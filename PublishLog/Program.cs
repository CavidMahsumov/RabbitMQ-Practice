using System.Text;
using RabbitMQ.Client;

var factory=new ConnectionFactory();
factory.HostName="localhost";

var connection=factory.CreateConnection();

var chanel=connection.CreateModel();

chanel.ExchangeDeclare(exchange:"logs",type:ExchangeType.Fanout);

var message=getMessage(args);

var body=Encoding.UTF8.GetBytes(message);

chanel.BasicPublish(exchange:"logs",routingKey:string.Empty,basicProperties:null,body:body);

Console.WriteLine("Mesaj Gonderildi : "+message);

static string getMessage(string[]args){
    return args.Length>0? string.Join(" ",args):"info : Hello World";
}
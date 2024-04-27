// See https://aka.ms/new-console-template for more information

using System.Runtime.Loader;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

Console.WriteLine("Hello, RabbitMQ!");

var connectionFactory = new ConnectionFactory
{
    HostName = "localhost"
};

var connection = connectionFactory.CreateConnection("rabbitmq");

/** send message **/
var sendChannel = connection.CreateModel();

var person = new Person(Guid.NewGuid(), "Rafael Coutinho", DateTime.Parse("1983/07/24"));
var personJson = JsonSerializer.Serialize(person);
var byteArray = Encoding.UTF8.GetBytes(personJson);

sendChannel.BasicPublish("curso-rabbitmq", "hr.person-created", null, byteArray);

Console.WriteLine($"Message published: {personJson}");


/** consumer **/

var consumerChannel = connection.CreateModel();

var consumer = new EventingBasicConsumer(consumerChannel);

consumer.Received += async (sender, eventArgs) =>
{
    var contentArrayBytes = eventArgs.Body.ToArray();
    var contentPerson = Encoding.UTF8.GetString(contentArrayBytes);

    var personMessage = JsonSerializer.Deserialize<Person>(contentPerson);

    Console.WriteLine($"Person received: {personMessage}");
    
    consumerChannel.BasicAck(eventArgs.DeliveryTag, false);
};

consumerChannel.BasicConsume("person-created", false, consumer);

Console.ReadLine();

public class Person
{
    public Person(Guid id, string name, DateTime birthDate)
    {
        Id = id;
        Name = name;
        BirthDate = birthDate;
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
}
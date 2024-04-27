// See https://aka.ms/new-console-template for more information

using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

Console.WriteLine("Hello, RabbitMQ!");

var connectionFactory = new ConnectionFactory
{
    HostName = "localhost"
};

var connection = connectionFactory.CreateConnection("rabbitmq");

var channel = connection.CreateModel();

var person = new Person(Guid.NewGuid(), "Rafael Coutinho", DateTime.Parse("1983/07/24"));
var personJson = JsonSerializer.Serialize(person);
var byteArray = Encoding.UTF8.GetBytes(personJson);

channel.BasicPublish("curso-rabbitmq", "hr.person-created", null, byteArray);

Console.WriteLine($"Message published: {personJson}");
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
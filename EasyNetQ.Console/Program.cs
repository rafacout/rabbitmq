// See https://aka.ms/new-console-template for more information

using EasyNetQ;
using EasyNetQ.Console;
using Newtonsoft.Json;

Console.WriteLine("Hello, EasyNetQ!");

var person = new Person(Guid.NewGuid(), "Rafael Coutinho", DateTime.Parse("1983/07/24"));

var bus = RabbitHutch.CreateBus("host=localhost");

//EasyMode.EasyModeQueue(person, bus);
AdvancedMode.AdvancedQueue(person, bus);

Console.Write("Finished!");
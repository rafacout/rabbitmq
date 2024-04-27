using Newtonsoft.Json;

namespace EasyNetQ.Console;

public static class AdvancedMode
{
    const string EXCHANGE = "curso-rabbitmq";
    const string QUEUE = "person-created";
    const string ROUTING_KEY = "hr.person-created";

    public static async void AdvancedQueue(Person person, IBus bus)
    {
        var advanced = bus.Advanced;

        var queue = advanced.QueueDeclare(QUEUE);

        var exchange = advanced.ExchangeDeclare(EXCHANGE, "topic");
        
        advanced.Publish(exchange, ROUTING_KEY, true, new Message<Person>(person));

        advanced.Consume<Person>(queue, (msg, info) =>
        {
            var json = JsonConvert.SerializeObject(msg.Body);
        });
    }
}
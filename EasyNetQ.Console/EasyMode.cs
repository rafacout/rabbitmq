using Newtonsoft.Json;

namespace EasyNetQ.Console;

public static class EasyMode
{
    public static async void EasyModeQueue(Person person, IBus bus)
    {
        await bus.PubSub.PublishAsync(person);

        await bus.PubSub.SubscribeAsync<Person>("marketing", msg =>
        {
            var json = JsonConvert.SerializeObject(msg);
        });
    }
}
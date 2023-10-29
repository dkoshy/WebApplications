

using Dapr.Client;

Console.WriteLine("Hello, - Dapr Counter Applications");
const string storeName = "statestore";
const string key = "counter";
var darpClient = new DaprClientBuilder().Build();

var counter = await darpClient.GetStateAsync<int>(storeName, key);

while (true)
{
    Console.WriteLine($"Increment Counter {++counter}");
    await darpClient.SaveStateAsync(storeName, key,counter);
    await Task.Delay(1000);
}


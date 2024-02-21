using System.Net;
using StackExchange.Redis;

namespace Redis_Sentinel.Services;

public class RedisService
{
    static ConfigurationOptions sentinelOptions => new()
    {
        EndPoints =
        {
            //burası tüm redis sentinel sunuclarının aldığı portlar.
            { "localhost", 6383 },
            { "localhost", 6384 },
            { "localhost", 6385 },
        },
        CommandMap = CommandMap.Sentinel,
        AbortOnConnectFail = false
    };

    static ConfigurationOptions masterOptions => new()
    {
        AbortOnConnectFail = false
    };

    public static async Task<IDatabase> RedisMasterDatabase()
    {
        ConnectionMultiplexer sentinelConnection = await ConnectionMultiplexer.SentinelConnectAsync(sentinelOptions);

        EndPoint masterEndPoint = null;
        foreach (System.Net.EndPoint endPoint in sentinelConnection.GetEndPoints())
        {
            IServer server = sentinelConnection.GetServer(endPoint);

            if (!server.IsConnected)
                continue;
            masterEndPoint = await server.SentinelGetMasterAddressByNameAsync("mymaster");//docker ile oluşturduğumuz seninel master name
            break;
        }

        var localMasterIP = masterEndPoint.ToString() switch
        {
            "172.17.0.2:6379" => "localhost:6379",
            "172.17.0.3:6379" => "localhost:6380",
            "172.17.0.4:6379" => "localhost:6381",
            "172.17.0.5:6379" => "localhost:6382",
        };

        ConnectionMultiplexer masterConnection = await ConnectionMultiplexer.ConnectAsync(localMasterIP);

        IDatabase database = masterConnection.GetDatabase();

        return database;
    }
}
using lego_indexor_api.Core.Interfaces.Brokers;
using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Infrastructure.Brokers;

public class RaspberryPiBroker: Broker, IRaspberryPiBroker
{
    public IEnumerable<RaspberryPi> GetRaspberryPies()
    {
        return Database.Raspberrypis.ToList();
    }

    public RaspberryPi? GetRaspberryPiById(int id)
    {
        return Database.Raspberrypis.Find(id);
    }

    public RaspberryPi? GetRaspberryPiByMacAddress(string? macAddress)
    {
        return Database.Raspberrypis.FirstOrDefault(r => r.MacAddress == macAddress);
    }

    public RaspberryPi? GetRaspberryPiByUserId(int? userId)
    {
        return Database.Raspberrypis.FirstOrDefault(r => r.UserId == userId);
    }

    public RaspberryPi CreateRaspberryPi(RaspberryPi raspberryPi)
    {
        var newRaspberryPi = Database.Raspberrypis.Add(raspberryPi).Entity;
        Database.SaveChanges();
        return newRaspberryPi;
    }

    public RaspberryPi UpdateIpAddress(RaspberryPi raspberryPi, string ip)
    {
        raspberryPi.IpAddress = ip;
        Database.SaveChanges();
        return raspberryPi;
    }

    public RaspberryPi UpdateUserId(RaspberryPi raspberryPi, int userId)
    {
        raspberryPi.UserId = userId;
        Database.SaveChanges();
        return raspberryPi;
    }
}
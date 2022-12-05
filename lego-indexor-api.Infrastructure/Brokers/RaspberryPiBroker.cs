using lego_indexor_api.Core.Interfaces.Brokers;
using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Infrastructure.Brokers;

public class RaspberryPiBroker: Broker, IRaspberryPiBroker
{
    public IEnumerable<Raspberrypi> GetRaspberryPies()
    {
        return Database.Raspberrypis.ToList();
    }

    public Raspberrypi? GetRaspberryPiById(int id)
    {
        return Database.Raspberrypis.Find(id);
    }

    public Raspberrypi? GetRaspberryPiByMacAddress(string? macAddress)
    {
        return Database.Raspberrypis.FirstOrDefault(r => r.MacAddress == macAddress);
    }

    public Raspberrypi? GetRaspberryPiByUserId(int? userId)
    {
        return Database.Raspberrypis.FirstOrDefault(r => r.UserId == userId);
    }

    public Raspberrypi CreateRaspberryPi(Raspberrypi raspberrypi)
    {
        var newRaspberryPi = Database.Raspberrypis.Add(raspberrypi).Entity;
        Database.SaveChanges();
        return newRaspberryPi;
    }

    public Raspberrypi UpdateIpAddress(Raspberrypi raspberrypi, string ip)
    {
        raspberrypi.IpAddress = ip;
        Database.SaveChanges();
        return raspberrypi;
    }
}
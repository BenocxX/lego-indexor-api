using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Core.Interfaces.Brokers;

public interface IRaspberryPiBroker
{
    IEnumerable<Raspberrypi> GetRaspberryPies();
    Raspberrypi? GetRaspberryPiById(int id);
    Raspberrypi? GetRaspberryPiByMacAddress(string? macAddress);
    Raspberrypi CreateRaspberryPi(Raspberrypi raspberrypi);
    Raspberrypi UpdateIpAddress(Raspberrypi raspberrypi, string ip);
}
using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Core.Interfaces.Brokers;

public interface IRaspberryPiBroker
{
    IEnumerable<Raspberrypi> GetRaspberryPies();
    Raspberrypi? GetRaspberryPiById(int id);
    Raspberrypi? GetRaspberryPiByMacAddress(string? macAddress);
    Raspberrypi? GetRaspberryPiByUserId(int? userId);
    Raspberrypi CreateRaspberryPi(Raspberrypi raspberryPi);
    Raspberrypi UpdateIpAddress(Raspberrypi raspberryPi, string ip);
    Raspberrypi UpdateUserId(Raspberrypi raspberryPi, int userId);
}
using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Core.Interfaces.Brokers;

public interface IRaspberryPiBroker
{
    IEnumerable<RaspberryPi> GetRaspberryPies();
    RaspberryPi? GetRaspberryPiById(int id);
    RaspberryPi? GetRaspberryPiByMacAddress(string? macAddress);
    RaspberryPi? GetRaspberryPiByUserId(int? userId);
    RaspberryPi CreateRaspberryPi(RaspberryPi raspberryPi);
    RaspberryPi UpdateIpAddress(RaspberryPi raspberryPi, string ip);
    RaspberryPi UpdateUserId(RaspberryPi raspberryPi, int userId);
}
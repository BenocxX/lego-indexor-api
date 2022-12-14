using lego_indexor_api.Core.Interfaces.Brokers;
using lego_indexor_api.Infrastructure.Brokers;

namespace lego_indexor_api.API.Services;

public class PieceService
{
    private readonly int _userId;
    private readonly IPieceBroker _pieceBroker;

    public PieceService(int userId)
    {
        _userId = userId;
        _pieceBroker = new PieceBroker();
    }

    public void Add()
    {
        
    }
}
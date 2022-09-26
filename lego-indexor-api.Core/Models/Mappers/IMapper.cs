namespace lego_indexor_api.Core.Models.Mappers;

public interface IMapper<TInput, TOutput>
{
    public TOutput ModelToRequest(TInput input);
    public TInput RequestToModel(TOutput input);
}
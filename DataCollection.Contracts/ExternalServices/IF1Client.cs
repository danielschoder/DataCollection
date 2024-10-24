using DataCollection.Contracts.F1Dtos;

namespace DataCollection.Contracts.ExternalServices
{
    public interface IF1Client
    {
        Task<List<F1Constructor>> GetConstructorsAsync(int year);

        Task<List<F1Driver>> GetDriversAsync(int year);

        Task<F1Race> GetRaceResultsAsync(int year, int round);

        Task<F1Race[]> GetRacesAsync(int year);
    }
}

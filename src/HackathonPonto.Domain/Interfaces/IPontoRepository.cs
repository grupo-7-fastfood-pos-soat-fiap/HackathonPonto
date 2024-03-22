using GenericPack.Data;
using HackathonPonto.Domain.Models;

namespace HackathonPonto.Domain.Interfaces
{
    public interface IPontoRepository : IRepository<Ponto>
    {
        Task<Ponto?> GetById(Guid id);
        Task<dynamic> GetDayByUser(DateOnly data, string cpf);
        Task<dynamic> GetMonthYearByUser(int mes, int ano, string cpf);
        dynamic GetReportMonthYearByUser(int mes, int ano, string cpf);
        int GetTotalRegistersDay(DateOnly data, Guid funcionarioId);
        void Add(Ponto ponto);
    }
}

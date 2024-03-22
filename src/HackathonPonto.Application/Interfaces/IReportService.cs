namespace HackathonPonto.Application.Interfaces
{
    public interface IReportService
    {
        void GerarDocumento(string arquivo, dynamic dados);
    }
}

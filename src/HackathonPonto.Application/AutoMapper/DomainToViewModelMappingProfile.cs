using AutoMapper;
using HackathonPonto.Application.ViewModels;
using HackathonPonto.Domain.Models;

namespace HackathonPonto.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile: Profile
    {
        public DomainToViewModelMappingProfile()
        {
            AllowNullCollections = true;            
    
            CreateMap<Funcionario, FuncionarioViewModel>()
                .ForMember( c => c.Ocupacao,
                    map => map.MapFrom(m => m.OcupacaoNavegation));
            
            CreateMap<Ocupacao, OcupacaoViewModel>();

            CreateMap<Ponto, PontoViewModel>();

        }
    }
}

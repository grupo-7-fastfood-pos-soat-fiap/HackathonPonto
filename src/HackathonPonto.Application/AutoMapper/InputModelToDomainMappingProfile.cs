using AutoMapper;
using HackathonPonto.Application.InputModels;
using HackathonPonto.Domain.Commands.FuncionarioCommands;
using HackathonPonto.Domain.Commands.PontoCommands;

namespace HackathonPonto.Application.AutoMapper
{
    public class InputModelToDomainMappingProfile:Profile
    {
        public InputModelToDomainMappingProfile()
        {            
            AllowNullCollections = true;

            //Funcionario
            CreateMap<FuncionarioInputModel, FuncionarioCreateCommand>();
            CreateMap<FuncionarioInputModel, FuncionarioUpdateCommand>();

            //Ponto
            CreateMap<PontoInputModel, PontoCreateCommand>();
        }
    }
}

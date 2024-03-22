using Dapper;
using GenericPack.Data;
using HackathonPonto.Domain.Interfaces;
using HackathonPonto.Domain.Models;
using HackathonPonto.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HackathonPonto.Infra.Data.Repository
{
    public class PontoRepository : IPontoRepository
    {
        protected readonly AppDbContext Db;
        protected readonly DbSet<Ponto> DbSet;
        protected readonly IDbConnection Connection;

        public PontoRepository(AppDbContext context, IDbConnection connection)
        {
            Db = context;
            DbSet = Db.Set<Ponto>();

            Connection = connection;
            Connection.ConnectionString = Db.Database.GetConnectionString();
        }

        public IUnitOfWork UnitOfWork => Db;

        public void Add(Ponto ponto)
        {
            DbSet.Add(ponto);
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public async Task<Ponto?> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<dynamic> GetDayByUser(DateOnly data, string cpf)
        {
            string query = @"select funcionario_id, nome, data, entrada1, saida1, entrada2, saida2, 
                            case when entrada2 is not null then  (entrada2-saida1)else '00:00:00' end as intervalo, 
                            case when quantidade <=3 then (saida1-entrada1) else
                            ((saida1-entrada1) + (saida2-entrada2)) end trabalhadas, quantidade
                            from (
                                select f.nome, p.funcionario_id, p.data, count(p.tiporegistro) as quantidade,
                                min(case when tiporegistro = 'E1' then hora end) as entrada1, 
                                min(case when tiporegistro = 'S1' then hora end) as saida1, 
                                min(case when tiporegistro = 'E2' then hora end) as entrada2, 
                                min(case when tiporegistro = 'S2' then hora end) as saida2  
                            from pontos p
                            inner join funcionarios f on p.funcionario_id = f.id
                            where f.cpf = @cpf and p.data = @data
                            group by f.nome, p.funcionario_id, p.data) as ponto;";
            var parameters = new DynamicParameters();
            parameters.Add("@cpf", cpf);
            parameters.Add("@data", data, DbType.Date);

            using (Connection)
            {
                Connection.Open();
                return await Connection.QueryAsync<dynamic>(query, parameters);
            }
        }

        public async Task<dynamic> GetMonthYearByUser(int mes, int ano, string cpf)
        {
            string query = @"select funcionario_id, nome, data, entrada1, saida1, entrada2, saida2, 
                            case when entrada2 is not null then  (entrada2-saida1)else '00:00:00' end as intervalo, 
                            case when quantidade <=3 then (saida1-entrada1) else
                            ((saida1-entrada1) + (saida2-entrada2)) end trabalhadas, quantidade
                            from (
                                select f.nome, p.funcionario_id, p.data, count(p.tiporegistro) as quantidade,
                                min(case when tiporegistro = 'E1' then hora end) as entrada1, 
                                min(case when tiporegistro = 'S1' then hora end) as saida1, 
                                min(case when tiporegistro = 'E2' then hora end) as entrada2, 
                                min(case when tiporegistro = 'S2' then hora end) as saida2  
                            from pontos p
                            inner join funcionarios f on p.funcionario_id = f.id
                            where f.cpf = @cpf and EXTRACT(YEAR FROM p.data) = @ano and EXTRACT(MONTH FROM p.data) = @mes
                            group by f.nome, p.funcionario_id, p.data) as ponto;";
            var parameters = new DynamicParameters();
            parameters.Add("@cpf", cpf);
            parameters.Add("@ano", ano);
            parameters.Add("@mes", mes);

            using (Connection)
            {
                Connection.Open();
                return await Connection.QueryAsync<dynamic>(query, parameters);
            }
        }

        public dynamic GetReportMonthYearByUser(int mes, int ano, string cpf)
        {
            string query = @$"select funcionario_id, nome, cpf, dia, TO_CHAR(Dia, 'DD/MM/YYYY') as data, entrada1, saida1, entrada2, saida2, 
                            case when entrada2 is not null then  (entrada2-saida1)else '00:00:00' end as intervalo, 
                            case when quantidade <=3 then (saida1-entrada1) else
                            ((saida1-entrada1) + (saida2-entrada2)) end trabalhadas, quantidade
                            from (
                                   select f.id AS funcionario_id, f.nome, f.cpf, series.Dia, count(p.tiporegistro) as quantidade,
                                   min(case when tiporegistro = 'E1' then hora end) as entrada1, 
                                   min(case when tiporegistro = 'S1' then hora end) as saida1, 
                                   min(case when tiporegistro = 'E2' then hora end) as entrada2, 
                                   min(case when tiporegistro = 'S2' then hora end) as saida2 
                            from generate_series('{DateOnly.FromDateTime(new DateTime(ano, mes, 1)).ToString("yyyy-MM-dd")}'::date, '{DateOnly.FromDateTime(new DateTime(ano, mes, DateTime.DaysInMonth(ano, mes))).ToString("yyyy-MM-dd")}'::date, '1 day') as series(dia)
                            cross join funcionarios as f
                            left join pontos as p ON series.dia = DATE(p.data) and f.id = p.funcionario_id
                            where f.cpf = @cpf and EXTRACT(YEAR FROM p.data) = @ano and EXTRACT(MONTH FROM p.data) = @mes
                            group by f.id, series.Dia, f.nome, f.cpf
                            order by f.id, series.Dia
                            ) as ponto;";
            
            var parameters = new DynamicParameters();
            parameters.Add("@cpf", cpf, DbType.String);
            parameters.Add("@ano", ano, DbType.Int32);
            parameters.Add("@mes", mes,DbType.Int32);

            using (Connection)
            {
                Connection.Open();
                return Connection.Query<dynamic>(query, parameters);
            }
        }

        public int GetTotalRegistersDay(DateOnly data, Guid funcionarioId)
        {
            return DbSet.AsNoTracking().Count(x => x.Data == data && x.FuncionarioId == funcionarioId);
        }
    }
}

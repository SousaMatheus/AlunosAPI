using AlunosApi.Context;
using AlunosApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunosApi.Services
{
    public class AlunosService : IAlunosService
    {
        private readonly AppDbContext _context;//dependencia com o AppDbContext

        public AlunosService(AppDbContext context)//injeta a dependencia pelo construtor
        {
            _context = context;
        }

        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            try
            {
                return await _context.Alunos.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<IEnumerable<Aluno>> GetAlunosByNome(string nome)
        {
            try
            {
                if(!string.IsNullOrEmpty(nome))
                {
                return await _context.Alunos.Where(x => x.Nome.Contains(nome)).ToListAsync();//retornar IEnumerable<Aluno>
                }
                else
                {
                    return await GetAlunos();
                }
            }
            catch
            {
                throw;
            }
        }
        public async Task<Aluno> GetAluno(int id)
        {
            try
            {
                return await _context.Alunos.FindAsync(id); //caso a entidade esteja em memoria traz o resultado
                                                            //sem a necessidade de consultar a bd
            }
            catch
            {
                throw;
            }
        }
        public async Task CreateAluno(Aluno aluno)
        {
            try
            {
                _context.Alunos.Add(aluno);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateAluno(Aluno aluno)
        {
            try
            {
                _context.Entry(aluno).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task DeleteAluno(Aluno aluno)
        {
            try
            {
                _context.Alunos.Remove(aluno);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}

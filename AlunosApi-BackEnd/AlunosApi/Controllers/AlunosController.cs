using AlunosApi.Models;
using AlunosApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunosApi.Controllers
{
    [Route("api/[controller]")]//permite definir o roteamento, gvisto que tem o middleware de roteamento, e mapeamento dos endpoints na classe startup
    [ApiController]//permite acionar automaticamente erros de validação para reposta http 400
    //[Produces("Application/json")]//por padrao produz um retorno do tipo Json, mas poderia ser outro tipo como XML
    public class AlunosController : ControllerBase//suporte ao controlador MVC sem suporte as
                                                  //views, Badrequest, notfound, ok, clientResult
    {
        private IAlunosService _alunoService;
        public AlunosController(IAlunosService alunosService)
        {
            _alunoService = alunosService;//injetado instancia do servico(ele utiliza o context) no controlador
        }
        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]  Indica quais podem ser os tipos de retorno
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos()//retorna lista de alunos. Task>Asincrono. ActionResult>pode retornar um
                                                                            //tipo derivado ou tipo da api. IAsyncEnumerable>eficiente como IEnumerable
                                                                            //é uma interação assincrona
        {
            try
            {
                var alunos = await _alunoService.GetAlunos();//retorna IEnumerable de alunos
                return Ok(alunos);
            }
            catch
            {
                //return BadRequest();
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos");
            }
        }
        [HttpGet("GetAlunosByNome")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunosByNòme([FromQuery] string nome)//recebe o parametro da consulta
        {
            try
            {
                var alunos = await _alunoService.GetAlunosByNome(nome);
                if(alunos.Count() == 0)
                    return NotFound($"Não foi encontrado o aluno com o critério '{nome}'");
                else
                    return Ok(alunos);
            }
            catch
            {
                return BadRequest("Request Inválido");
            }
        }
        [HttpGet("{id:int}", Name ="GetAluno")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);
                if(aluno.Id.Equals(id))
                    return Ok(aluno);
                
                return StatusCode(StatusCodes.Status404NotFound, $"Não foi encontrado o aluno com o id {id}");
            }
            catch
            {
                return BadRequest($"Não foi possível encontrar o aluno com o id {id}");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Aluno>> CreateAluno(Aluno aluno)
        {
            try
            {
                await _alunoService.CreateAluno(aluno);
                return CreatedAtRoute(StatusCodes.Status201Created, $"Aluno criado com sucesso");
            }
            catch
            {
                return BadRequest($"Não foi possível criar o aluno");
            }
        }
        [HttpPut("{id:int}")]//put tem que informar todos os dados, caso nao queira informar todos usar patch
        public async Task<ActionResult> UpdateAluno(int id, [FromBody]Aluno aluno)
        {
            try
            {
                if(aluno.Id == id)
                {
                    await _alunoService.UpdateAluno(aluno);
                    return Ok($"Aluno com id {id} foi atualizado com sucesso");
                }
                else
                {
                    return BadRequest($"Dados inconsistentes");
                }
            }
            catch
            {
                return BadRequest($"Não foi possível atualizar o aluno");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAluno(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);
                if(aluno != null)
                {
                    await _alunoService.DeleteAluno(aluno);
                    return Ok($"Aluno com id {id} foi deletado com sucesso");
                }
                else
                {
                    return NotFound($"Não foi encontrado aluno com id {id}");
                }
            }
            catch
            {
                return BadRequest($"Não foi possível atualizar o aluno");
            }
        }
    }
}

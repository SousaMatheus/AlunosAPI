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
    public class AlunosController : ControllerBase//utilizaremos os recursos oferecido por essa classe, suporte ao controlador MVC sem suporte as
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
    }
}

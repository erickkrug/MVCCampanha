using Microsoft.AspNetCore.Mvc;
using MVCCampanha.Models;
using System.Data.SqlClient;
using Dapper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
namespace MVCCampanha.Controllers
{
    public class ChamadasController : Controller
    {

        [HttpPost]
        public IActionResult ListarEmpresas()
        {
            try
            {
                var result = Querys.ListarEmpresas();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public IActionResult ListarServicos(int empresaId)
        {
            try
            {
                var result = Querys.ListServicos(empresaId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public IActionResult ListarUsuarios()
        {
            try
            {
                var result = Querys.ListarUsuarios();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ListMotivoDe(int idServico)
        {
            try
            {
                var result = Querys.ListMotivoDe(idServico);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public IActionResult ListCanalAtendimento()
        {
            try
            {
                var result = Querys.ListCanalAtendimento();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public IActionResult InserirDados(int EmpresaId, string Orientacao, string relato, DateTime DataAtendimento, int CanalAtendimentoId, int UsuarioId, int MotivoChamadaId, int TipoImportacao, bool TipoAtendimento, int ResultadoAtd, int Prioridade, int FuncInseridos)
        {
            List<string> LstMatriculas = new();
            ResultMatricula resultMatricula = new ResultMatricula();
            try
            {
                var result = Querys.InsertAtendimentos(EmpresaId, Orientacao, relato, DataAtendimento, CanalAtendimentoId, UsuarioId, MotivoChamadaId, TipoImportacao, TipoAtendimento, ResultadoAtd, Prioridade);
                
                //result == -1 : erro na importação
                //result == 0 : nenhum funcionario inserido
                //result > 0 : numero de funcionários inseridos

                if (result != -1)
                {
                    resultMatricula.Result = result;
                    if (TipoImportacao == 2 && result == 0)
                    {
                        // Houve falha, mas inseriu alguns atendimentos
                        Querys.ListMatriculaInexistente().ForEach(item => resultMatricula.Matriculas.Add(item));

                        return StatusCode(200, resultMatricula);
                    }
                    else if (TipoImportacao == 2 && result < FuncInseridos)
                    {

                        Querys.ListMatriculaInexistente().ForEach(item => resultMatricula.Matriculas.Add(item));

                        return StatusCode(200, resultMatricula);
                    }
                    else if (result > 0)
                    {
                        // Os atendimentos foram inseridos
                        return StatusCode(200, resultMatricula);
                    }
                    else
                    {
                        // Deu erro, nenhum atendimento feito
                        return StatusCode(500, resultMatricula);
                    }
                }
                else
                {
                    // É graxa é graxa
                    return StatusCode(500, resultMatricula);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using MVCCampanha.Models;
using System.Data.SqlClient;
using Dapper;
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



        public IActionResult ListarServicos()
        {
            try
            {
                var result = Querys.ListServicos();
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

        public IActionResult ResultadoAtd()
        {
            try
            {
                var result = Querys.ResultadoAtd();
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

            try
            {
                var result = Querys.InsertAtendimentos(EmpresaId, Orientacao, relato, DataAtendimento, CanalAtendimentoId, UsuarioId, MotivoChamadaId, TipoImportacao, TipoAtendimento, ResultadoAtd, Prioridade);

                if (result != -1)
                {
                    if (TipoImportacao == 2 && result == 0)
                    {
                        Querys.ListMatriculaInexistente().ForEach(item => LstMatriculas.Add(item));

                        return StatusCode(500, LstMatriculas);
                    }
                    else if (TipoImportacao == 2 && result < FuncInseridos)
                    {
                        Querys.ListMatriculaInexistente().ForEach(item => LstMatriculas.Add(item));

                        return StatusCode(200, LstMatriculas);
                    }
                    else if (result > 0)
                    {
                        return StatusCode(200, "Os atendimentos foram inseridos"); 
                    }
                    else
                    {
                        return StatusCode(500, "Verificar procedencia do erro");
                    }
                }
                else
                {
                    return StatusCode(500, "Ocorreu um erro ao inserir os atrendimentos");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
;
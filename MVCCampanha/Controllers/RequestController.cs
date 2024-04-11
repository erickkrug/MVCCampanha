using Microsoft.AspNetCore.Mvc;
using MVCCampanha.Models;
using System.Data.SqlClient;
using System.Data;

namespace MVCCampanha.Controllers
{
    
    public  class RequestController : ControllerBase
    {
       

        [HttpPost]
        public IActionResult ListarEmpresas()
        {
            List<DefaultObject> empresas = new List<DefaultObject>();

            try
            {
                Querys.ListEmpresas();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter empresas: {ex.Message}");
            }

            return Ok(empresas);
        }


        public IActionResult MatriculasInexistentes()
        {
            List<DefaultObject> matriculas = new List<DefaultObject>();
            try
            {
                Querys.ListMatriculaInexistente();
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Erro ao obter matriculas: {ex.Message}");
            }
            return Ok(matriculas);

        }

        public IActionResult InsertFuncionarios()
        {
            List<Atendimento> funcionarios = new List<Atendimento>();
            try
            {
                Querys.InsertFuncionario();
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Erro ao obter funcionario: {ex.Message}");
            }
            return Ok(funcionarios);
        }



        public IActionResult servicos()
        {
            List<DefaultObject> servicos = new List<DefaultObject>();
            try
            {
                Querys.ListServicos();
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Erro ao obter os tipos de servicos: {ex.Message}");
            }
            return Ok(servicos);
        }

        public IActionResult CanalAtendimento()
        {
            List<Atendimento> atendimentos = new List<Atendimento>();
            try
            {
                Querys.ListCanalAtendimento();
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Erro ao obter os tipos de Atendimento: {ex.Message}");
            }
            return Ok(atendimentos);
        }

        public IActionResult ListUsuarios()
        {
            List<Atendimento> usuarios = new List<Atendimento>();
            try
            {
                Querys.ListUsuario();
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Erro ao obter os tipos de servicos: {ex.Message}");
            }
            return Ok(usuarios);
        }

        public IActionResult InsertAtendimentos()
        {
            List<Atendimento> atendimentos = new List<Atendimento>();
            try
            {
                Querys.InsertAtendimentos();
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Erro ao obter os tipos de servicos: {ex.Message}");
            }
            return Ok(atendimentos);
                
        }
        public IActionResult MotivoChamada()
        {
            List<Atendimento> MotivoChamadas = new List<Atendimento>();
            try
            {
                Querys.ListMotivoChamada();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter os tipos de servicos: {ex.Message}");
            }
            return Ok(MotivoChamadas);
        }


    }
}

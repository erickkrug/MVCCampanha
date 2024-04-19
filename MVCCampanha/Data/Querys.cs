using Dapper;
using Microsoft.AspNetCore.Mvc;
using MVCCampanha.Models;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace MVCCampanha.Controllers
{
    public static class Querys
    {

        public static void InsertFuncionario(string IdAtendimento)
        {
            using (SqlConnection connection = new SqlConnection(Settings.SQLConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_campanha_funcionario_insert";
                command.Parameters.AddWithValue("@Ids", IdAtendimento.ToString());
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static List<string> ListMatriculaInexistente()
        {

            using (SqlConnection connection = new SqlConnection(Settings.SQLConnectionString))
            {
                try
                {
                    return connection.Query<string>(
                        @"select 
      	                sq_titu
                        From TEMP_SQ_TITU
                        where id_funcionario is null")
                        .ToList();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        public static List<DefaultObject> MatriculasInexistentes()
        {
            List<DefaultObject> matriculas = new();
            using (SqlConnection connection = new SqlConnection(Settings.SQLConnectionString))
            {
                try
                {
                    var response = connection.Query<DefaultObject>("select emp.empr_cd_empresa as 'Value', emp.empr_tx_nome_fantasia as 'Text' from empresa emp").ToList();
                    matriculas = response;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
            return matriculas;
        }

        //Exclui os Ids dos Funcionario na tabela TAMP_SQ_TITU
        public static void DeleteFuncionario()
        {
            using (SqlConnection connection = new SqlConnection(Settings.SQLConnectionString))
            {
                try
                {
                    SqlCommand comand = connection.CreateCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "delete from TEMP_SQ_TITU";
                    connection.Open();
                    comand.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }


        }

        public static List<DefaultObject> ListarEmpresas()
        {
            List<DefaultObject> empresas = new();
            using (SqlConnection connection = new SqlConnection(Settings.SQLConnectionString))
            {
                try
                {
                    var response = connection.Query<DefaultObject>("select emp.empr_cd_empresa as 'Value', emp.empr_tx_nome_fantasia as 'Text' from empresa emp").ToList();
                    empresas = response;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
            return empresas;
        }


        /// <summary>
        /// Trabalar aqui :
        /// </summary>
        /// <returns></returns>


        public static List<DefaultObject> ListServicos(int empresaId)
        {
            List<DefaultObject> servicos = new();
            using (SqlConnection connection = new SqlConnection(Settings.SQLConnectionString))
            {
                try
                {
                    string query = @"select 
	                                     serv.Id as 'Value',
	                                     serv.serv_tx_descricao as 'Text'
                                     from servico serv
	                                     inner join ContratoServico cs on cs.Servico_Id = serv.Id
	                                     inner join contrato ctr on ctr.cont_cd_contrato = cs.Contrato_Id
                                     where ctr.emp_cd_empresa = @empresaId
                                     group by serv.Id, serv.serv_tx_descricao
                                     order by serv.serv_tx_descricao";


                    var response = connection.Query<DefaultObject>(query, new
                    {
                        empresaId
                    }).ToList();
                    servicos = response;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
            return servicos;
        }

        public static List<DefaultObject> ListMotivoDe(int idServico)
        {
            List<DefaultObject> motivo = new();
            using (SqlConnection connection = new SqlConnection(Settings.SQLConnectionString))
            {
                try
                {
                    var query = @"select 
	                                   moch.moch_cd_motivoChamada as 'Value', 
	                                   moch_nm_motivoChamada as 'Text' 
                                   from motivo_chamada moch 
                                   where moch.serv_cd_servico = @idServico and
	                                   moch.moch_in_ativo = 1 and 
	                                   moch.moch_in_atende_pap = 1 and 
	                                   moch.moch_in_atende_seguros = 0";

                    var response = connection.Query<DefaultObject>(query, new
                    {
                        idServico
                    }).ToList();
                    motivo = response;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
            return motivo;
        }


        /// <summary>
        /// Trabalhar aqui
        /// </summary>
        /// <returns></returns>



        public static List<DefaultObject> ListCanalAtendimento()
        {
            List<DefaultObject> atendimento = new();
            using (SqlConnection connection = new SqlConnection(Settings.SQLConnectionString))
            {
                try
                {
                    var response = connection.Query<DefaultObject>("select atd.caat_cd_canal_atendimento as 'Value', atd.caat_nm_canalAtendimento as 'Text' from canal_atendimento atd").ToList();
                    atendimento = response;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
            return atendimento;
        }

        public static List<DefaultObject> ListarUsuarios()
        {
            List<DefaultObject> usuario = new();
            using (SqlConnection connection = new SqlConnection(Settings.SQLConnectionString))
            {
                try
                {
                    var response = connection.Query<DefaultObject>("select usr.usua_cd_usuario as 'Value', usr.usua_nm_usuario as 'Text' from usuario usr").ToList();
                    usuario = response;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
            return usuario;
        }


        public static int InsertAtendimentos(int EmpresaId, string Orientacao, string relato, DateTime DataAtendimento, int CanalAtendimentoId, int UsuarioId, int MotivoChamadaId, int TipoImportacao, bool TipoAtendimento, int ResultadoAtd, int Prioridade)
        {
            int retorno = -1;

            using (SqlConnection connection = new SqlConnection(Settings.SQLConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_campanha_atendimento_insert";
                command.Parameters.AddWithValue("@empresaId", EmpresaId);
                command.Parameters.AddWithValue("@orientacao", Orientacao);
                command.Parameters.AddWithValue("@relato", relato);
                command.Parameters.AddWithValue("@dataAtendimento", DataAtendimento);
                command.Parameters.AddWithValue("@canalId", CanalAtendimentoId);
                command.Parameters.AddWithValue("@usuarioId", UsuarioId);
                command.Parameters.AddWithValue("@motivoChamadaId", MotivoChamadaId);
                command.Parameters.AddWithValue("@tipoImportacao", TipoImportacao);
                command.Parameters.AddWithValue("@tipoAtendimento", TipoAtendimento);
                command.Parameters.AddWithValue("@ResultadoAtd", ResultadoAtd);
                command.Parameters.AddWithValue("@Prioridade", Prioridade);

                try
                {
                    connection.Open();
                    retorno = (int)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    // Log de exceção ou tratamento adequado
                    retorno = -1;
                }
                finally
                {
                    connection.Close();
                }
            } // A conexão será fechada automaticamente ao sair do bloco "using"

            return retorno;
        }
    }
}

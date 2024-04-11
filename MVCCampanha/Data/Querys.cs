using Dapper;
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
                catch(Exception)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }

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

        //Busca e retorna uma lista com as empresas
        public static List<DefaultObject> ListEmpresas()
        {
            List<DefaultObject> retorno = new List<DefaultObject>();

            using (SqlConnection conect = new SqlConnection(Settings.SQLConnectionString))
            {
                SqlCommand comand = conect.CreateCommand();
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "sp_campanha_empresa_list";

                try
                {
                    conect.Open();
                    SqlDataReader reader = comand.ExecuteReader();

                    while (reader.Read())
                    {
                        var emp = new DefaultObject(reader["empr_cd_empresa"].ToString(), reader["empr_tx_nome_fantasia"].ToString());
                        //emp.Value = reader["empr_cd_empresa"].ToString();
                        //emp.Text = reader["empr_tx_nome_fantasia"].ToString();
                        retorno.Add(emp);
                    }
                    reader.Close(); // Fechar o leitor dentro do bloco "using" também
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conect.Close();
                }
            } // A conexão será fechada automaticamente ao sair do bloco "using"

            return retorno;
        }

        public static List<DefaultObject> ListServicos()
        {
            List<DefaultObject> retorno = new List<DefaultObject>();

            using (SqlConnection conect = new SqlConnection(Settings.SQLConnectionString))
            {
                SqlCommand comand = conect.CreateCommand();
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "sp_campanha_servico_list";

                try
                {
                    conect.Open();
                    SqlDataReader reader = comand.ExecuteReader();

                    while (reader.Read())
                    {
                        var servico = new DefaultObject(reader["Id"].ToString(), reader["serv_tx_descricao"].ToString());
                        //servico.Value = reader["Id"].ToString();
                        //servico.Text = reader["serv_tx_descricao"].ToString();
                        retorno.Add(servico);
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conect.Close();
                }
            }

            return retorno;
        }

        public static List<DefaultObject> ListMotivoChamada(int IdServico)
        {
            List<DefaultObject> retorno = new List<DefaultObject>();

            using (SqlConnection connection = new SqlConnection(Settings.SQLConnectionString))
            {

                SqlCommand comand = connection.CreateCommand();
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "sp_campanha_Motivo_list";
                comand.Parameters.AddWithValue("@id", IdServico);
                IDataReader reader = null;
                try
                {


                    connection.Open();
                    reader = comand.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    foreach (DataRow dr in dt.Rows)
                    {
                        var emp = new DefaultObject(dr["moch_nm_motivoChamada"].ToString(), dr["moch_cd_motivoChamada"].ToString());
                        //emp.Value = dr["moch_cd_motivoChamada"].ToString();
                        //emp.Text = dr["moch_nm_motivoChamada"].ToString();
                        retorno.Add(emp);
                    }
                    return retorno;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                    if (reader != null)
                    {
                        reader.Close();
                        reader = null;
                    }
                }
            }

        }

        public static List<DefaultObject> ListCanalAtendimento()
        {
            List<DefaultObject> retorno = new List<DefaultObject>();

            using (SqlConnection connection = new SqlConnection(Settings.SQLConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_campanha_CanalAtencimento_list";

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var canalAtendimento = new DefaultObject(reader["caat_cd_canal_atendimento"].ToString(), reader["caat_nm_canalAtendimento"].ToString());
                        //canalAtendimento.Value = reader["caat_cd_canal_atendimento"].ToString();
                        //canalAtendimento.Text = reader["caat_nm_canalAtendimento"].ToString();
                        retorno.Add(canalAtendimento);
                    }

                    reader.Close(); // Fechar o leitor dentro do bloco "using" também
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            } // A conexão será fechada automaticamente ao sair do bloco "using"

            return retorno;
        }

        public static List<DefaultObject> ListUsuario()
        {
            List<DefaultObject> retorno = new List<DefaultObject>();

            using (SqlConnection connection = new SqlConnection(Settings.SQLConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_campanha_usuario_list";

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var usuario = new DefaultObject(reader["usua_cd_usuario"].ToString(), reader["usua_tx_apelido"].ToString());
                        //usuario.Value = reader["usua_cd_usuario"].ToString();
                        //usuario.Text = reader["usua_tx_apelido"].ToString();
                        retorno.Add(usuario);
                    }

                    reader.Close(); // Fechar o leitor dentro do bloco "using" também
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            } // A conexão será fechada automaticamente ao sair do bloco "using"

            return retorno;
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

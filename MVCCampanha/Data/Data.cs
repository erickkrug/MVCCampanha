using MVCCampanha.Controllers;
using MVCCampanha.Models;
namespace MVCCampanha.Data
{
    public static class Data
    {

        //Chama o metodo que insere os dados no banco e passa os valores
        public static int InsertAtend(List<string> ListIds)
        {
            int countId = 0;
            try
            {
                foreach (var id in ListIds)
                {
                    try
                    {
                        Querys.InsertFuncionario(id.ToString());
                        countId += 1;
                    }
                    catch (Exception ex)
                    {
                        return countId = -1;
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return countId;
        }



        //Chama o metodo para buscar as empresas
        public static List<DefaultObject> ListEmpresa()
        {
            var listEmpresas = Querys.ListarEmpresas();
            return listEmpresas;
        }

        public static List<DefaultObject> ListServico()
        {
            var listServico = Querys.ListServicos();
            return listServico;
        }

        public static List<DefaultObject> ListMotivoDe(int idServico)
        {
            var listMotivo = new List<DefaultObject>();
            try
            {
                if (idServico != -1)
                {
                    listMotivo = Querys.ListMotivoDe(idServico);
                    return listMotivo;
                }
                else
                {
                    return listMotivo;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static List<DefaultObject> ListCanalAtendimento()
        {
            var listCanalAten = Querys.ListCanalAtendimento();
            return listCanalAten;
        }

        public static List<DefaultObject> ListUsuario()
        {
            var listUsuario = Querys.ListarUsuarios();
            return listUsuario;
        }
    }

}


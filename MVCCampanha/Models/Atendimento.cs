namespace MVCCampanha.Models
{
    public class Atendimento
    {

        public int TipoImportacao { get; set; }
        public int IdEmpresa { get; set; }
        public DateTime DataAtendimento { get; set; }
        public int IdUsuario { get; set; }
        public int IdPrioridade { get; set; }
        public int ResultAtendimento { get; set; }
        public int Servico { get; set; }
        public int Motivo { get; set; }
        public int CanalAtend { get; set; }
        public int Tipo { get; set; }
        public string Relato { get; set; }
        public string Orientacao { get; set; }

    }
}

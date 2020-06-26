namespace Valtec.Models
{
    public class Ordem
    {
        public string Nome {get;set;}
        public ulong ordemNumero {get;set;}
        public string Telefone {get;set;}
        public double Orcamento {get;set;}
        public double Desconto {get;set;}

        public Ordem() {
            this.Orcamento = 0;
            this.Desconto = 0;
        }
    }
}
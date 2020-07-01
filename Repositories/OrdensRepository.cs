using System;
using System.Collections.Generic;
using System.IO;
using Valtec.Models;

namespace Valtec.Repositories
{
    public class OrdensRepository : RepositoryBase
    {
        private string PATH = $"Database/{DateTime.Now.Year}";
        public OrdensRepository()
        {
            if(!File.Exists(PATH)) {
                File.Create(PATH).Close();
            }
        }

        public bool Inserir(Ordem ordem)
        {
            var dadosOrdem = new string[] {PrepararRegistroCSV(ordem)};
            File.AppendAllLines(PATH, dadosOrdem);
            return true;
        }

        private string PrepararRegistroCSV(Ordem ordem)
        {
            return $"nome={ordem.Nome};numero_ordem={ordem.ordemNumero};telefone={ordem.Telefone};orçamento={ordem.Orcamento};desconto={ordem.Desconto}";
        }

        public Ordem ObterPor(string opcao)
        {
            var ordens = File.ReadAllLines(PATH);
            foreach(var ordem in ordens) {
                if(ExtrairValorDoCampo("nome", ordem).Equals(opcao) || ExtrairValorDoCampo("numero_ordem", ordem).Equals(opcao)) {
                    Ordem o = new Ordem();
                    o.Nome = ExtrairValorDoCampo("nome", ordem);
                    o.ordemNumero = ulong.Parse(ExtrairValorDoCampo("numero_ordem", ordem));
                    o.Telefone = ExtrairValorDoCampo("telefone", ordem);
                    o.Orcamento = double.Parse(ExtrairValorDoCampo("orçamento", ordem));
                    o.Desconto = double.Parse(ExtrairValorDoCampo("desconto", ordem));
                    return o;
                }
            }
            return null;
        }

        public List<Ordem> ObterTodos()
        {
            var linhas = File.ReadAllLines(PATH);
            List<Ordem> ordens = new List<Ordem>();
            foreach(var linha in linhas) {
                Ordem ordem = new Ordem();
                ordem.Nome = ExtrairValorDoCampo("nome", linha);
                ordem.ordemNumero = ulong.Parse(ExtrairValorDoCampo("numero_ordem", linha));
                ordem.Telefone = ExtrairValorDoCampo("telefone", linha);
                ordem.Orcamento = double.Parse(ExtrairValorDoCampo("orçamento", linha));
                ordem.Desconto = double.Parse(ExtrairValorDoCampo("desconto", linha));
                ordens.Add(ordem);
            }
            return ordens;
        }

        public List<Ordem> ObterTodosSemOrcamento()
        {
            var linhas = File.ReadAllLines(PATH);
            List<Ordem> ordensSemOrcamento = new List<Ordem>();
            foreach(var linha in linhas) {
                if(ExtrairValorDoCampo("orçamento", linha) == "0") {
                    Ordem ordem = new Ordem();
                    ordem.Nome = ExtrairValorDoCampo("nome", linha);
                    ordem.ordemNumero = ulong.Parse(ExtrairValorDoCampo("numero_ordem", linha));
                    ordem.Telefone = ExtrairValorDoCampo("telefone", linha);
                    ordem.Orcamento = double.Parse(ExtrairValorDoCampo("orçamento", linha));
                    ordem.Desconto = double.Parse(ExtrairValorDoCampo("desconto", linha));
                    ordensSemOrcamento.Add(ordem);
                }
            }
            return ordensSemOrcamento;
        }

        public void AtualizarOrdem(string opcao, Ordem ordemAtualizada) {
            Ordem ordemBuscada = ObterPor(opcao);
            int linha = 0;
            var linhas = File.ReadAllLines(PATH);
            if(ordemBuscada.Orcamento != ordemAtualizada.Orcamento) {
                ordemBuscada.Orcamento = ordemAtualizada.Orcamento;
            }
            if(ordemBuscada.Desconto != ordemAtualizada.Desconto) {
                ordemBuscada.Desconto = ordemAtualizada.Desconto;
            }

            var ordensTotais = ObterTodos();
            for(int i = 0; i < ordensTotais.Count; i++) {
                if(ordensTotais[i].Nome.ToString() == opcao
                || ordensTotais[i].ordemNumero.ToString() == opcao) {
                    linha = i;
                    break;
                }
            }

            linhas[linha] = PrepararRegistroCSV(ordemBuscada);
            File.WriteAllLines(PATH, linhas);
        }
    }
}
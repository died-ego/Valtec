using System;
using System.Collections.Generic;
using System.IO;
using Valtec.Models;
using Valtec.Repositories;

namespace Valtec
{
    class Program
    {
        static void Main(string[] args)
        {
            OrdensRepository ordensRepository = new OrdensRepository();
            Console.Clear();
            
            System.Console.WriteLine("1 - Cadastrar nova ordem");
            System.Console.WriteLine("2 - Consultar uma ordem");
            System.Console.WriteLine("0 - Sair");
            System.Console.Write("Digite a opção: ");
            string opcaoMenu = System.Console.ReadLine();

            switch(opcaoMenu)
            {
                default:
                Console.Clear();
                System.Console.WriteLine("Digite uma opção válida.");
                break;

                case "0":
                Console.Clear();
                System.Console.WriteLine("Até mais o/");
                break;

                case "1":
                Console.Clear();
                string nome;
                ulong OrdemNumero;
                string telefone;
                double orcamento;
                double desconto;
                System.Console.Write("Nome da ordem: ");
                nome = Console.ReadLine();

                System.Console.Write("Número da ordem: ");                
                OrdemNumero = ulong.Parse(Console.ReadLine());

                System.Console.Write("Telefone da ordem: ");
                telefone = Console.ReadLine();

                System.Console.Write("Orçamento: R$");
                orcamento = double.Parse(Console.ReadLine());

                System.Console.Write("Desconto: R$");
                desconto = double.Parse(Console.ReadLine());

                Ordem novaOrdem = new Ordem{
                    Nome = nome,
                    ordemNumero = OrdemNumero,
                    Telefone = telefone,
                    Orcamento = orcamento,
                    Desconto = desconto
                };
                ordensRepository.Inserir(novaOrdem);
                break;

                case "2":
                Console.Clear();
                System.Console.WriteLine("1 - Consultar todas as ordens");
                System.Console.WriteLine("2 - Pesquisar uma ordem");
                System.Console.Write("Digite a opção: ");
                string opcaoConsultar = Console.ReadLine();
                if(opcaoConsultar == "1") {
                    Console.Clear();
                    List<Ordem> ordens = new List<Ordem>();
                    ordens = ordensRepository.ObterTodos();
                    foreach(var ordem in ordens) {
                        System.Console.WriteLine($"Nome: {ordem.Nome}  Nº da Ordem: {ordem.ordemNumero}  Telefone: {ordem.Telefone}  Orçamento: R$ {ordem.Orcamento}  Desconto: RS {ordem.Desconto}");
                    }
                } else if(opcaoConsultar == "2") {
                    Console.Clear();
                    Ordem ordemBuscada = new Ordem();
                    System.Console.Write("Digite o NOME ou o Nº DA ORDEM que deseja consultar: ");
                    string opcao = Console.ReadLine();
                    ordemBuscada = ordensRepository.ObterPor(opcao);
                    Console.Clear();
                    System.Console.WriteLine($"Nome: {ordemBuscada.Nome}  Nº da Ordem: {ordemBuscada.ordemNumero}  Telefone: {ordemBuscada.Telefone}  Orçamento: R$ {ordemBuscada.Orcamento}  Desconto: RS {ordemBuscada.Desconto}");
                } else {
                    System.Console.WriteLine("Digite uma opção válida.");
                }
                break;
            }
        }
    }
}

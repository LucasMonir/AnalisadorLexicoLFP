using System;
using System.Linq;

namespace AnalisadorLexicoLFP
{
	public class Program
	{
		static void Main(string[] args)
		{
			try
			{

				string caminho = args.Length <= 0 ? string.Empty : args[0];

				if (string.IsNullOrEmpty(args[0]))
				{
					Console.WriteLine("Caminho do código fonte com extensão .lmh não informado! \n Deseja informar? (s/n)");
					if (Console.ReadLine().ToLower() == "s")
					{
						Console.WriteLine("Insira o caminho: ");
						caminho = Console.ReadLine();
						Console.WriteLine(caminho);
					}
				}

				var analisador = new AnalisadorLexico(caminho);
				var finalizou = false;

				while (analisador.leitor.CharAtual() != Utils.EndOfFile && !finalizou)
				{
					finalizou = analisador.Estado_0();
					Console.WriteLine("Simbolo reconhecido, continuando...");
				}

				if (analisador.Blocos.Where(x => x == '{').Count() != analisador.Blocos.Where(x => x == '}').Count())
					throw new Exception("Erro: Blocos de código não finalizados, toda abertura de bloco '{' deve ter um fechamento '}' e vice versa!");

				Console.WriteLine("Leitura Finalizada, sem erros encontrados!");
			}
			catch (FimDeArquivoException)
			{
				Console.WriteLine("Análise finalizada, sem erros encontrados!");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}

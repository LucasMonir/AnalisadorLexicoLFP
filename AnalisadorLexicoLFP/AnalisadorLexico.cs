using System;
using System.Collections.Generic;

namespace AnalisadorLexicoLFP
{
	public class AnalisadorLexico
	{
		public readonly Leitor leitor = new Leitor();
		public string TokenAtual = string.Empty;
		public int enumTokenAtual = 9;
		public bool espacoInicial = true;
		public List<char> Blocos = new List<char>();

		public AnalisadorLexico(string caminhoArquivo)
		{
			leitor.LerEsseArquivo(caminhoArquivo);
		}

		public bool Estado_0()
		{
			try
			{
				VerificaBlocos();

				if (leitor.VerificaProximoChar(Utils.QuebraDeLinha) || leitor.VerificaCharAtual(Utils.QuebraDeLinha))
				{
					Console.WriteLine("Quebra de linha econtrada, pulando...");
					leitor.LerProximoChar();
				}
				else if (leitor.VerificaProximoChar(Utils.Algarismos + Utils.Letras))
				{
					Console.WriteLine("Numero encontrado, processando...");
					Estado_1();
				}
				else if (leitor.VerificaProximoChar(Utils.AtribuicaoInicial))
				{
					Console.WriteLine("Encontrado inicial de atribuição '=', processando...");
					leitor.LerProximoChar();
					Estado_2();
				}
				else if (leitor.VerificaProximoChar(Utils.Espaco))
				{
					Console.WriteLine("Espaçamento encontrado, processando...");

					if (leitor.CharAtual() != ' ')
						TokenAtual += leitor.CharAtual();

					leitor.LerProximoChar();
					Estado_3();
				}
				else if (leitor.VerificaProximoChar(Utils.AbreCondicao))
				{
					Console.WriteLine("Encontrado inicial de condição, processando...");

					leitor.LerProximoChar();
					Estado_4();
					leitor.LerProximoChar();
				}
				else if (leitor.VerificaProximoChar(Utils.PontoEVirgula))
				{
					leitor.LerProximoChar();
					espacoInicial = true;
				}
				else
				{
					throw new Exception($"Caractere inválido! {leitor.CharAtual()}, caractere numero {leitor.posicaoAtual}");
				}

			}
			catch (FimDeArquivoException)
			{
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception($"Erro Léxico: {ex.Message}", ex);
			}

			return false;
		}

		private void Estado_1()
		{
			TokenAtual += leitor.CharAtual();
			leitor.LerProximoChar();
		}

		private void Estado_2()
		{
			if (leitor.VerificaProximoChar(Utils.AtribuicaoFinal))
			{
				Console.WriteLine("Encontrado final de atribuição '>', processando...");
				leitor.LerProximoChar();
				Estado_5();
			}
			else
			{
				throw new Exception("Sintaxe incorreta, uso incorreto do assignador reservado '=', deve ser seguido de '>', por favor corrigir...");
			}
		}

		private void Estado_3()
		{
			if (!espacoInicial)
				return;

			espacoInicial = false;

			if (TokenAtual == "inteiro")
				enumTokenAtual = (int)TokensReconhecidos.inteiro;
			else if (TokenAtual == "logico")
				enumTokenAtual = (int)TokensReconhecidos.logico;
			else if (TokenAtual == "literal")
				enumTokenAtual = (int)TokensReconhecidos.literal;
			else if (TokenAtual == "se")
				enumTokenAtual = (int)TokensReconhecidos.decisao;
			else if (TokenAtual == "enquanto")
				enumTokenAtual = (int)TokensReconhecidos.repeticao;

			if (enumTokenAtual == (int)TokensReconhecidos.decisao || enumTokenAtual == (int)TokensReconhecidos.decisao)
				Estado_4();

			Console.WriteLine($"Declaração de token encontrada, tipo: {TokenAtual}");

			TokenAtual = string.Empty;
		}

		private void Estado_4()
		{

			if (enumTokenAtual == 3)
			{
				leitor.LerProximoChar();
				var erro = "Erro encontrado: estrutura de decisão 'se' deve ser seguida de (condicao) e { acao }!";

				TokenAtual = null;
				TokenAtual += leitor.CharAtual();

				if (leitor.CharAtual() != '(')
					throw new EstruturaException(erro);

				leitor.LerProximoChar();

				while (leitor.CharAtual().ToString() != Utils.FechaCondicao)
					Estado_1();

				TokenAtual += leitor.CharAtual();

				if (TokenAtual[TokenAtual.Length - 1].ToString() != Utils.FechaCondicao)
					throw new EstruturaException(erro);

				leitor.LerProximoChar();

				if (leitor.VerificaProximoChar(Utils.AbreEscopo))
					Estado_6();
				else
					throw new EstruturaException(erro);
			}
			else if (enumTokenAtual == 4)
			{
				var erro = "Erro encontrado: estrutura de decisão 'enquanto' deve ser seguida de (condicao) e { acao }!";

				TokenAtual = null;
				TokenAtual += leitor.CharAtual();

				if (leitor.CharAtual() != '(')
					throw new EstruturaException(erro);

				leitor.LerProximoChar();

				while (leitor.CharAtual().ToString() != Utils.FechaCondicao)
					Estado_1();

				TokenAtual += leitor.CharAtual();

				if (TokenAtual[TokenAtual.Length - 1].ToString() != Utils.FechaCondicao)
					throw new EstruturaException(erro);

				leitor.LerProximoChar();

				if (leitor.VerificaProximoChar(Utils.AbreEscopo))
					Estado_6();
				else
					throw new EstruturaException(erro);
			}

			Console.WriteLine($"Palavra reservada detectada: {TokenAtual}!");
			ResetaBloco();
		}

		private void Estado_5()
		{
			leitor.LerProximoChar();

			if (enumTokenAtual == 0)
			{
				TokenAtual = string.Empty;

				while (leitor.CharAtual() != Utils.PontoEVirgula)
					Estado_1();

				int numero;
				bool ehNumero = int.TryParse(TokenAtual, out numero);

				if (!ehNumero)
					throw new ProblemaDeTipagemException("Erro encontrado, o valor informado para essa variável deve ser inteiro!");
			}
			else if (enumTokenAtual == 1)
			{
				TokenAtual = string.Empty;

				while (leitor.CharAtual() != Utils.PontoEVirgula)
					Estado_1();

				if (TokenAtual.Trim() != "verdadeiro" && TokenAtual.Trim() != "falso")
					throw new ProblemaDeTipagemException("Erro encontrado, o valor informado para essa variável deve ser Lógico/Booleano (verdadeiro/falso)!");
			}
			else if (enumTokenAtual == 2)
			{
				TokenAtual = string.Empty;
				var erro = "Erro encontrado, o valor informado para essa variável deve conter aspas duplas no início e fim \"texto\"!";
				leitor.LerProximoChar();

				if (leitor.CharAtual() != Utils.Aspa)
					throw new ProblemaDeTipagemException(erro);

				while (leitor.CharAtual() != Utils.PontoEVirgula)
					Estado_1();

				if (TokenAtual[TokenAtual.Length - 1] != Utils.Aspa)
					throw new ProblemaDeTipagemException(erro);
			}

			Console.WriteLine($"Valor da variável atual encontrado: {TokenAtual}");
			leitor.LerProximoChar();
			ResetaBloco();
		}

		private void Estado_6()
		{
			var erro = "Erro encontrado: A estrutura deve conter abertura e fechamento de bloco em pares, Exemplo: { }";

			leitor.LerProximoChar();
			VerificaBlocos();

			if (!leitor.VerificaCharAtual(Utils.AbreEscopo))
				throw new EstruturaException(erro);

			while (leitor.CharAtual().ToString() != Utils.FechaEscopo)
				Estado_1();

			VerificaBlocos();

			Console.WriteLine("Estrutura de repetição/decisão encontrada!");
			ResetaBloco();
			leitor.LerProximoChar();
			Estado_0();
		}

		private void VerificaBlocos()
		{
			 var caractere = leitor.CharAtual();

			if (caractere == '{' || caractere == '}')
			{
				Blocos.Add(caractere);
				return;
			}
		}

		private void ResetaBloco()
		{
			espacoInicial = true;
			TokenAtual = string.Empty;
			enumTokenAtual = 9;
		}
	}
}

using System;
using System.IO;

namespace AnalisadorLexicoLFP
{
	public class Leitor
	{
		public int posicaoAtual = 0;
		string conteudoArquivo = string.Empty;

		public char LerProximoChar()
		{
			char retorno = char.MinValue;
			if (posicaoAtual < conteudoArquivo.Length)
			{
				retorno = conteudoArquivo[posicaoAtual];
				posicaoAtual++;
			}
			Console.WriteLine(retorno);
			return retorno;
		}

		public bool VerificaProximoChar(string tipo)
		{
			if (posicaoAtual + 1 < conteudoArquivo.Length)
				return tipo.Contains(conteudoArquivo[posicaoAtual + 1]);

			throw new FimDeArquivoException("Arquivo finalizado!");
		}

		public bool VerificaProximoChar(char character)
		{
			if (conteudoArquivo[posicaoAtual + 1] != character || posicaoAtual + 1 < conteudoArquivo.Length)
				return false;

			return true;
		}

		public bool VerificaCharAtual(string tipo)
		{
			if (posicaoAtual < conteudoArquivo.Length)
				return tipo.Contains(conteudoArquivo[posicaoAtual]);

			throw new FimDeArquivoException("Arquivo finalizado!");
		}

		public void LerEsseArquivo(string file)
		{
			try
			{
				conteudoArquivo = File.ReadAllText(file);
			}
			catch (Exception ex)
			{
				throw new Exception($"Erro ao ler arquivo: {ex.Message}", ex);
			}
		}

		public char CharAtual()
		{
			if (posicaoAtual < conteudoArquivo.Length)
				return conteudoArquivo[posicaoAtual];

			return char.MinValue;
		}
	}
}

using System;

namespace AnalisadorLexicoLFP
{
	public class FimDeArquivoException : Exception
	{
		FimDeArquivoException() { }
		public FimDeArquivoException(string message) : base(message) { }

		public FimDeArquivoException(string message, Exception inner) : base(message, inner) { }
	}
}

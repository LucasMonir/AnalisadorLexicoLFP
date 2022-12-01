using System;

namespace AnalisadorLexicoLFP
{
	public class EstruturaException : Exception
	{
		EstruturaException() { }
		public EstruturaException(string message) : base(message) { }
		public EstruturaException(string message, Exception inner) : base(message, inner) { }
	}
}

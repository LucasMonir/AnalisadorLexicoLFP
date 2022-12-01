using System;

namespace AnalisadorLexicoLFP
{
	public class ProblemaDeTipagemException : Exception
	{
		ProblemaDeTipagemException() { }
		 
		public ProblemaDeTipagemException(string message) : base(message) { }

		public ProblemaDeTipagemException(string message, Exception inner) : base(message, inner) { }
	}
}

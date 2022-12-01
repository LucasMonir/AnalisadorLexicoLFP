namespace AnalisadorLexicoLFP
{
	public static class Utils
	{
		public static string Letras = "abcdefghijklmnopqrstuvxwyzABCDEFGHIJKLMNOPQRSTUVXWYZ_";
		public static string Algarismos = "0123456789";
		public static string AtribuicaoInicial = "=";
		public static string AtribuicaoFinal = ">";
		public static string AbreEscopo = "{";
		public static string FechaEscopo = "}";
		public static char PontoEVirgula = ';';
		public static string QuebraDeLinha = "\r\n\t";
		public static string Espaco = " ";
		public static string TodosCharsValidos = Letras + Algarismos;
		public static char EndOfFile = char.MinValue;
		public static char Aspa = '"';
		public static string AbreCondicao = "(";
		public static string FechaCondicao = ")";

	}

	public enum TokensReconhecidos
	{ 
		inteiro = 0, 
		logico = 1,
		literal = 2,
		decisao = 3,
		repeticao = 4
	}

}

# Estrutura da linguagem:

Todos os programas em .lmh devem ter a estrutura a seguir:
	• começar com "{" e finalizar com "}", toda abertura de bloco deve ter um fechamento
	
	• as variáveis inteiras, logicas e literais devem ser declaradas como:
		inteiro variavelInteira => 5;
		logico variavelLogica => verdadeiro;
		literal variavelLiteral => "literal";
	ou seja, tipo - nome - operador de atribuição "=>" - valor - simbolo ";"

	• os blocos de decisão e repetição, como "se" e "enquanto" devem ter a seguinte estrutura:
		se ( condição ) { ação }
		enquanto ( condição ) { ação }
	ou seja, identificador "se" ou "enquanto", codição envolta por "(" e ")", abertura de bloco com "{", código e ações e por fim fechamento de bloco "}" 
	
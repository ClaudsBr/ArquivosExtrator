if (p.cod_ambiente != AmbienteEnum.DEV && p.cod_ambiente != AmbienteEnum.PROD)
{
    throw new ArgumentException($"Falha ao verificar os dados preenchidos nas planilhas de parametrização: campo cod_ambiente deve ser DEV ou PROD na linha {parametrizacao.IndexOf(p) + 1}");
}

// Para converter uma string para um valor de enum em C#, você pode utilizar o método Enum.Parse(). 
//Por exemplo, para converter a string "PROD" para o valor correspondente do enum AmbienteEnum, 
//você pode fazer o seguinte:

string ambienteStr = "PROD";
AmbienteEnum ambienteEnum = (AmbienteEnum)Enum.Parse(typeof(AmbienteEnum), ambienteStr);

// Nesse exemplo, ambienteEnum terá o valor AmbienteEnum.PROD. Note que é importante passar o tipo do 
//enum como parâmetro para o método Enum.Parse(), usando typeof(AmbienteEnum).
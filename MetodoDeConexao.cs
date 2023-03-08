using System.Data.OleDb;

public OleDbConnection GetConnection(AmbienteEnum ambiente)
{
    string connectionString;
    switch (ambiente)
    {
        case AmbienteEnum.DEV:
            connectionString = @"Provider=IBMDADB2.1; User ID=IMSTST3; Data Source=DB2DES; Password=token:033006172:IMSTST3:F70E5ED2CF4C923C:0DA1739E2ABC0C2A";
            break;
        case AmbienteEnum.PROD:
            connectionString = @"Provider=IBMDADB2.1;User ID=IMSTST3;Data Source=DB2PRO;Token=033006172:IMSTST3:F70E5ED2CF4C923C:0DA1739E2ABC0C2A";
            break;
        default:
            throw new ArgumentException("Ambiente inválido");
    }

    return new OleDbConnection(connectionString);
}

// O método recebe como parâmetro o ambiente desejado, cria uma string de conexão correspondente e 
//retorna uma instância de OleDbConnection que pode ser usada para executar comandos no banco de dados.


// OUTRA OPÇÃO
using System.Data.OleDb;

public class ConexaoDB2
{
    private readonly string connectionStringDev = "Provider=IBMDADB2.1;User ID=IMSTST3;Data Source=DB2DES;Password=token:033006172:IMSTST3:F70E5ED2CF4C923C:0DA1739E2ABC0C2A";
    private readonly string connectionStringProd = "Provider=IBMDADB2.1;User ID=IMSTST3;Data Source=DB2PRO;Token=033006172:IMSTST3:F70E5ED2CF4C923C:0DA1739E2ABC0C2A";

    public OleDbConnection GetConnection(AmbienteEnum ambiente)
    {
        string connectionString = ambiente == AmbienteEnum.DEV ? connectionStringDev : connectionStringProd;
        return new OleDbConnection(connectionString);
    }
}

// Para utilizar esse método, basta criar uma instância da classe ConexaoDB2 e chamar o método 
//GetConnection passando como parâmetro o ambiente desejado. Por exemplo:

ConexaoDB2 conexao = new ConexaoDB2();
OleDbConnection conn = conexao.GetConnection(AmbienteEnum.DEV);
conn.Open();

// fazer operações no banco de dados utilizando a conexão

conn.Close();

// Lembrando que é necessário ter a biblioteca System.Data.OleDb referenciada no projeto para utilizar a 
//classe OleDbConnection.
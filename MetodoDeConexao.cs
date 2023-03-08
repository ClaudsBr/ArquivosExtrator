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

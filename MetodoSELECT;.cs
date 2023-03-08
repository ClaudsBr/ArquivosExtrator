using IBM.Data.DB2;

public List<AtributosValor> SelectAtributosValor(string codFami, List<string> codSeries, List<string> codAtributos, DateTime dataInicio, DateTime dataFim, AmbienteEnum ambiente)
{
    // Monta a string de conexão de acordo com o ambiente
    string connectionString;
    switch (ambiente)
    {
        case AmbienteEnum.DEV:
            connectionString = ConfigurationManager.ConnectionStrings["DB2OLEDB_DES"].ConnectionString;
            break;
        case AmbienteEnum.PROD:
            connectionString = ConfigurationManager.ConnectionStrings["DB2_CONNECTION"].ConnectionString;
            break;
        default:
            throw new ArgumentException("Ambiente inválido.");
    }

    // Monta a query com os parâmetros parametrizados
    string query = "SELECT COD_FAMI, COD_SRIE, VARCHAR_FORMAT(DAT_INIO_VIGE_ATBT, 'yyyyMMdd'), COD_ATBT, COD_TIP_ATBT, DES_VLR_ATBT " +
                   "FROM DB2GPP.TBIMVLO0 " +
                   "WHERE COD_FAMI = @codFami " +
                   "AND COD_SRIE IN (" + string.Join(",", codSeries.Select(x => "'" + x + "'")) + ") " +
                   "AND COD_ATBT IN (" + string.Join(",", codAtributos.Select(x => "'" + x + "'")) + ") " +
                   "AND DAT_INIO_VIGE_ATBT BETWEEN @dataInicio AND @dataFim";

    // Cria a lista que será preenchida com os valores do banco de dados
    List<AtributosValor> atributosValores = new List<AtributosValor>();

    // Realiza a conexão com o banco e a consulta
    using (DB2Connection connection = new DB2Connection(connectionString))
    {
        DB2Command command = new DB2Command(query, connection);
        command.Parameters.Add(new DB2Parameter("@codFami", DB2Type.VarChar, 8) { Value = codFami });
        command.Parameters.Add(new DB2Parameter("@dataInicio", DB2Type.Date) { Value = dataInicio });
        command.Parameters.Add(new DB2Parameter("@dataFim", DB2Type.Date) { Value = dataFim });

        connection.Open();
        DB2DataReader reader = command.ExecuteReader();

        // Preenche a lista com os valores da consulta
        while (reader.Read())
        {
            AtributosValor atributoValor = new AtributosValor()
            {
                cod_fami = reader.GetString(0),
                cod_srie = reader.GetString(1),
                dat_inio_vige_atbt = reader.GetString(2),
                cod_atbt = reader.GetString(3),
                cod_tip_atbt = reader.GetString(4),
                des_vlr_atbt = reader.GetString(5)
            };
            atributosValores.Add(atributoValor);
        }

        reader.Close();
        connection.Close();
    }

    return atributosValores;
}

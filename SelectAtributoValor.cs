public List<AtributoValor> SelectAtributoValor(string codFami, List<string> series, List<string> atributos, string dataInicio, string dataFim, AmbienteEnum ambiente)
{
    string connectionString = GetConnectionString(ambiente);
    string query = $"SELECT COD_FAMI, COD_SRIE, VARCHAR_FORMAT(DAT_INIO_VIGE_ATBT,'yyyyMMdd'), COD_ATBT, COD_TIP_ATBT, DES_VLR_ATBT FROM DB2GPP.TBIMVLO0 WHERE COD_FAMI = '{codFami}' AND COD_SRIE IN ({string.Join(",", series.Select(s => $"'{s}'"))}) AND COD_ATBT IN ({string.Join(",", atributos.Select(a => $"'{a}'"))}) AND DAT_INIO_VIGE_ATBT BETWEEN '{dataInicio}' AND '{dataFim}'";

    List<AtributoValor> atributosValores = new List<AtributoValor>();

    using (OleDbConnection connection = new OleDbConnection(connectionString))
    {
        OleDbCommand command = new OleDbCommand(query, connection);
        connection.Open();

        OleDbDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            AtributoValor atributoValor = new AtributoValor();
            atributoValor.Cod_fami = reader.GetString(0);
            atributoValor.Cod_srie = reader.GetString(1);
            atributoValor.Dat_inio_vige_atbt = reader.GetString(2);
            atributoValor.Cod_atbt = reader.GetString(3);
            atributoValor.Cod_tip_atbt = reader.GetString(4);
            atributoValor.Des_vlr_atbt = reader.GetString(5);

            atributosValores.Add(atributoValor);
        }

        reader.Close();
    }

    return atributosValores;
}

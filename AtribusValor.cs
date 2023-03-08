using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

public class AtributosValor
{
    public string Cod_fami { get; set; }
    public string COD_SRIE { get; set; }
    public string DAT_INIO_VIGE_ATBT { get; set; }
    public string COD_ATBT { get; set; }
    public string Cod_tip_atbt { get; set; }
    public string DES_VLR_ATBT { get; set; }
}

public class DataAccess
{
    private readonly string connectionString;

    public DataAccess(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public List<AtributosValor> GetAtributosValor(string cod_fami, List<string> cod_series, List<string> cod_atributos, DateTime data_inicial, DateTime data_final, int cod_ambiente)
    {
        string ambiente = cod_ambiente == 1 ? "DEV" : "PROD";
        string query = @"SELECT A.COD_FAMI, A.COD_SRIE, VARCHAR_FORMAT(A.DAT_INIO_VIGE_ATBT,'yyyyMMdd'), A.COD_ATBT, 'VALOR', A.DES_VLR_ATBT
                         FROM DB2GPP.TBIMVLO0 A
                         WHERE A.COD_FAMI = ? AND A.COD_SRIE IN ({0}) AND A.COD_ATBT IN ({1}) AND DAT_INIO_VIGE_ATBT BETWEEN ? AND ?";

        string seriesParam = string.Join(",", cod_series);
        string atributosParam = string.Join(",", cod_atributos);
        query = string.Format(query, seriesParam, atributosParam);

        List<AtributosValor> atributosValores = new List<AtributosValor>();

        using (OleDbConnection connection = new OleDbConnection(connectionString))
        {
            connection.Open();
            using (OleDbCommand command = new OleDbCommand(query, connection))
            {
                command.Parameters.AddWithValue("@cod_fami", cod_fami);
                command.Parameters.AddWithValue("@data_inicial", data_inicial.ToString("yyyyMMdd"));
                command.Parameters.AddWithValue("@data_final", data_final.ToString("yyyyMMdd"));

                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AtributosValor atributoValor = new AtributosValor
                        {
                            Cod_fami = reader.GetString(0),
                            COD_SRIE = reader.GetString(1),
                            DAT_INIO_VIGE_ATBT = reader.GetString(2),
                            COD_ATBT = reader.GetString(3),
                            Cod_tip_atbt = "VALOR",
                            DES_VLR_ATBT = reader.GetString(5)
                        };
                        atributosValores.Add(atrib

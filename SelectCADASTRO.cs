using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

//implementação do método de SELECT para a tabela de CADASTRO:
public class CadastroRepository
{
    private readonly ConexaoDB2 conexaoDB2;

    public CadastroRepository(ConexaoDB2 conexaoDB2)
    {
        this.conexaoDB2 = conexaoDB2;
    }

    public List<Cadastro> Select(string codFami, string serie, AmbienteEnum ambiente)
    {
        List<Cadastro> cadastros = new List<Cadastro>();

        using (OleDbConnection conn = conexaoDB2.GetConnection(ambiente))
        {
            conn.Open();

            // Define a consulta SQL com os parâmetros parametrizados
            string sql = "SELECT COD_FAMI, COD_SRIE, COD_ENTI, COD_SOCI, COD_CNTR, DAT_INIC_VIGE, DAT_FIM_VIGE, COD_MODO_PERC_TAXA, VAL_TAXA_FATU FROM DB2GPP.TBIMCAD0 WHERE COD_FAMI = ? AND COD_SRIE = ?";

            // Cria um objeto OleDbCommand com a consulta SQL e a conexão
            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                // Adiciona os parâmetros à consulta SQL
                cmd.Parameters.AddWithValue("@codFami", codFami);
                cmd.Parameters.AddWithValue("@serie", serie);

                // Executa a consulta SQL e obtém um objeto OleDbDataReader para ler os resultados
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    // Verifica se há linhas retornadas
                    if (reader.HasRows)
                    {
                        // Percorre as linhas retornadas
                        while (reader.Read())
                        {
                            // Cria um objeto Cadastro com os valores retornados na linha atual
                            Cadastro cadastro = new Cadastro
                            {
                                CodFami = reader.GetString(0),
                                CodSerie = reader.GetString(1),
                                CodEnti = reader.GetString(2),
                                CodSoci = reader.GetString(3),
                                CodCntr = reader.GetString(4),
                                DatInicVige = reader.GetDateTime(5),
                                DatFimVige = reader.GetDateTime(6),
                                CodModoPercTaxa = reader.GetString(7),
                                ValTaxaFatu = reader.GetDecimal(8)
                            };

                            // Adiciona o objeto Cadastro à lista de resultados
                            cadastros.Add(cadastro);
                        }
                    }
                }
            }
        }

        return cadastros;
    }
}

// Explicação:

   // A classe CadastroRepository encapsula a lógica de acesso à tabela de CADASTRO.
    //O construtor da classe recebe uma instância da classe ConexaoDB2 para obter uma conexão com o 
    //banco de dados.
    //O método Select recebe como parâmetros o código da família (codFami), a série (serie) e o ambiente 
    //(ambiente) e retorna uma lista de objetos Cadastro.
   // Dentro do método, é criada uma conexão com o banco de dados utilizando o método GetConnection da 
   //classe ConexaoDB2.
   // Em seguida, é definida uma consulta SQL com os parâmetros parametrizados (@codFami e @serie).
  //  É criado um objeto OleDbCommand com a consulta SQL e a conexão, e os parâmetros são adicionados à 
  //consulta utilizando o método AddWithValue.
   // A consulta é executada utilizando o método ExecuteReader() do objeto OleDbCommand, que retorna um 
   //objeto OleDbDataReader contendo os resultados da consulta.

//Os resultados são percorridos em um loop while utilizando o método Read() do objeto OleDbDataReader. 
//Dentro do loop, é criada uma instância da classe EntidadeCadastro e seus atributos são preenchidos com 
//os valores retornados pela consulta. A instância da classe é adicionada a uma lista que será retornada 
//como resultado do método.

//Por fim, a conexão é fechada e a lista de instâncias da classe EntidadeCadastro é retornada como 
//resultado do método.

//Veja abaixo o exemplo de implementação do método de SELECT para a tabela de CADASTRO:

public List<EntidadeCadastro> SelectCadastro(string codFami, string codSrie, AmbienteEnum ambiente)
{
    List<EntidadeCadastro> cadastroList = new List<EntidadeCadastro>();

    using (OleDbConnection conn = GetConnection(ambiente))
    {
        conn.Open();
        string sql = "SELECT COD_FAMI, COD_SRIE, COD_CLIE, DES_CLIE, TIP_CLIE, NUM_CPF_CNPJ, NUM_CELU_CLIE, DAT_NASC_CLIE, DAT_ATIV_CLIE, DAT_INAT_CLIE, COD_TIPO_CLIE, COD_STAT, DAT_INIO_VIGE, DAT_FIM_VIGE, COD_GRUPO_PAG, COD_EMPR, NOM_EMPR, NUM_MATR_FUNC, NOM_FUNC, NOM_LOGIN, DAT_SIST_CLIE " +
                     "FROM DB2GPP.TBIMCAD0 " +
                     "WHERE COD_FAMI = ? AND COD_SRIE = ? " +
                     "AND DAT_INIO_VIGE <= ? AND (DAT_FIM_VIGE >= ? OR DAT_FIM_VIGE IS NULL)";

        using (OleDbCommand cmd = new OleDbCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@codFami", codFami);
            cmd.Parameters.AddWithValue("@codSrie", codSrie);
            cmd.Parameters.AddWithValue("@dataVigencia", DateTime.Now);
            cmd.Parameters.AddWithValue("@dataVigencia2", DateTime.Now);

            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    EntidadeCadastro cadastro = new EntidadeCadastro();
                    cadastro.CodFami = reader.GetString(0);
                    cadastro.CodSrie = reader.GetString(1);
                    cadastro.CodClie = reader.GetString(2);
                    cadastro.DesClie = reader.GetString(3);
                    cadastro.TipClie = reader.GetString(4);
                    cadastro.NumCpfCnpj = reader.GetString(5);
                    cadastro.NumCeluClie = reader.GetString(6);
                    cadastro.DatNascClie = reader.GetDateTime(7);
                    cadastro.DatAtivClie = reader.GetDateTime(8);
                    cadastro.DatInatClie = reader.GetDateTime(9);
                    cadastro.CodTipoClie = reader.GetString(10);
                    cadastro.CodStat = reader.GetString(11);
                    cadastro.DatInioVige = reader.GetDateTime(12);
                    cadastro.DatFimVige = reader.IsDBNull(13) ? default(DateTime?) : reader.GetDateTime(13);
                    cadastro.CodGrupoPag = reader.GetString(14);
                    cadastro.CodEmpr = reader.GetString(15);
                    cadastro.NomEmpr = reader.GetString(16);
                    cadastro.NumMatrFunc = reader.GetString(17);
                    cadastro.NomFunc = reader.GetString(18);
                    cadastro.Nom

// OUTRA OPÇÃO

public List<Cadastro> ConsultarCadastros(AmbienteEnum ambiente, string codFami, List<string> codSrieList, List<string> codAtbtList)
{
    using (OleDbConnection connection = GetConnection(ambiente))
    {
        List<Cadastro> cadastros = new List<Cadastro>();
        string sql = "SELECT COD_FAMI, COD_SRIE, COD_ATBT, NOM_ATBT, IND_OBRG_ATBT, COD_ENTD_ATBT, COD_FNT_ATBT FROM DB2GPP.TBIMVCA0 WHERE COD_FAMI = @codFami AND COD_SRIE IN ({0}) AND COD_ATBT IN ({1})";
        string codSrieParams = string.Join(",", codSrieList.Select((s, i) => "@codSrie" + i));
        string codAtbtParams = string.Join(",", codAtbtList.Select((s, i) => "@codAtbt" + i));
        sql = string.Format(sql, codSrieParams, codAtbtParams);
        using (OleDbCommand command = new OleDbCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@codFami", codFami);
            for (int i = 0; i < codSrieList.Count; i++)
            {
                command.Parameters.AddWithValue("@codSrie" + i, codSrieList[i]);
            }
            for (int i = 0; i < codAtbtList.Count; i++)
            {
                command.Parameters.AddWithValue("@codAtbt" + i, codAtbtList[i]);
            }
            connection.Open();
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Cadastro cadastro = new Cadastro();
                    cadastro.CodFami = reader.GetString(0);
                    cadastro.CodSrie = reader.GetString(1);
                    cadastro.CodAtbt = reader.GetString(2);
                    cadastro.NomAtbt = reader.GetString(3);
                    cadastro.IndObrgAtbt = reader.GetString(4);
                    cadastro.CodEntdAtbt = reader.GetString(5);
                    cadastro.CodFntAtbt = reader.GetString(6);
                    cadastros.Add(cadastro);
                }
            }
        }
        return cadastros;
    }
}

//Esse método pode ser chamado passando como parâmetros o ambiente, o código da família, a lista de 
//códigos de série e a lista de códigos de atributo desejados. Por exemplo:

List<string> codSrieList = new List<string>() { "SERIE1", "SERIE2" };
List<string> codAtbtList = new List<string>() { "ATRIB1", "ATRIB2", "ATRIB3" };
List<Cadastro> cadastros = ConsultarCadastros(AmbienteEnum.DEV, "FAMILIA1", codSrieList, codAtbtList);


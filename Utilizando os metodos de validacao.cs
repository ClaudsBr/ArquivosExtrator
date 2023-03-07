//Para utilizá-las, basta chamar o método Validate passando como parâmetro a lista de objetos correspondente à entidade que deseja validar. Exemplo:


var csvHelper = new CsvHelper();

// Lendo dados da planilha Ativos
var ativosCSV = csvHelper.CSVHelper<Ativos>(arquivoAtivos);

// Validando dados da planilha Ativos
var ativosValidator = new AtivosValidator();
bool isAtivosValid = ativosValidator.Validate(ativosCSV);

// Lendo dados da planilha Atributos
var atributosCSV = csvHelper.CSVHelper<Atributos>(arquivoAtributos);

// Validando dados da planilha Atributos
var atributosValidator = new AtributosValidator();
bool isAtributosValid = atributosValidator.Validate(atributosCSV);

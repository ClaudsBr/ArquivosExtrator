using System;
using System.Collections.Generic;
using System.Linq;

namespace IM.ExtratorDB2.Ativos.Validations
{
    public static class AtivosValidation
    {
        public static bool ValidarAtivos(List<Ativos> ativos, out string mensagemErro)
        {
            if (ativos == null || !ativos.Any())
            {
                mensagemErro = "A planilha de Ativos está vazia.";
                return false;
            }

            var linhasSemPreenchimento = ativos.Where(a => string.IsNullOrWhiteSpace(a.cod_fami) ||
                                                           string.IsNullOrWhiteSpace(a.cod_seri) ||
                                                           string.IsNullOrWhiteSpace(a.cod_tipo_ativ) ||
                                                           string.IsNullOrWhiteSpace(a.nome_ativ) ||
                                                           string.IsNullOrWhiteSpace(a.nom_font)).ToList();

            if (linhasSemPreenchimento.Any())
            {
                var linhasComErro = string.Join(", ", linhasSemPreenchimento.Select(a => $"linha {ativos.IndexOf(a) + 1}"));
                mensagemErro = $"A planilha de Ativos contém linhas sem preenchimento: {linhasComErro}.";
                return false;
            }

            mensagemErro = string.Empty;
            return true;
        }
    }
}

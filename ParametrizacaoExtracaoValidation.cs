using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IM.ExtratorDB2.Ativos.Validations
{
    public class ParametrizacaoExtracaoValidation
    {
        public static void Validate(List<ParametrizacaoExtracao> parametrizacao)
        {
            foreach (var p in parametrizacao)
            {
                if (string.IsNullOrEmpty(p.cod_fami))
                {
                    throw new ArgumentException($"Falha ao verificar os dados preenchidos nas planilhas de parametrização: campo cod_fami obrigatório na linha {parametrizacao.IndexOf(p) + 1}");
                }

                if (p.cod_tip_atbt != "1" && p.cod_tip_atbt != "2")
                {
                    throw new ArgumentException($"Falha ao verificar os dados preenchidos nas planilhas de parametrização: campo cod_tip_atbt deve ser 1 ou 2 na linha {parametrizacao.IndexOf(p) + 1}");
                }

                if (p.cod_tip_atbt == "2" && (string.IsNullOrEmpty(p.data_inic) || string.IsNullOrEmpty(p.data_fim)))
                {
                    throw new ArgumentException($"Falha ao verificar os dados preenchidos nas planilhas de parametrização: campos data_inic e data_fim obrigatórios quando cod_tip_atbt = 2 na linha {parametrizacao.IndexOf(p) + 1}");
                }

                if (p.cod_tip_atbt == "1" && (!string.IsNullOrEmpty(p.data_inic) || !string.IsNullOrEmpty(p.data_fim)))
                {
                    throw new ArgumentException($"Falha ao verificar os dados preenchidos nas planilhas de parametrização: campos data_inic e data_fim devem estar vazios quando cod_tip_atbt = 1 na linha {parametrizacao.IndexOf(p) + 1}");
                }
            }
        }
    }
}

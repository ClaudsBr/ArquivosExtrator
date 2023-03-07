using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IM.ExtratorDB2.Ativos.Services
{
    public class AtributosValidator
    {
        public bool Validate(List<Atributos> atributos)
        {
            bool isValid = true;

            foreach (var atributo in atributos)
            {
                if (string.IsNullOrWhiteSpace(atributo.cod_fami) ||
                    string.IsNullOrWhiteSpace(atributo.cod_atbt) ||
                    string.IsNullOrWhiteSpace(atributo.cod_tip_atbt) ||
                    string.IsNullOrWhiteSpace(atributo.nome_atbt) ||
                    string.IsNullOrWhiteSpace(atributo.desc_atbt))
                {
                    Console.WriteLine($"Falha ao verificar os dados preenchidos na planilha de Atributos (linha {atributos.IndexOf(atributo) + 2}): Algum campo obrigatório não foi preenchido.");
                    isValid = false;
                }
            }

            return isValid;
        }
    }
}

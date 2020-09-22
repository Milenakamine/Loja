using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Senai.EfCore.Tarde.Utils
{
    public class Moeda
    {
        [JsonPropertyName("USD")]
        public MoedaInfo MoedaInfo { get; set; }

        public float GetDolarValue()
        {
            float dolarHoje = 7;

            using (var client = new HttpClient())
            {
                
                client.BaseAddress = new System.Uri("https://economia.awesomeapi.com.br/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                                          // GET (action)
                HttpResponseMessage response = client.GetAsync("all/USD-BRL").Result;
          
                if (response.IsSuccessStatusCode)
                {
                    string cotacao = response.Content.ReadAsStringAsync().Result;
                    Moeda convertido = JsonSerializer.Deserialize<Moeda>(cotacao);

                    dolarHoje = float.Parse(convertido.MoedaInfo.Valor.Replace('.', ','));
                }
            }

            return dolarHoje;
        }



    }


    public class MoedaInfo
    {
        [JsonPropertyName("high")]
        public string Valor { get; set; }
    }

}

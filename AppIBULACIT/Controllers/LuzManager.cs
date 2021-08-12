using AppIBULACIT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AppIBULACIT.Controllers
{
    public class LuzManager
    {
        string UrlBase = "http://localhost:49220/api/Luz/";

        HttpClient GetClient(string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            return httpClient;
        }

        public async Task<IEnumerable<Luz>> ObtenerLuzFacturas(string token, string codigo)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(string.Concat(UrlBase, codigo));

            return JsonConvert.DeserializeObject<IEnumerable<Luz>>(response);
        }

        public async Task<Luz> Ingresar(Luz luzFactura, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(luzFactura),
                Encoding.UTF8,
                "application/json"));

            return JsonConvert.DeserializeObject<Luz>(await
                response.Content.ReadAsStringAsync());
        }

        public async Task<Luz> Actualizar(Luz luzFactura, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(luzFactura),
                Encoding.UTF8,
                "application/json"));

            return JsonConvert.DeserializeObject<Luz>(await response.
                Content.ReadAsStringAsync());
        }

        public async Task<string> Eliminar(string id, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(UrlBase, id));

            return JsonConvert.DeserializeObject<string>(await
                response.Content.ReadAsStringAsync());
        }
    }
}
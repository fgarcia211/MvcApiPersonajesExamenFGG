using MvcApiPersonajesExamenFGG.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcApiPersonajesExamenFGG.Services
{
    public class ServiceApiPersonajes
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiPersonajes(IConfiguration configuration)
        {
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiPersonajesExamen");
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsyncGet<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                HttpResponseMessage response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Personaje>> GetPersonajesAPI()
        {
            string request = "/api/Personajes";
            return await this.CallApiAsyncGet<List<Personaje>>(request);
        } 

        public async Task<List<Personaje>> GetPersonajesSerieAPI(int idserie)
        {
            string request = "/api/Personajes/GetPersonajesSerie/" + idserie;
            return await this.CallApiAsyncGet<List<Personaje>>(request);
        }

        public async Task<Personaje> FindPersonajeAPI(int idpersonaje)
        {
            string request = "/api/Personajes/" + idpersonaje;
            return await this.CallApiAsyncGet<Personaje>(request);
        }

        public async Task PostPersonajeAPI(Personaje personaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Personajes";

                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                string json = JsonConvert.SerializeObject(personaje);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }

        public async Task PutPersonajeAPI(Personaje personaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Personajes";

                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                string json = JsonConvert.SerializeObject(personaje);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }

        public async Task DeletePersonajeAPI(int idpersonaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Personajes/" + idpersonaje;

                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();

                HttpResponseMessage response = await client.DeleteAsync(request);
            }
            
        }
    }
}

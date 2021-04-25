using GameLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class RestService : IRestService
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly UrlService urlService = new UrlService();

        public async Task<ApiResponse> Login(string userName, string hashedPwd)
        {
            var json = JsonConvert.SerializeObject(new User() { Id= 0, Username = userName, Password = hashedPwd });
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://localhost:44384/api/users/login", data);
            //var response = await httpClient.PostAsync(urlService.ApiAddress + "/users/login", data); for hosting in azure

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseString);
                return apiResponse;
            }

            return new ApiResponse(false, "", 0);
        }

        public async Task<ApiResponse> AddUser(string userName, string hashedPwd)
        {
            var json = JsonConvert.SerializeObject(new User() { Username = userName, Password = hashedPwd });
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://localhost:44384/api/users/add", data);
            //var response = await httpClient.PostAsync(urlService.ApiAddress + "/users/add", data); for hosting in azure

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseString);

                return apiResponse;
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return new ApiResponse(false, "The user already exsist.", 0);
            }

            return new ApiResponse(false, "Try again.", 0);
        }
    }

    public interface IRestService
    {
        Task<ApiResponse> Login(string userName, string hashedPassword);

        Task<ApiResponse> AddUser(string userName, string hashedPassword);
    }
}

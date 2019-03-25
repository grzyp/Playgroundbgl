using BGL.Git.Service.Models;
using BGL.Git.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace BGL.Git.Service.Services
{
    public class Client : IClient
    {
        private readonly HttpClient _httpClient;

        public Client()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["HttpClientUri"])
            };

            //Github requires user-agent header otherwise the request will be forbidden
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "request");
        }

        /// <summary>
        /// Simply return full HTTPResponseMessage (no matter what request we make)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<GitUser> GetUserRequest(string path)
        {
            var result = await _httpClient.GetAsync(path);
            if(result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsAsync<GitUser>();
            }
            return null;
        }

        public async Task<List<Repository>> GetRepositoriesRequest(string path)
        {
            var result =  await _httpClient.GetAsync(path);
            if(result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsAsync<List<Repository>>();
            }
            return null;
        }

    }
}

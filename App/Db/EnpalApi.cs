using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using App.Models;

namespace App.Db 
{
    public class EnpalApi
    {
        private readonly string BaseUrl = "";

        private readonly HttpClient Client = new();

        public EnpalApi(string baseUrl) {
            this.BaseUrl = baseUrl;
        }


        public async Task<IEnumerable<PowerGenerationRecord>> GetPowerGenerationData() {
            var url = BaseUrl + "PowerGenerationRecord";
            try 
            {
                HttpResponseMessage response = await this.Client.GetAsync(url);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the JSON content from the response
                    string jsonContent = await response.Content.ReadAsStringAsync();

                    if (jsonContent == null) {
                        return new List<PowerGenerationRecord>();
                    }
                    IEnumerable<PowerGenerationRecord> records = JsonSerializer.Deserialize<IEnumerable<PowerGenerationRecord>>(
                        jsonContent, new JsonSerializerOptions {
                            PropertyNameCaseInsensitive = true
                        });

                    return records;
                }
                else
                {
                    Console.WriteLine($"Failed to fetch data. Status code: {response.StatusCode}");
                    return new List<PowerGenerationRecord>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<PowerGenerationRecord>();
            }
        }
    }
}


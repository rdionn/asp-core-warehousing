using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Warehouse.Services
{
    public class RajaOngkirService {
        private String _apiKey;
        private HttpClient _httpClient;

        public RajaOngkirService(String apiKey) {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("key", _apiKey);
            _httpClient.BaseAddress = new Uri("https://api.rajaongkir.com/");
        }

        public async Task<RajaOngkirResponse<List<RajaongkirProvince>>> GetProvices() {
            try {
                String responseJson = await _httpClient.GetStringAsync("starter/province");
                Console.WriteLine(responseJson);
                RajaOngkirResponse<List<RajaongkirProvince>> provinces = JsonSerializer.Deserialize<RajaOngkirResponse<List<RajaongkirProvince>>>(responseJson);
                return provinces;
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            return null;
        }
    }

    public class RajaOngkirResponse<T> {
        [JsonPropertyName("rajaongkir")]
        public RajaOngkirData<T> Data { get; set; }
    }

    public class RajaOngkirData<T> {
        [JsonPropertyName("results")]
        public T Result { get; set; }
    }

    public class RajaongkirProvince {
        [JsonPropertyName("province_id")]
        public String ProvinceId { get; set; }

        [JsonPropertyName("province")]
        public String Province { get; set; }
    }

    public class RajaongkirCity {
        [JsonPropertyName("province_id")]
        public int ProvinceId { get; set; }

        [JsonPropertyName("province")]
        public String Province { get; set; }

        [JsonPropertyName("type")]
        public String Type { get; set; }

        [JsonPropertyName("city_name")]
        public String City { get; set; }

        [JsonPropertyName("postal_code")]
        public String Postal { get; set; }
    }
}
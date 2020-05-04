using Newtonsoft.Json;
using SalesOrder.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrder.Client.Services
{
    public static class ApiService
    {

        public static async Task<bool> RegisterUser(string name, string email, string password)
        {
            var register = new Register()
            {
                Name = name,
                Email = email,
                Password = password
            };

            var json = JsonConvert.SerializeObject(register);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/Accounts/Register", content);

            if (!response.IsSuccessStatusCode) return false;

            return true;
        }

        public static async Task<bool> Login(string email, string password)
        {
            var login = new Login()
            {
                Email = email,
                Password = password
            };

            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/Accounts/Login", content);

            if (!response.IsSuccessStatusCode) return false;

            var jsonResult = await response.Content.ReadAsStringAsync();
            Token result = JsonConvert.DeserializeObject<Token>(jsonResult);
            Preferences.AccessToken = result.Access_Token;
            Preferences.UserId = result.User_Id;
            Preferences.UserName = result.User_Name;

            return false;
        }

        public static async Task<List<Category>> GetCategories()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.AccessToken);
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Categories");

            return JsonConvert.DeserializeObject<List<Category>>(response);
        }

        public static async Task<Product> GetProductById(int productId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.AccessToken);
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Products/" + productId);

            return JsonConvert.DeserializeObject<Product>(response);
        }

        public static async Task<List<ProductByCategory>> GetProductByCategory(int categoryId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.AccessToken);
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Products/ProductsByCategory/" + categoryId);

            return JsonConvert.DeserializeObject<List<ProductByCategory>>(response);
        }

        public static async Task<List<PopularProduct>> GetPopularProducts()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.AccessToken);
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Products/PopularProducts");

            return JsonConvert.DeserializeObject<List<PopularProduct>>(response);
        }

        public static async Task<bool> AddItemsInCart(AddToCart addToCart)
        {
            var json = JsonConvert.SerializeObject(addToCart);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.AccessToken);
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/ShoppingCartItems", content);

            if (!response.IsSuccessStatusCode) return false;

            return false;
        }

        public static async Task<CartSubTotal> GetCartSubTotal(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.AccessToken);
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/ShoppingCartItems/SubTotal/" + userId);

            return JsonConvert.DeserializeObject<CartSubTotal>(response);
        }

        public static async Task<List<ShoppingCartItem>> GetShoppingCartItem(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.AccessToken);
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/ShoppingCartItems/" + userId);

            return JsonConvert.DeserializeObject<List<ShoppingCartItem>>(response);
        }

        public static async Task<TotalCartItem> GetTotalCartItems(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.AccessToken);
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/ShoppingCartItems/TotalItems/" + userId);

            return JsonConvert.DeserializeObject<TotalCartItem>(response);
        }

        public static async Task<bool> ClearShoppingCart(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.AccessToken);
            var response = await httpClient.DeleteAsync(AppSettings.ApiUrl + "api/ShoppingCartItems/" + userId);

            if (response.IsSuccessStatusCode) return true;

            return false;
        }

        public static async Task<OrderResponse> PlaceOrder(Order order)
        {
            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.AccessToken);
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/Orders", content);
            var jsonResult = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<OrderResponse>(jsonResult);
        }

        public static async Task<List<OrderByUser>> GetOrdersByUser(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.AccessToken);
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Orders/OrdersByUser/" + userId);

            return JsonConvert.DeserializeObject<List<OrderByUser>>(response);
        }

        public static async Task<List<Order>> GetOrderDetails(int orderId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.AccessToken);
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Orders/OrderDetails/" + orderId);

            return JsonConvert.DeserializeObject<List<Order>>(response);
        }
    }
}

using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IgniteAdmin.Services
{
    public class AccessTokenStorage
    {
        private IJSRuntime _jsRuntime;

        public AccessTokenStorage(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<string> GetTokenAsync()
            => await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "accessToken");

        public async Task SetTokenAsync(string token)
        {
            if (token == null)
            {
                await _jsRuntime.InvokeAsync<object>("localStorage.removeItem",
                                                                "accessToken");
            }
            else
            {
                await _jsRuntime.InvokeAsync<object>("localStorage.setItem",
                                                       "accessToken", token);
            }


        }
    }
}

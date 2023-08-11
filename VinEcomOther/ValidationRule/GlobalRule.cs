using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinEcomOther.ValidationRule
{
    public static class GlobalRule
    {
        public static async Task<IRuleBuilderOptions<T, string>> IsImageUrlAsync<T> (this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.MustAsync(async (imageUrl, c)=>
            {
                using HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(imageUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                string contentType = response.Content.Headers.ContentType.MediaType;
                return contentType.StartsWith("image/");
            }); 
        }
    }
}

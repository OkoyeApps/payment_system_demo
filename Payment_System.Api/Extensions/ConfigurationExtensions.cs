﻿using Microsoft.Extensions.Configuration;

namespace Payment_System.Api.Extensions
{
    public static class ConfigurationExtensions
    {
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);

            return model;
        }
    }
}
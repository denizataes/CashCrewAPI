        using System;
    using System.Configuration;
    using System.Xml.Linq;
        using AutoMapper;
        using Entities.DataTransferObjects;
        using Entities.Exceptions;
        using Entities.Models;
        using Entities.RequestFeatures;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Repositories.Contracts;
        using Services.Contracts;

        namespace Services
        {
            public class LoginManager : ILoginService
            {
                private readonly IRepositoryManager _manager;
                private readonly ILoggerService _logger;
                private readonly IMapper _mapper;
                private readonly IConfiguration _configuration;


        public LoginManager(IRepositoryManager manager,
                    ILoggerService logger,
                    IMapper mapper,
                    IConfiguration configuration)
                {
                    _manager = manager;
                    _logger = logger;
                    _mapper = mapper;
                    _configuration = configuration;

        }

        public async Task<LoginResponseModel> LoginAsync(LoginDto user)
                {
                    try
                   { 
                
                        var endpoint = _configuration.GetSection("AppSettings:IdentityEndpoint");
                        var requestUri = $"{endpoint.Value}/connect/token"; // Özel URL oluşturuluyor.

                        var requestData = new List<KeyValuePair<string, string>>
          {
              new KeyValuePair<string, string>("client_id", "ResourceOwnerPasswordClient"),
              new KeyValuePair<string, string>("client_secret", "ResourceOwnerPasswordSecret"),
              new KeyValuePair<string, string>("grant_type", "password"),
              new KeyValuePair<string, string>("username", user.Username),
              new KeyValuePair<string, string>("password", user.Password)
          };

                            var content = new FormUrlEncodedContent(requestData);

                        // HttpClient oluşturma
                        using (var client = new HttpClient())
                        {
                            var response = await client.PostAsync(requestUri, content);
                            var responseContent = await response.Content.ReadAsStringAsync();
                            JObject obj = JObject.Parse(responseContent);

                            var token = obj.Value<string>("access_token");
                            var errorDescription = obj.Value<string>("error") + " | " + obj.Value<string>("error_description");

                            if (string.IsNullOrWhiteSpace(errorDescription.Replace(" | ", "")))
                            {
                                return new LoginResponseModel { Message = "Success", Token = token };
                            }
                            else
                            {
                                return new LoginResponseModel { Message = errorDescription };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return new LoginResponseModel { Message = ex.Message + ex.InnerException?.Message };
                    }
                }
            }
        }


using RestSharp;
using SavedMessages.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;

namespace SavedMessages.Helpers
{
    public class RestSharpHelper
    {   //59628
           private string WebApiUrl = ConfigurationManager.AppSettings["WebApiUrl"];
        //private string Url = "http://localhost:99/";

        public IRestResponse Execute(
            RestServiceNames methodName,
            Method method,
            Dictionary<string, object> parameters = null,
            bool isAuthorized = false,
            DataFormat dataFormat = DataFormat.Json)
        {
            return !isAuthorized
                ? ExecuteImpl(methodName, method, parameters, null, dataFormat)
                : ExecuteImpl(methodName, method, parameters, GetAccessToken(), dataFormat);
        }

        public IRestResponse<TResult> Execute<TResult>(
           RestServiceNames methodName,
           Method method,
           Dictionary<string, object> parameters = null,
           bool isAuthorized = false,
           DataFormat dataFormat = DataFormat.Json)
           where TResult : new()
        {
            return !isAuthorized
                ? ExecuteImpl<TResult>(methodName, method, parameters, null, dataFormat)
                : ExecuteImpl<TResult>(methodName, method, parameters, GetAccessToken(), dataFormat);
        }

        private IRestResponse ExecuteImpl(
            RestServiceNames methodName,
            Method method,
            Dictionary<string, object> parameters,
            string authorizedToken,
            DataFormat dataFormat)
        {
            RestClient client = new RestClient(WebApiUrl);
            RestRequest request = CreateRequest(methodName, method, parameters, authorizedToken, dataFormat);
            IRestResponse response = client.Execute(request);
            return response;
        }

        private IRestResponse<TResult> ExecuteImpl<TResult>(
            RestServiceNames methodName,
            Method method,
            Dictionary<string, object> parameters,
            string authorizedToken,
            DataFormat dataFormat)
            where TResult : new()
        {
            RestClient client = new RestClient(WebApiUrl);
            RestRequest request = CreateRequest(methodName, method, parameters, authorizedToken, dataFormat);
            IRestResponse<TResult> response = client.Execute<TResult>(request);
            return response;
        }

        private RestRequest CreateRequest(
            RestServiceNames methodName,
            Method method,
            Dictionary<string, object> parameters,
            string authorizedToken,
            DataFormat dataFormat)
        {
            string methodUrl = ConfigurationManager.AppSettings[methodName.ToString()];
            UpdateMethodNameWithParameters(ref methodUrl, parameters);

            RestRequest request = new RestRequest(methodUrl, method);
            request.Parameters.Clear();
            request.AddHeader("Accept", "application/json");
            if (authorizedToken != null)
            {
                request.AddHeader("Authorization", authorizedToken);
            }

            AddParameters(request, parameters, dataFormat);

            return request;
        }

        private void UpdateMethodNameWithParameters(ref string methodName,
            Dictionary<string, object> parameters)
        {
            if (parameters == null)
                return;

            var parametersList = parameters.ToList();

            foreach (var item in parametersList)
            {
                string pattern = $"{{{item.Key}}}";

                if (methodName.ToString().Contains(pattern))
                {
                    methodName = methodName.Replace(pattern, $"{item.Value}");
                    parameters.Remove(item.Key);
                }
            }
        }

        private void AddParameters(RestRequest request,
            Dictionary<string, object> parameters, DataFormat dataFormat)
        {
            if (parameters == null)
                return;

            foreach (var parameter in parameters)
            {
                if (dataFormat == DataFormat.Json)
                {
                    if (parameter.Key.ToUpper() == "BODY")
                        request.AddJsonBody(parameter.Value);
                }
                else
                {
                    request.AddParameter(parameter.Key, parameter.Value,
ParameterType.RequestBody);
                }
            }
        }


        private string GetAccessToken()
        {
            Token token = (Token)HttpContext.Current.Session["access_token"];
            if (token != null)
            {
                var response = ExecuteImpl<Object>(RestServiceNames.GetVersion, Method.GET, null,
                    $"{token.TokenType} {token.AccessToken}", DataFormat.None);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    token = null;
                    HttpContext.Current.Session["access_token"] = null;
                }
            }
            if (token == null)
            {
                System.Web.HttpCookie phoneBookCookie =
                    HttpContext.Current.Request.Cookies["BlogsCookie"];

                var response = ExecuteImpl<Token>(RestServiceNames.GetAccessToken,
                    Method.POST,
                    new Dictionary<string, object>()
                    {
                        { "application/json",
                            $"grant_type=refresh_token&refresh_token={phoneBookCookie.Value}" }
                    },
                    null,
                    DataFormat.None);
                token = response.Data;

                phoneBookCookie.Value = token.RefreshToken;
                HttpContext.Current.Response.Cookies.Add(phoneBookCookie);
                HttpContext.Current.Session["access_token"] = token;
            }

            return $"{token.TokenType} {token.AccessToken}";
        }
    }
}
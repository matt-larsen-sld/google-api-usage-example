using System;
using Google.Apis.Services;
using System.IO;
using Google.Apis.Admin.Directory.directory_v1;
using Google.Apis.Auth.OAuth2;

namespace google_admin_api_list_users
{
    class Program
    {
        static void Main(string[] args)
        {
            GoogleCredential googleCred;
            string impersonatedUser = "matt.larsen@connorgp.com";
            string domain = "connorgp.com";

            Console.WriteLine("Get G-Suite Users Sample.");
            Console.WriteLine("=========================");

            string credPath = "C:\\sources\\google-admin-api-list-users\\input\\exampleprojecttoberemoved-4a1fefccb001.json";
            var scopes = new[] { DirectoryService.Scope.AdminDirectoryUserReadonly };

            using (var stream = new FileStream(credPath, FileMode.Open, FileAccess.Read))
            {
                googleCred = GoogleCredential.FromStream(stream).CreateScoped(scopes).CreateWithUser(impersonatedUser);
            }

            var initializer = new BaseClientService.Initializer()
            {
                HttpClientInitializer = googleCred,
                ApplicationName = "InactiveUserAccountManagerApp"
            };

            var directoryService = new DirectoryService(initializer);
            var usersRequest = directoryService.Users.List();
            usersRequest.Domain = domain;
            var response = usersRequest.Execute();
            
            Console.WriteLine($"{response.UsersValue.Count} users found from '{domain}'.");
        }
    }
}

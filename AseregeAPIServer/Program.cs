using System.Net;
using System.Text;
using AseregeEdgar.Manager;
using Model;
using MySqlX.XDevAPI.CRUD;
using Newtonsoft.Json;

namespace AseregeAPIServer
{
    class AseregeAPIServer
    {
        static void Main(string[] args)
        {
            HttpListener listener = new HttpListener();
            int port = 4321;
            listener.Prefixes.Add($"http://localhost:{port}/api/");
            listener.Prefixes.Add($"http://127.0.0.1:{port}/api/");
            
            if (args.Length > 0 )
            {
                listener.Prefixes.Add($"{args[0]}:{port}/api/");
            }
            
            listener.Start();
            Console.WriteLine($"Server started. Listening to port {port}.");

            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                string responseString = "";

                if (request.Url.LocalPath == "/api/user")
                {
                    switch (request.HttpMethod)
                    {
                        case "GET":
                            using (var reader = new StreamReader(request.InputStream))
                            {
                                string requestBody = reader.ReadToEnd();
                                User[] users = new MySQLManager().SelectUsers().ToArray();
                                foreach(var user in users)
                                {
                                    user.Passwordseguro = "";                                    
                                }
                                responseString = JsonConvert.SerializeObject(users, Formatting.Indented); //dar formato al json                                 
                            }
                            break;
                        case "POST":
                            using (var reader = new StreamReader(request.InputStream))
                            {
                                string requestBody = reader.ReadToEnd(); // Lee el cuerpo de la solicitud                                                                     
                                responseString = new MySQLManager().InsertUser(PostJson.GetJsonResult<User>(requestBody));
                            }                            
                            break;
                        case "PUT":
                            // Handle PUT user request
                            responseString = "Handling PUT user request...";
                            break;
                        case "DELETE":
                            // Handle DELETE user request
                            responseString = "Handling DELETE user request...";
                            break;
                        default:
                            // Handle any other HTTP method
                            responseString = "Invalid HTTP method.";
                            break;
                    }
                }
                else if (request.Url.LocalPath == "/api/role")
                {
                    switch (request.HttpMethod)
                    {
                        case "GET":
                            // Handle GET user request
                            responseString = "Handling GET user request...";
                            break;
                        case "POST":
                            using (var reader = new StreamReader(request.InputStream))
                            {
                                string requestBody = reader.ReadToEnd(); // Lee el cuerpo de la solicitud                                                                     
                                responseString = new MySQLManager().InsertRole(PostJson.GetJsonResult<Role>(requestBody));
                            }                            
                            break;
                        case "PUT":
                            // Handle PUT user request
                            responseString = "Handling PUT user request...";
                            break;
                        case "DELETE":
                            // Handle DELETE user request
                            responseString = "Handling DELETE user request...";
                            break;
                        default:
                            // Handle any other HTTP method
                            responseString = "Invalid HTTP method.";
                            break;
                    }
                }
                else if (request.Url.LocalPath == "/api/login")
                {
                    switch (request.HttpMethod)
                    {
                        case "GET":
                            // Handle GET user request
                            responseString = "Handling GET user request...";
                            break;
                        case "POST":                            
                            using (var reader = new StreamReader(request.InputStream))                            
                            {                                
                                string requestBody = reader.ReadToEnd(); // Lee el cuerpo de la solicitud                                                                                                                                                                             
                                

                                MySQLManager sql = new MySQLManager();
                             
                                Authorize perms = PostJson.GetJsonResult<Authorize>(requestBody);
                                                                                            
                                responseString = JsonConvert.SerializeObject(sql.Login(perms), Formatting.Indented);
                                

                            }
                            break;
                        case "PUT":
                            // Handle PUT user request
                            responseString = "Handling PUT user request...";
                            break;
                        case "DELETE":
                            // Handle DELETE user request
                            responseString = "Handling DELETE user request...";
                            break;
                        default:
                            // Handle any other HTTP method
                            responseString = "Invalid HTTP method.";
                            break;
                    }
                }
                else
                {
                    // Handle unknown API request
                    responseString = "Unknown API request...";
                }

                byte[] buffer = Encoding.UTF8.GetBytes(responseString);

                response.ContentType = "application/json";
                response.ContentLength64 = buffer.Length;
                response.StatusCode = 200;
                response.OutputStream.Write(buffer, 0, buffer.Length);

                response.Close();
            }
        }
    }
}
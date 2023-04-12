using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static void Main(string[] args)
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 8080);
        listener.Start();
        Console.WriteLine("Server started. Listening to port 8080.");

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Client connected.");

            StreamReader reader = new StreamReader(client.GetStream());
            string request = reader.ReadLine();
            Console.WriteLine(request);

            string[] tokens = request.Split(' ');
            string path = tokens[1];
            if (path == "/")
            {
                path = "/index.html";
            }

            string extension = Path.GetExtension(path);
            string type = "";

            switch (extension)
            {
                case ".html":
                    type = "text/html";
                    break;
                case ".css":
                    type = "text/css";
                    break;
                case ".js":
                    type = "text/javascript";
                    break;
                case ".png":
                    type = "image/png";
                    break;
                case ".jpg":
                case ".jpeg":
                    type = "image/jpeg";
                    break;
                case ".ogg":
                    type = "audio/ogg";
                    break;
            }

            byte[] content;

            try
            {
                FileStream fileStream = new FileStream("public" + path, FileMode.Open);
                content = new byte[fileStream.Length];
                fileStream.Read(content, 0, (int)fileStream.Length);
                fileStream.Close();
            }
            catch (FileNotFoundException)
            {
                
                content = Encoding.UTF8.GetBytes(File.ReadAllText("public/404.html"));
                type = "text/html";
            }

            StreamWriter writer = new StreamWriter(client.GetStream());
            writer.WriteLine("HTTP/1.1 200 OK");
            writer.WriteLine("Content-Type: " + type);
            writer.WriteLine("Content-Length: " + content.Length);
            writer.WriteLine();
            writer.Flush();

            client.GetStream().Write(content, 0, content.Length);

            client.Close();
            Console.WriteLine("Client disconnected.");
        }
    }
}

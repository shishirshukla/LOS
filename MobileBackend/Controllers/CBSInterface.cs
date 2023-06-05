using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MobileBackend.Controllers
{
    public class CBSInterface : Controller
    {
        public ActionResult Index()
        {

            try
            {
                // Connect to Telnet server
                TcpClient client = new TcpClient("10.43.3.15", 3065);

                // Get the network stream
                NetworkStream stream = client.GetStream();

                // Create a stream reader and writer for the network stream
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);

                // Send a command to the server
                writer.WriteLine(" 0144                    **  0000      003099020029900002060465000000000     0         00000000        000000000000000000000000000000142           41");
                writer.Flush();

                // Read the response from the server
                string response = reader.ReadLine();
                ViewBag.Message = response;

                // Close the connection
                client.Close();
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
            }
            return View();
        }
    }
}

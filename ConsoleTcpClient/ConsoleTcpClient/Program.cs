﻿using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleTcpClient
{
    class Program
    {
        static void Connect(String server, String message)
        {
            try
            {
                Int32 port = 777;
                TcpClient client = new TcpClient(server, port);

                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                NetworkStream stream = client.GetStream();

                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);

                data = new Byte[256];

                String responseData = String.Empty;

                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);

                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

        static void Main(string[] args)
        {
            bool running = true;
            string input;
            while (running)
            {
                Console.WriteLine("Please enter some data:");
                input = Console.ReadLine();
                if (input == "") running = false;
                else Connect("localhost", input);
            }
        }
    }
}

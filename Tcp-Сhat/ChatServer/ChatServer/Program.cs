using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ChatServer
{
    //Асинхронный делегат для остановки сервера
    public delegate void StopDel();

    /// <summary>
    /// Серверная часть чата (консольная)
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //Создание экземпляра сервера и запуск его из потока
            Server server = new Server();
            if (server.isConnected)
            {
                Thread serverThread = new Thread(server.RunServer);
                serverThread.Start();

                string s;

                //Сервер работает до ввода команды shutdown
                while ((s = Console.ReadLine()) != "shutdown")
                {
                }

                //Остановка сервера и закрытие всех клиентских потоков
                StopDel del = new StopDel(server.ServerStop);
                IAsyncResult res = del.BeginInvoke(null, null);
                del.EndInvoke(res);

                //Отображение статуса сервера
                server.DisplayMessage("Stopped");

                //Остановка потока сервера
                serverThread.Abort();
            }
        }
    }
}

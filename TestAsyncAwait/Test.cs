using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace TestAsyncAwait
{
    public static  class Test
    {
        /// <summary>
        /// 1) заходим в AsyncMethod, доходим до (1), возвращает управление сюда,
        /// 2) отображаем сообщение After ..., больше выполнять нечего - передаем управление в Main
        /// 3) завершаем все действия в Main
        /// </summary>
        public static void RunAsyncVersion1()
        {
            Console.WriteLine($"\tRunAsyncVersion1. Before thread Id: {Thread.CurrentThread.ManagedThreadId}");
            AsyncMethod();
            Console.WriteLine($"\tRunAsyncVersion1. After thread Id: {Thread.CurrentThread.ManagedThreadId}");
        }

        /// <summary>
        /// 1) заходим в AsyncMethod, доходим до (1), возвращает управление сюда,
        /// 2) завершаем все действия в Main
        /// 3) завершаем асинхронные задачи webRequest1 и webRequest2
        /// 4) отображаем After AsyncMethod ...
        /// </summary>
        public static async Task RunAsyncVersion2()
        {
            Console.WriteLine($"\tRunAsyncVersion2. Before thread Id: {Thread.CurrentThread.ManagedThreadId}");
            await AsyncMethod();
            Console.WriteLine($"\tRunAsyncVersion2. After thread Id: {Thread.CurrentThread.ManagedThreadId}");
        }

        public static void RunAsyncVersion31()
        {
            Console.WriteLine($"\tRunAsyncVersion3. Before thread Id: {Thread.CurrentThread.ManagedThreadId}");

            var task = AsyncMethod();

            var webRequest3 = WebRequest.Create("http://blogs.msdn.com");
            Console.WriteLine($"\tWebRequest3. Before thread Id: {Thread.CurrentThread.ManagedThreadId}");

            webRequest3.GetResponse();
            Console.WriteLine($"\tWebRequest3. After thread Id: {Thread.CurrentThread.ManagedThreadId}");
            
            //если раскомментить, то ожидаем, что все задачи выполнены, только потом идем в Main
            //task.Wait();

            Console.WriteLine($"\tRunAsyncVersion3. After thread Id: {Thread.CurrentThread.ManagedThreadId}");
        }


        public static void RunAsyncVersion32()
        {
            Console.WriteLine($"\tRunAsyncVersion3. Before thread Id: {Thread.CurrentThread.ManagedThreadId}");

            var task = AsyncMethod();

            var webRequest3 = WebRequest.Create("http://blogs.msdn.com");
            Console.WriteLine($"\tWebRequest3. Before thread Id: {Thread.CurrentThread.ManagedThreadId}");

            webRequest3.GetResponse();
            Console.WriteLine($"\t tWebRequest3. After thread Id: {Thread.CurrentThread.ManagedThreadId}");

            //если закомментить, то передача управления в Main случится раньше, чем выполнятся задачи
            task.Wait();

            Console.WriteLine($"\tRunAsyncVersion3. After thread Id: {Thread.CurrentThread.ManagedThreadId}");
        }

        public static async Task<string> RunAsyncVersion41()
        {
            Console.WriteLine($"\tRunAsyncVersion3. Before thread Id: {Thread.CurrentThread.ManagedThreadId}");

            var task = AsyncMethod();

            var webRequest3 = WebRequest.Create("http://blogs.msdn.com");
            Console.WriteLine($"\tWebRequest3. Before thread Id: {Thread.CurrentThread.ManagedThreadId}");

            webRequest3.GetResponse();
            Console.WriteLine($"\tWebRequest3. After thread Id: {Thread.CurrentThread.ManagedThreadId}");

            var result = await task;
            return $"{result} . Check thread Id: {Thread.CurrentThread.ManagedThreadId}";
        }

        public static Task<string> RunAsyncVersion42()
        {
            Console.WriteLine($"\tRunAsyncVersion3. Before thread Id: {Thread.CurrentThread.ManagedThreadId}");

            var task = AsyncMethod();

            var webRequest3 = WebRequest.Create("http://blogs.msdn.com");
            Console.WriteLine($"\tWebRequest3. Before thread Id: {Thread.CurrentThread.ManagedThreadId}");

            webRequest3.GetResponse();
            Console.WriteLine($"\tWebRequest3. After thread Id: {Thread.CurrentThread.ManagedThreadId}");

            return task;
        }

        public static async Task<string> AsyncMethod()
        {
            var webRequest1 = WebRequest.Create("http://rsdn.ru");
            Console.WriteLine($"\t\tWebRequest1. Before thread Id: {Thread.CurrentThread.ManagedThreadId}");

            // (1)
            await webRequest1.GetResponseAsync().ConfigureAwait(false);
            Console.WriteLine($"\t\tWebRequest1. After thread Id: {Thread.CurrentThread.ManagedThreadId}");

            var webRequest2 = WebRequest.Create("http://gotdotnet.ru");
            Console.WriteLine($"\t\tWebRequest2. Before thread Id: {Thread.CurrentThread.ManagedThreadId}");

            // (2)
            await webRequest2.GetResponseAsync().ConfigureAwait(false);
            Console.WriteLine($"\t\tWebResponse2. Аfter thread Id: {Thread.CurrentThread.ManagedThreadId}");

            return $"Finish AsyncMethod. Thread Id: {Thread.CurrentThread.ManagedThreadId}";
        }
    }
}

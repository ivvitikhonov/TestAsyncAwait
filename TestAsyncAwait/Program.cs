using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestAsyncAwait
{
    class Program
    {
        //для примеров 1-3 заменить "async Task" на void
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine($"Main. Start thread Id: {Thread.CurrentThread.ManagedThreadId}");

                //#1
                //Несколько последовательных длительных асинхронных операций. 
                //Возвращаемое значение не предоставляет интереса.
                //Test.RunAsyncVersion1();

                //#2
                //Несколько последовательных длительных операций. 
                //Возвращаемое значение не предоставляет интереса.
                //НО есть необходимость выполнить некоторые действия после полного завершения асинхронного метода -
                //в данном случае код после await AsyncMethod()
                //Test.RunAsyncVersion2();

                //#3.1
                //К примеру #1 добавляется синхронная операция
                Test.RunAsyncVersion31();

                //#3.2
                //К примеру #2 добавляется синхронная операция
                //Test.RunAsyncVersion32();


                //#4.1. Возвращаемое значение имеет место быть, но в главной программе оно не важно/не используется
                //при вызове аcинхронного метода await не используется
                //var result1 = Test.RunAsyncVersion41();
                //Console.WriteLine($"Test.RunAsyncVersion4 result : {result1}");

                //#4.2. Возвращаемое значение имеет место быть, в главной программе оно важно/используется
                //при вызове аcинхронного метода await используется
                //в вызываемом методе используются дополнительные преобразования
                //var result2 = await Test.RunAsyncVersion41();
                //Console.WriteLine($"Test.RunAsyncVersion4 result : {result2}");

                //#4.3. Метод Test.RunAsyncVersion42 не помечен как асинхронный
                //в вызываемом методе не используются дополнительные преобразования
                //var result3 = await Test.RunAsyncVersion42();
                //Console.WriteLine($"Test.RunAsyncVersion4 result : {result3}");

                Console.WriteLine($"Main. Finish thread Id: {Thread.CurrentThread.ManagedThreadId}");

            }
            catch (System.AggregateException e)
            {
                //Все исключения в TPL пробрасываются обернутые в AggregateException
                Console.WriteLine("AggregateException: {0}", e.InnerException.Message);
            }
            Console.ReadLine();
        }
    }
}

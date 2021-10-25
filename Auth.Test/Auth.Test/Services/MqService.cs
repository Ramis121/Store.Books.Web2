using EasyNetQ;
using Microsoft.Extensions.Hosting;
using Store.Books.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Test.Services
{
    public class MqService : IHostedService
    {
        private readonly IBus _bus;
        
        public MqService(IBus bus)
        {
            _bus = bus;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _bus.PubSub.Subscribe<MqMessage>("mq_message", HandleTextMessage, cancellationToken: cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        void HandleTextMessage(MqMessage textMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Zhangir says: {0}", textMessage.Body);
            Console.ResetColor();
        }
    }
}

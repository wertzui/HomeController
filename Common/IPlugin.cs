using System;
using System.Threading.Tasks;

namespace Common
{
    public interface IPlugin
    {
        Task StartAsync(string hubUri);

        Task StopAsync();
    }
}
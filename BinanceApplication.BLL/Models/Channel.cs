using System.Threading.Channels;

namespace BinanceApplication.BLL.Models
{
    public sealed class Channel<T>
    {
        private readonly System.Threading.Channels.Channel<T> _channel = System.Threading.Channels.Channel.CreateUnbounded<T>();

        public ChannelWriter<T> Writer => _channel.Writer;
        public ChannelReader<T> Reader => _channel.Reader;
    }
}

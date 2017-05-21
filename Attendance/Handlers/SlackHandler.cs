using System.Net.WebSockets;
using System.Threading.Tasks;
using WebSocketManager;

namespace Attendance.Handlers
{
    public class SlackHandler : WebSocketHandler
    {
        public SlackHandler(WebSocketConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {
        }

        public override Task OnConnected(WebSocket socket)
        {
            return base.OnConnected(socket);
        }
    }
}
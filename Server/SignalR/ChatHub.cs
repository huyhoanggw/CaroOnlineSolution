using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.SignalR;
using Server.Entities;
using Server.Enum;
using Server.Services;
using System.Runtime.CompilerServices;

namespace Server.SignalR
{
    public class ChatHub : Hub
    {
        private static int MaxPersions = 2;
        private static List<string> Persions = new List<string>();
        static Status CurrentStatus = Status.X;
        public async Task JoinChat()
        {
            if (Persions.Count < MaxPersions)
            {
                Persions.Add(Context.ConnectionId);
                await Groups.AddToGroupAsync(Context.ConnectionId, "ChatRoom");
                await Clients.Groups("ChatRoom").SendAsync("UserJoined", Context.ConnectionId);
                await Clients.Client(Context.ConnectionId).SendAsync("NotifyStatus", Persions.Count == 1 ? "X" : "O");
            }
            else
                await Clients.Client(Context.ConnectionId).SendAsync("RoomFull", "Phòng đầy");
        }
        public async Task Click(int index, Status status)
        {
            if (Persions.Count < MaxPersions)
            {
                await Clients.All.SendAsync("RoomFull", "Cần đủ 2 người chơi ");
                return;
            }
            var id = Persions.IndexOf(Context.ConnectionId);
            if ((id == 0 && CurrentStatus == Status.X) || (id == 1 && CurrentStatus == Status.O))
            {
                await Clients.All.SendAsync("Click", index, status);
                CurrentStatus = CurrentStatus == Status.X ? Status.O : Status.X;
                await Clients.All.SendAsync("ChangeTurn", CurrentStatus);

            }

        }
        public async Task CheckWin(List<Cell> map, int index)
        {
            var result = new CheckWinService(map).CheckWin(map[index]);
            if (result)
            {
                await Clients.All.SendAsync("CheckWin", CurrentStatus == Status.X ? Status.O : Status.X);
                await Clients.Client(Context.ConnectionId).SendAsync("NotifyStatus", Persions.IndexOf(Context.ConnectionId) == 0 ? "X" : "O");
            }
        }
        public async override Task OnDisconnectedAsync(Exception ex)
        {
            Persions.Remove(Context.ConnectionId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "ChatRoom");
            await Clients.OthersInGroup("ChatRoom").SendAsync("UserLeft", Context.ConnectionId);
            await base.OnDisconnectedAsync(ex);
        }
    }
}

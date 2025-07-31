using Microsoft.AspNetCore.SignalR;
using RockPaperScissorsGame.Models;

namespace BlazorApp2.Hubs
{
    public class GameHub : Hub
    {
        private static readonly Dictionary<string, GameRoom> Rooms = [];

        public async Task JoinRoom(string roomId)
        {
            string opponentId = null;

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

            lock (Rooms)
            {
                if (!Rooms.ContainsKey(roomId))
                {
                    Rooms[roomId] = new GameRoom(roomId);
                    Rooms[roomId].Player1 = Context.ConnectionId;
                }
                else if (string.IsNullOrWhiteSpace(Rooms[roomId].Player2))
                {
                    Rooms[roomId].Player2 = Context.ConnectionId;
                    opponentId = Rooms[roomId].Player1;
                }
            }

            if (!string.IsNullOrWhiteSpace(opponentId))
            {
                await Clients.Groups(roomId).SendAsync("PlayerJoined");
            }
        }

        public async Task MakeMove(string roomId, Move move)
        {
            if (!Rooms.ContainsKey(roomId)) return;

            var room = Rooms[roomId];
            if (Context.ConnectionId == room.Player1)
            {
                room.Move1 = move;
            }

            if (Context.ConnectionId == room.Player2)
            {
                room.Move2 = move;
            }

            if (room.Move1 != Move.None && room.Move2 != Move.None)
            {
                var result = GameLogic.DetermineWinner(room.Move1, room.Move2);
                await Clients.Group(roomId).SendAsync("ShowResult", result);
                room.Reset();
            }
        }

        public Task Reset(string roomId)
        {
            if (Rooms.TryGetValue(roomId, out var room))
            {
                room.Reset();
            }

            return Task.CompletedTask;
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Clean up could be handled here
            await base.OnDisconnectedAsync(exception);
        }
    }
}

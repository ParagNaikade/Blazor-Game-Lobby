using Microsoft.AspNetCore.SignalR;
using RockPaperScissorsGame.Models;

namespace BlazorApp2.Hubs
{
    public class GameHub : Hub
    {
        private static readonly Dictionary<string, GameRoom> Rooms = [];

        public async Task JoinRoom(string roomId, string playerName)
        {
            string opponentId = null;
            bool isRoomFull = false;

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

            lock (Rooms)
            {
                if (!Rooms.ContainsKey(roomId))
                {
                    Rooms[roomId] = new GameRoom(roomId);
                    Rooms[roomId].Player1Id = Context.ConnectionId;
                    Rooms[roomId].Player1Name = playerName;
                }
                else if (string.IsNullOrWhiteSpace(Rooms[roomId].Player2Id))
                {
                    Rooms[roomId].Player2Id = Context.ConnectionId;
                    opponentId = Rooms[roomId].Player1Id;
                    Rooms[roomId].Player2Name = playerName;
                }
                else
                {
                    isRoomFull = true;
                }
            }

            if (isRoomFull)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("RoomFull");
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
            if (Context.ConnectionId == room.Player1Id)
            {
                room.Move1 = move;
            }

            if (Context.ConnectionId == room.Player2Id)
            {
                room.Move2 = move;
            }

            if (room.Move1 != Move.None && room.Move2 != Move.None)
            {
                var result = GameLogic.GetWinnerNumber(room.Move1, room.Move2);
                string? resultMessage;
                if (result == null)
                {
                    resultMessage = "It's a tie";
                }
                else if (result == 1)
                {
                    resultMessage = $"{room.Player1Name} wins!";
                }
                else
                {
                    resultMessage = $"{room.Player2Name} wins!";
                }

                await Clients.Group(roomId).SendAsync("ShowResult", resultMessage);
                room.Reset();
            }
        }

        public async Task Reset(string roomId)
        {
            if (Rooms.TryGetValue(roomId, out var room))
            {
                await Clients.Groups(roomId).SendAsync("ResetGame");
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string roomId = null;
            string opponentId = null;

            lock (Rooms)
            {
                foreach (var kvp in Rooms)
                {
                    var room = kvp.Value;
                    if (room.Player1Id == Context.ConnectionId)
                    {
                        room.Player1Id = null;
                        room.Move1 = Move.None;
                        roomId = kvp.Key;
                        opponentId = room.Player2Id;
                        break;
                    }
                    else if (room.Player2Id == Context.ConnectionId)
                    {
                        room.Player2Id = null;
                        room.Move2 = Move.None;
                        roomId = kvp.Key;
                        opponentId = room.Player1Id;
                        break;
                    }
                }
            }

            // Notify the opponent if they are still connected
            if (!string.IsNullOrWhiteSpace(roomId) && !string.IsNullOrWhiteSpace(opponentId))
            {
                await Clients.Client(opponentId).SendAsync("OpponentLeft");
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}

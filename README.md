# BlazorGameLobby

# RockPaperScissorsGame

A real-time, multiplayer Rock-Paper-Scissors game built with Blazor WebAssembly and SignalR.  
Players can join a game room, select an avatar, and play against each other with live updates.

## Features

- **Blazor WebAssembly** frontend for fast, interactive UI
- **SignalR** for real-time communication between players
- **Bootstrap** for responsive, modern styling
- **Player avatars and names** for personalized gameplay
- **Room system**: create or join rooms by ID
- **Game state management**: handles moves, results, and player disconnects

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)

### Running the App

1. **Restore NuGet packages**
2. **Build the Project**
3. **Run the Server**
4. **Open in browser**
   - Navigate to `https://localhost:5001` (or the port shown in the console)

### Playing the Game

1. Go to `/lobby` to enter your name, choose an avatar, and join a room.
2. Share the room ID with a friend so they can join the same room.
3. Once both players are in, select your move (Rock, Paper, or Scissors).
4. The winner is shown instantly. Play again or join a new room!

## Project Structure

- `RockPaperScissorsGame.Client` — Blazor WebAssembly frontend
- `RockPaperScissorsGame.Server` — ASP.NET Core backend with SignalR hub
- `RockPaperScissorsGame.Shared` — Shared models and enums
- `RockPaperScissorsGame.Hubs.GameHub.cs` — SignalR hub for game logic
- `RockPaperScissorsGame.Client/Pages/Lobby.razor` — Lobby page for joining rooms
- `RockPaperScissorsGame.Client/Pages/GameRoom.razor` — Main game room UI

## Technologies Used

- Blazor WebAssembly (.NET 8)
- ASP.NET Core SignalR
- Bootstrap 5
- C#

## Customization

- **Avatars**: Add more emojis in the `Avatars` array in `Lobby.razor`
- **UI**: Tweak Bootstrap classes for your preferred look
- **Game Logic**: Extend in `GameHub.cs` and shared models

## License

MIT

---

**Enjoy playing Rock-Paper-Scissors with friends in real time!**
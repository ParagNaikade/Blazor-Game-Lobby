namespace GameLobby.Models
{
    public enum Move { None, Rock, Paper, Scissors }

    public class GameRoom(string roomId)
    {
        public string RoomId { get; } = roomId;

        public string Player1 { get; set; } = string.Empty;

        public string Player2 { get; set; } = string.Empty;

        public Move Move1 { get; set; } = Move.None;

        public Move Move2 { get; set; } = Move.None;

        public void Reset()
        {
            Move1 = Move2 = Move.None;
        }
    }

    public static class GameLogic
    {
        public static string DetermineWinner(Move m1, Move m2)
        {
            if (m1 == m2)
            {
                return "It's a tie!";
            }

            if ((m1 == Move.Rock && m2 == Move.Scissors) ||
                (m1 == Move.Paper && m2 == Move.Rock) ||
                (m1 == Move.Scissors && m2 == Move.Paper))
            {
                return "Player 1 wins!";
            }

            return "Player 2 wins!";
        }
    }
}

namespace RockPaperScissorsGame.Models
{
    public enum Move { None, Rock, Paper, Scissors }

    public class GameRoom(string roomId)
    {
        public string RoomId { get; } = roomId;

        public string Player1Id { get; set; } = string.Empty;

        public string Player2Id { get; set; } = string.Empty;

        public string Player1Name { get; set; } = string.Empty;

        public string Player2Name { get; set; } = string.Empty;

        public Move Move1 { get; set; } = Move.None;

        public Move Move2 { get; set; } = Move.None;

        public void Reset()
        {
            Move1 = Move2 = Move.None;
        }
    }

    public static class GameLogic
    {
        public static int? GetWinnerNumber(Move m1, Move m2)
        {
            if (m1 == m2)
            {
                return null;
            }

            if (m1 == Move.Rock && m2 == Move.Scissors ||
                m1 == Move.Paper && m2 == Move.Rock ||
                m1 == Move.Scissors && m2 == Move.Paper)
            {
                return 1;
            }

            return 2;
        }
    }
}

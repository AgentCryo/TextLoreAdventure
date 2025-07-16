using System.Collections.Generic;
using System.Threading.Tasks;
using TextLoreAdventure.Data;

namespace TextLoreAdventure;

public class MainMenu
{   
    public void Start(List<Line> lines)
    {
        lines.Clear();
        lines.Add(new Line("Select Save File:", 60, Color.Green));
        lines.Add(new Line(" Note: no saving exists, all will create a new game.", 25, Color.DarkGray));
        lines.Add(new Line("  (1) New", 30, Color.Green));
        lines.Add(new Line("  (2) New", 30, Color.Green));
        lines.Add(new Line("  (3) New ", 30, Color.Green));
    }

    public async Task<GameState> ExecuteInputAsync(HttpClient http, string input, List<Line> lines, GameState gameState, Game game)
    {
        switch (input)
        {
            case "1":
            case "2":
            case "3":
                gameState = GameState.Game;
                await game.LoadJsonSceneAsync(http, lines, "Test");
                break;
        }
        return gameState;
    }
}
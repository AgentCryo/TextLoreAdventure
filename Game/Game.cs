using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TextLoreAdventure.Data;

namespace TextLoreAdventure;

public class Game
{
    public SceneStory Scene;

    public async Task LoadJsonSceneAsync(HttpClient http, List<Line> lines, string jsonFileName)
    {
        Scene = await ReadJson.SceneStoryAsync(http, jsonFileName);
        lines.Clear();
        lines.AddRange(Scene.Lines);
        foreach (var choice in Scene.Choices)
        {
            lines.Add(choice.Line);
        }
    }

    public async Task<GameState> ExecuteInputAsync(HttpClient http, string input, List<Line> lines, GameState gameState)
    {
        Random random = new();

        if (Scene == null || Scene.Choices == null)
            return gameState;

        for (int i = 1; i <= Scene.Choices.Count; i++)
        {
            if (input == i.ToString())
            {
                var scenes = Scene.Choices[i - 1].Scenes;
                if (scenes.Count == 0) 
                    return gameState;

                int sceneIndex = random.Next(0, scenes.Count);
                await LoadJsonSceneAsync(http, lines, scenes[sceneIndex]);
                return GameState.Game;
            }
        }

        return gameState;
    }
}
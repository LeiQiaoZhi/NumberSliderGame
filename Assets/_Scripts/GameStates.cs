using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Game States", menuName = "Game States", order = 0)]
public class GameStates : ScriptableObject
{
    public enum GameState
    {
        Start,
        Playing,
        Pause,
        Over
    }

    [FormerlySerializedAs("gameState")] public GameState state;

    public void Init()
    {
        state = GameState.Start;
    }
    public bool IsPlaying()
    {
        return state == GameState.Playing;
    }
    
}
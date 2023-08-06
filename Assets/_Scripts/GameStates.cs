using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Game States", menuName = "Game States", order = 0)]
public class GameStates : ScriptableObject
{
    public enum GameState
    {
        Prepare,
        Playing,
        Pause,
        Over
    }

    [FormerlySerializedAs("gameState")] public GameState state;

    public void Init()
    {
        state = GameState.Prepare;
    }
    public bool IsPlaying()
    {
        return state == GameState.Playing;
    }
    
}
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    private enum GameState {
        Starting,
        Playing,
        Paused
    }
    private GameState state;
    private bool hasElectricity = false;
    public bool HasElectricity => hasElectricity;
    public void ElectricityOn() {
        hasElectricity = true;
    }

    private void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void GameStart() {
        state = GameState.Playing;
    }
    public void PauseGame() {
        state = GameState.Paused;
    }
    public bool CanMove() => state == GameState.Playing;
    private void OnDestroy() {
        Instance = null;
    }
}
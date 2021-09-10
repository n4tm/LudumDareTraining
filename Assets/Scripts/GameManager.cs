using UnityEngine;

public class GameManager
{
    private static GameManager instance;
    public static GameManager Instance => instance ??= new GameManager();

    public void Pause(bool paused)
    {
        Time.timeScale = paused ? 0 : 1;
    }
}

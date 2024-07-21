using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject gameWinUI;

    bool isGameOver, isGameWin, isPause;
    private void Awake()
    {
        StartGame();
    }
    private void Update()
    {
        gameOverUI.SetActive(isGameOver);
        gameWinUI.SetActive(isGameWin);
    }
    public void StartGame()
    {
        isGameOver = isPause = isGameWin = false;
    }
    public bool IsGameOver() {  return isGameOver; }
    public bool IsGameWin() { return isGameWin; }
    public bool IsPause() { return isPause; }
    public void SetGameOver(bool gameOver) { isGameOver = gameOver; }
    public void SetGameWin(bool gameWin) { isGameWin = gameWin; }
    public void SetPause(bool pause) { isPause = pause; }
    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject gameWinUI;
    public GameObject pauseUI;

    [Header("UI Buttons")]
    [SerializeField] Button pauseButton;
    [SerializeField] Button retryButton;
    [SerializeField] Button resumeButton;
    [SerializeField] Button closePauseUIButton;

    bool isGameOver, isGameWin, isPause, isStartGame;
    UnityEvent OnGameOver;

    SoundManager soundManager;
    private void Awake()
    {
        StartGame();
    }
    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();

        pauseButton.onClick.AddListener(Pause);
        retryButton.onClick.AddListener(Retry);
        resumeButton.onClick.AddListener(Pause);
        closePauseUIButton.onClick.AddListener(Pause);

        GameOver();
    }
    private void Update()
    {
        gameOverUI.SetActive(isGameOver);
        gameWinUI.SetActive(isGameWin);
        pauseUI.SetActive(isPause && !isGameOver && !isGameWin);
    }
    public void StartGame()
    {
        isGameOver =  isPause = isGameWin = isStartGame = false;
    }
    void Pause()
    {
        if (isPause)
        {
            isPause = false;
            AudioListener.pause = false;
            Time.timeScale = 1f;
        }
        else
        {
            isPause = true;
            AudioListener.pause = true;
            Time.timeScale = 0f;
        }
    }
    void GameOver()
    {
        StartCoroutine(ShowGameOver());
    }
    IEnumerator ShowGameOver()
    {
        // Wait until isGameOver becomes true
        while (!isGameOver)
        {
            yield return null; // Wait for the next frame
        }

        soundManager.PauseSound(soundManager.runningSound, true);
        soundManager.PauseSound(soundManager.runningSound2, true);
        soundManager.PauseSound(soundManager.closeToLeftArmsSound, true);

        // Once isGameOver is true, perform the game over actions
        Debug.Log("Game Over!!");

        var doorShutSound = soundManager.doorShutSound;
        var deepSignSound = soundManager.deepSighSound;

        soundManager.PlaySound(doorShutSound);

        yield return new WaitForSeconds(doorShutSound.clip.length + 1f);

        soundManager.PlaySound(deepSignSound);
    }
    public UnityEvent OnGameOverEvent() {  return OnGameOver; }
    public bool IsGameOver() {  return isGameOver; }
    public bool IsStartGame() {  return isStartGame; }
    public bool IsGameWin() { return isGameWin; }
    public bool IsPause() { return isPause; }
    public void SetGameOver(bool gameOver) { isGameOver = gameOver; }
    public void SetStartGame(bool startGame) { isStartGame = startGame; }
    public void SetGameWin(bool gameWin) { isGameWin = gameWin; }
    public void SetPause(bool pause) { isPause = pause; }
    public void Retry() { SceneManager.LoadScene(1); }
}

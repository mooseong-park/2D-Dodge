using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject spawner;
    public GameObject hudContainer, gameOverPanel;
    public Text timeCounter;
    public bool gamePlaying { get; private set; }

    public int countdownTime;
    public Text countdownDisplay;

    private float startTime, elapsedTime;
    TimeSpan timePlaying;

    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gamePlaying = false;
        timeCounter.text = "Time : 00:00:00";
        StartCoroutine(CountdownToStart());
    }

    private void BeginGame()
    {
        gamePlaying = true;
        startTime = Time.time;
        StartCoroutine(SpawnStart());
    }

    public void Update()
    {
        if (gamePlaying)
        {
            elapsedTime = Time.time - startTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);

            string timePlayingStr = "Time : " + timePlaying.ToString("mm':'ss':'ff");
            timeCounter.text = timePlayingStr;
        }
    }

    public void EndGame()
    {
        gamePlaying = false;
        ShowGameOverScreen();
        Time.timeScale = 0;
        //Invoke("ShowGameOverScreen", 0.25f);
    }

    public void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
        //hudContainer.SetActive(false);

        string timePlayingStr = "Time : " + timePlaying.ToString("mm':'ss':'ff");
        gameOverPanel.transform.Find("FinalTimeText").GetComponent<Text>().text = timePlayingStr;
    }

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }
        countdownDisplay.text = "START!";

        BeginGame();
        
        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);
    }

    IEnumerator SpawnStart()
    {
        yield return new WaitForSeconds(1f);
        spawner.SetActive(true);
    }
}

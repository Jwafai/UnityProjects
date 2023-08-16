using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text lifeText;
    [SerializeField] private int maxLife;
    [SerializeField] private int lifeReGain;
    [SerializeField] Button playBtn;
    private const string LIFE = "Life";
    private const string LIFE_REGAIN = "LifeRegain";
    private int life;

    private ScoresSetter scoresSetter;
    private AndroidNotificationsController androidNotificationsController;

    private void Awake()
    {
        scoresSetter = FindObjectOfType<ScoresSetter>(); // Find ScoresSetter script in the scene
        androidNotificationsController = FindObjectOfType<AndroidNotificationsController>(); // Find AndroidNotificationsController script in the scene
    }

    private void Start()
    {
        onApplicationFocus(true);
    }

    private void onApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
            return;
        CancelInvoke();

        int highScore = PlayerPrefs.GetInt(ScoresSetter.HIGH_SCORE, 0);
        highScoreText.text = "High Score: " + highScore.ToString();
        life = PlayerPrefs.GetInt(LIFE, maxLife);

        if (life == 0)
        {
            string lifeReGainTime = PlayerPrefs.GetString(LIFE_REGAIN, string.Empty);
            if (lifeReGainTime == string.Empty) return;                                     
            DateTime lifeReady = DateTime.Parse(lifeReGainTime);

            if (lifeReady <= DateTime.Now)
            {
                life = maxLife;
                PlayerPrefs.SetInt(LIFE, life);
            }
            else
            {
                playButton.interactable = true;
                Invoke(nameof(lifeReady), (lifeReady - DateTime.Now).Seconds);
            }
        }
        lifeText.text = "Play: (" + life.ToString() + ")";
          
        
        
    }

    private void lifeReady()
    {
        playButton.interactable = true;
        life = maxLife;
        PlayerPrefs.SetInt(LIFE, life);
        lifeText.text = "Play: (" + life.ToString() + ")";
        if (life == maxLife)
    {
        Debug.Log("Reset");
        PlayerPrefs.SetInt(ScoresSetter.HIGH_SCORE, 0);
    }
    }

    public void PlayGame()
{
    if (life < 1) return;
    life--;
    PlayerPrefs.SetInt(LIFE, life);

    if (life == 0)
    {   
        DateTime lifeReady = DateTime.Now.AddMinutes(lifeReGain);
        PlayerPrefs.SetString(LIFE_REGAIN, lifeReady.ToString());
        
        #if UNITY_ANDROID
        androidNotificationsController.ScheduleNotification(lifeReady);
        #endif
        //
    }
    

    UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    
}

    

}

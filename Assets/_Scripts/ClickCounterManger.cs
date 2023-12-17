using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCounterManger : MonoBehaviour
{

    private static ClickCounterManger _instance;
    public static ClickCounterManger Instance { get { return _instance; } }

    private int clickCount = 0;
    private int targetNumber; // The randomly generated target number
    private float timer = 0f;
    private float adShowInterval = 30f; // Show ad after 30 seconds
    private bool isTimerEnded = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            GenerateTargetNumber();
        }
    }
    private void Update()
    {
        if (!isTimerEnded)
        {
            timer += Time.deltaTime;
            //Debug.Log("Timer :"+ timer);
            if (timer >= adShowInterval)
            {
                isTimerEnded = true;
                Debug.Log("Timer Ended!");
            }
        }
    }
    public void GenerateTargetNumber()
    {
        // Generate a random number between 2 and 5 (inclusive) as the target number
        Debug.Log("Target Number: " + targetNumber);
         targetNumber = Random.Range(4, 8);
       
    }
    public int GetClickCount()
    {
        return clickCount;
    }
    

    public void IncrementClickCount()
    {
        if(clickCount == targetNumber)
        {
            CheckClickCount();
        }
        else
        {
            clickCount++;
            Debug.Log("clickclickclikc!!!!!!!!!!!!!" + clickCount);
        }
       
        
        

        // Optionally, you can put logic here to check if the clickCount reaches a specific value and show an ad.
    }
    private void CheckClickCount()
    {
        if (isTimerEnded)
        {
            ApplovinMaxManager.Instance.ShowInterstitialAd();
            ResetClickCountAndTimer();
            Debug.Log("Click Count Matches Target Number!");
        }
        else
        {
            Debug.Log("Conditions not met: Timer not ended or target not reached!");
        }
        
    }
    private void ResetClickCountAndTimer()
    {
        clickCount = 0;
        timer = 0f;
        isTimerEnded = false;
        GenerateTargetNumber();
        Debug.Log("ResetClickCountAndTimer");
    }

}

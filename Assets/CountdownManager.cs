using UnityEngine;

[CreateAssetMenu(fileName = "CountdownManager", menuName = "ScriptableObjects/CountdownManager")]
public class CountdownManager : ScriptableObject
{
    public float countdownTime = 30f;
    public float remainingTime;
    public bool timerRunning;
}
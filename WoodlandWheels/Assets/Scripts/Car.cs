using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement ;

public class Car : MonoBehaviour
{
    [SerializeField] private float horizantalInput;
   [SerializeField] private float initialSpeed = 30f;
[SerializeField] private float speedIncrease = 0.1f;
[SerializeField] private float turnSpeedVal = 200f;
[SerializeField] private float speedIncreaseDuration = 5f;
[SerializeField] ScoresSetter scoreSetter;

private float currentSpeed;
private bool isSpeedIncreased;
private float speedIncreaseTimer;

void Start()
{
    currentSpeed = initialSpeed;
}

void Update()
{
    if (isSpeedIncreased)
    {
        if (speedIncreaseTimer <= speedIncreaseDuration)
        {
            speedIncreaseTimer += Time.deltaTime;
            currentSpeed = 40f;
            Debug.Log("Speed Increased"+currentSpeed);
        }
        else
        {
            isSpeedIncreased = false;
            currentSpeed = initialSpeed;
        }
    }

    currentSpeed += speedIncrease * Time.deltaTime;

    transform.Rotate(0f, horizantalInput * turnSpeedVal * Time.deltaTime, 0f);
    transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
}

private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Obstacle"))
    {
        SceneManager.LoadScene(0);
    }
    else if (other.CompareTag("IncreaseScore"))
    {
        Destroy(other.gameObject);
        scoreSetter.ActivatePowerUp();
    }
    else if (other.CompareTag("IncreaseSpeed"))
    {
        Destroy(other.gameObject);
        isSpeedIncreased = true;
        speedIncreaseTimer = 0f;
    }
}

    public void Turn( int value )
    {
        horizantalInput = value ;
    }
    
}

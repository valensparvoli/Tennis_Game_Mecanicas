using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    Vector3 initialPos;
    public string hitter;

    int playerScore;
    int botScore;

    public bool playing = true;

    [SerializeField] Text playerScoreUI;
    [SerializeField] Text botScoreUI;

    private void Start()
    {
        initialPos = transform.position;
        playerScore = 0;
        botScore = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            //transform.position = initialPos;

            GameObject.Find("Player").GetComponent<Player>().Reset();

            if (playing)
            {
                if (hitter == "Player")
                {
                    playerScore++;
                }
                else if (hitter == "Bot")
                {
                    botScore++;
                }
                playing = false; 
            }
        }

        else if (collision.transform.CompareTag("Out"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = initialPos;

            GameObject.Find("Player").GetComponent<Player>().Reset();

            if (playing)
            {
                if (hitter == "Player")
                {
                    playerScore++;
                }
                else if (hitter == "Bot")
                {
                    botScore++;
                }
                playing = false;
                updateScore();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Out")&&playing)
        {
            if (hitter == "Player")
            {
                playerScore++;
            }else if (hitter == "Bot")
            {
                botScore++;
            }
            playing = false;
            updateScore();
        }
    }

    void updateScore()
    {
        playerScoreUI.text = "Player: " + playerScore;
        botScoreUI.text = "Bot: " + botScore;
    }
}

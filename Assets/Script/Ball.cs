using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Ball : MonoBehaviour
{
    //Variables para el reinicio de la pelota
    Vector3 initialPos;
    public string hitter;

    //Variables para la puntuacion
    public int playerScore;
    public int botScore;
    [SerializeField] Text playerScoreUI;
    [SerializeField] Text botScoreUI;

    //Variable que me permite saber si la pelota esta o no en juego
    public bool playing = true;

    private void Start()
    {
        //Reinicio la posicion e igualo todos los puntajes a cero para poder hacer un UI
        initialPos = transform.position;
        playerScore = 0;
        botScore = 0;
        //StartCoroutine(Winner());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            //Baja la velocidad de la pelota a 0 al entrar en contacto con alguna pared
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            //transform.position = initialPos;

            //Llama la funcion de reset para poder ejecutar el saque
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
            //Bajamos la velocidad de la pelota a 0
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            //Reiniciamos la posicion
            transform.position = initialPos;

            //Preparamos al jugador para que ejecute el saque
            GameObject.Find("Player").GetComponent<Player>().Reset();
            
            //Registramos quien fue el ultimo en pegarle para poder realizar la suma de puntaje
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
        //Revisamos quien fue el ultimo en pegarle y si la pelota esta o no en juego para realizar el puntaje
        if (other.CompareTag("Out") && playing)
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

    void Update()
    {
        if (playerScore == 4)
        {
            Debug.Log("PlayerWin");
        }
        else if (botScore == 4)
        {
            Debug.Log("BotWin");
        }
    }

    /*
    IEnumerator Winner()
    {
        if (playerScore == 4)
        {
            Debug.Log("PlayerWin");
        }
        else if (botScore == 4)
        {
            Debug.Log("BotWin");
        }
        yield return new WaitForSeconds(5F);
        Winner();
    }
    */
}

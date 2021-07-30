using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void VolverMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void VolverJugar()
    {
        SceneManager.LoadScene("PlayerVSIA");
    }

    public void PlayerVSIA()
    {
        SceneManager.LoadScene("PlayerVSIA");
    }

    public void PlayerVSPlayer()
    {
        SceneManager.LoadScene("PlayerVSPlayer");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndGame : MonoBehaviour
{
    private AudioSource startMenu;
    public void Quit()
    {
        startMenu.Play();
        Application.Quit();
    }
}

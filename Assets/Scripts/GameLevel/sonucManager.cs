using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sonucManager : MonoBehaviour
{
    public void YenidenYukle()
    {
        SceneManager.LoadScene("Game");
    }
    public void AnaMenuyeDon()
    {
        SceneManager.LoadScene("Menu");
    }
}

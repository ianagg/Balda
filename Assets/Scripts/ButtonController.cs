using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void OnClickExit() {
        Application.Quit();

    }

    public void OnClickPlay() {
        SceneManager.LoadScene("game");
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class control_menu : MonoBehaviour
{
   public void join()
    {
        SceneManager.LoadScene("Startmenu");
    }
    public void join2()
    {
        SceneManager.LoadScene("Scene1");
    }
    public void join3()
    {
        SceneManager.LoadScene("Scene2");
    }
    public void join4()
    {
        SceneManager.LoadScene("Scene3");
    }

    public void back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void quit()
    {
        Application.Quit();
    }

    public void highscore()
    {
        SceneManager.LoadScene("highscoremenu");
    }
   
}

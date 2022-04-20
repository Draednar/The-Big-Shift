using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuPause : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;

    public Button resume;

    [SerializeReference] InputMgr input;

    private void OnEnable()
    {
        InputMgr.menu += Menu;
    }

    private void OnDisable()
    {
        InputMgr.menu -= Menu;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (GameIsPaused)
        //    {
        //        Resume();
        //    }
        //    else
        //    {
        //        Pause();
        //    }
        //}   
    }

    public void Menu(bool value)
    {
        if (value)
        {
            Pause();
            return;
        }

        Resume();
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        input.menuOpen = false;
    }


    void Pause()
    {
        PauseMenuUI.SetActive(true);
        resume.Select();
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Hub(int index)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(index);

    }

    public void Menu(int index)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(index);

    }



}

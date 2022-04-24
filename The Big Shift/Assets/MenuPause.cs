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

    public Image BlackStripe_1, BlackStripe_2;

    public Camera Cam;

    public Button resume;

    [SerializeReference] InputMgr input;

    private void OnEnable()
    {
        InputMgr.menu += Menu;

        Cam.farClipPlane = 30;
        Cam.orthographicSize = PlayerPrefs.GetFloat("Size");

        if (PlayerPrefs.GetInt("Res") == 1)
        {
            BlackStripe_1.gameObject.SetActive(true);
            BlackStripe_2.gameObject.SetActive(true);
        }

        else if (PlayerPrefs.GetInt("Res") == 0)
        {
            BlackStripe_1.gameObject.SetActive(false);
            BlackStripe_2.gameObject.SetActive(false);
        }


        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            if (PlayerPrefs.GetFloat("Size") == 17f)
            {
                Cam.orthographicSize = 18.8f;
            }

            else if (PlayerPrefs.GetFloat("Size") == 12.9f)
            {
                Cam.orthographicSize = 14f;
            }

            else
            {
                Cam.orthographicSize = 15f;

                BlackStripe_1.gameObject.SetActive(true);
                BlackStripe_2.gameObject.SetActive(true);


                BlackStripe_1.rectTransform.sizeDelta = new Vector2(0, 66.5f);
                BlackStripe_2.rectTransform.sizeDelta = new Vector2(0, 66.5f);
            }
        }


        if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            if (PlayerPrefs.GetFloat("Size") == 17f)
            {
                Cam.orthographicSize = 9.34f;
            }

            else if (PlayerPrefs.GetFloat("Size") == 12.9f)
            {
                Cam.orthographicSize = 7f;
            }

            else
            {
                Cam.orthographicSize = 7.42f;

                BlackStripe_1.gameObject.SetActive(true);
                BlackStripe_2.gameObject.SetActive(true);


                BlackStripe_1.rectTransform.sizeDelta = new Vector2(0, 53.54f);
                BlackStripe_2.rectTransform.sizeDelta = new Vector2(0, 62.40f);
            }
        }



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

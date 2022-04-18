using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public enum MenuType {MainMennu, InGame}

    [SerializeField] GameObject pauseMenu, settingMenu;
    public MenuType type;

    private void OnEnable()
    {
        InputMgr.menu += Menu;
    }

    private void OnDisable()
    {
        InputMgr.menu -= Menu;   
    }

    public void Menu(bool value)
    {
        if (value && type == MenuType.InGame)
        {
            pauseMenu.SetActive(true);
            settingMenu.SetActive(false);
            return;
        }

        else if (value && type == MenuType.MainMennu)
        {
            settingMenu.SetActive(true);
            return;
        }

        pauseMenu.SetActive(false);
        settingMenu.SetActive(false);
    }
}

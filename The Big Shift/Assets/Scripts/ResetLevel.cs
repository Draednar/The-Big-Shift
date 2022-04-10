using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene(0);
        }   
    }

    public static void ResetLevelS()
    {
        Scene s = SceneManager.GetActiveScene();

        SceneManager.LoadScene(s.name);
        
    }
}

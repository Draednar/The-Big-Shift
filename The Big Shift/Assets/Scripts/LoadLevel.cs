using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLevel : MonoBehaviour
{
    public int LevelIndex;
    [SerializeField] ResetLevel load;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            load.ChangeNextLevel(LevelIndex);
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelYukle : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
    }
}

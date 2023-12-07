using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadSceneAsync(1);
        
    }
    
    public void SettingsButton()
    {
        // SceneManager.LoadSceneAsync(1);
    }


}

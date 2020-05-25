using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    //1.开始场景
    //2.角色选择界面
    //3.实际游戏界面
    
   
   public void OnNewGame()
    {
        //加载场景2
        PlayerPrefs.SetInt("DataFromSave", 0);
        NextScene();
    }
    public void OnLaodGame()
    {
        //加载场景3
        PlayerPrefs.SetInt("DataFromSave",1);
    }

    public void PlayButtonClip(int i)
    {
        AudioManager.Instance.PlayButtonClip(i);
    }
    public void NextScene()
    {
        Invoke("LoadNextScene", 1f);
    }
   private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

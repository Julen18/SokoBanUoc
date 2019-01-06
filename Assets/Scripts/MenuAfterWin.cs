using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAfterWin : MonoBehaviour
{
    private GameObject GoToLoadLv;
    private Level level;
    // Start is called before the first frame update
    void Start()
    {
        GoToLoadLv = GameObject.Find("MapTpPlay");
        level = GoToLoadLv.GetComponent<MapToPlay>().GetMapSelected();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void ReloadLv()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void NetxLevel()
    {
        int lv = GoToLoadLv.GetComponent<MapToPlay>().historyLevelsCount++;//sum 1 lv
        GoToLoadLv.GetComponent<MapToPlay>().NextLv();
        GoToLoadLv.GetComponent<MapToPlay>().IsHistory(true);
        SceneManager.LoadScene("GameScene");
    }
}

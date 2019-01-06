using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapToPlay : MonoBehaviour
{
    private Object mapSelected;
    private Object[] mapSelectedHistory;
    public int historyLevelsCount;
    private bool historyModeOn =false;
    private static Level lvlSelected;
    public int a;
    void Awake()
    {
        DontDestroyOnLoad(this);

    }

    public void ResetMapToPlay()
    {
        mapSelected = new Object();
    }
    public void AssignMapToPlay(Object map)
    {
        mapSelected = map;
    }
    public void AssignHistory(Object[] maps)
    {
        historyLevelsCount = 0;
        mapSelectedHistory = maps;
        
        mapSelected = mapSelectedHistory[0];
        lvlSelected = new Level();
        lvlSelected = JsonUtility.FromJson<Level>(mapSelectedHistory[0].ToString());
    }
    public void NextLv()
    {
            mapSelected = mapSelectedHistory[historyLevelsCount];
            lvlSelected = JsonUtility.FromJson<Level>(mapSelectedHistory[historyLevelsCount].ToString());
        
    }

    public Level GetMapSelected()
    {
        lvlSelected = new Level();
        lvlSelected = JsonUtility.FromJson<Level>(mapSelected.ToString());
        return lvlSelected;
    }

    public void LvlReset()
    {
        lvlSelected = new Level();
    }
    public void IsHistory(bool val)
    {
        historyModeOn = val;
    }
}

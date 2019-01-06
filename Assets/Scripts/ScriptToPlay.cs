using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptToPlay : MonoBehaviour
{
    public GameObject managerScene;
    public GameObject LvlSaved;
    public GameObject win;

    private int[] lastPosPlayer = new int[] {-1,-1};

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!win.activeSelf)
        {
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {//1
                managerScene.GetComponent<EditLevelManager>().MovePJ(1);
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {//2
                managerScene.GetComponent<EditLevelManager>().MovePJ(2);
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {//3
                managerScene.GetComponent<EditLevelManager>().MovePJ(3);
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {//4
                managerScene.GetComponent<EditLevelManager>().MovePJ(4);
            }

        }
       
    }




}

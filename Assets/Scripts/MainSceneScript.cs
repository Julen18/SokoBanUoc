using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneScript : MonoBehaviour
{
    private int totallyLevelsCreated;
    private Object[] levelsObjs;

    public GameObject MainMenu;
    public GameObject MainLoadLevel;

    public GameObject buttonLvPrefab;
    public Transform fpos;

    private int myLv;//only used on buttons to load
    private Object objToLoad;//only used on buttons to load
    public void MainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadLevel()
    {
        levelsObjs = LevelsCount();
        CreateButtons(levelsObjs);
        //Saber cuantos niveles tenemos para cargar
        //creación de botones/lista de los mismos con un onclick asociado
        //al clicar lanzar la escena pasando el parametro que pertoque.
        //SceneManager.LoadScene("OtherSceneName", LoadSceneMode.Additive);
    }
    public void LoadLevelsHistory()
    {
        Object[] lvls = Resources.LoadAll("LevelsHistory");
        GameObject mapita = GameObject.Find("MapTpPlay");
        objToLoad = lvls[0];//first level
        mapita.GetComponent<MapToPlay>().AssignHistory(lvls);
        mapita.GetComponent<MapToPlay>().IsHistory(true);
        SceneManager.LoadScene("GameScene");
    }

    public void CreateLevels()
    {
        SceneManager.LoadScene("CreateLevelScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private Object[] LevelsCount()
    {
        MainLoadLevel.SetActive(!MainLoadLevel.activeSelf);
        MainMenu.SetActive(!MainLoadLevel.activeSelf);

        Object[] lvls = Resources.LoadAll("LevelsPlayer");
        return lvls;
    }
    private void CreateButtons(Object[] lvls)
    {
        int x;
        for (x = 0; x < lvls.Length; x++)
        {

            GameObject btns = Instantiate(buttonLvPrefab, new Vector3(fpos.GetComponent<Transform>().position.x,
                                fpos.GetComponent<Transform>().position.y - (x*55),
                                fpos.GetComponent<Transform>().position.z),transform.rotation) as GameObject;
             btns.transform.SetParent(fpos,false);
            btns.SetActive(!btns.activeSelf);
            Button butt = btns.GetComponent<Button>();
            btns.GetComponent<MainSceneScript>().SetMyLv(x,lvls[x]);
            btns.GetComponentInChildren<Text>().text = lvls[x].name;

        }
    }

    public void GoToLevel()
    {
        GameObject mapita = GameObject.Find("MapTpPlay");
        mapita.GetComponent<MapToPlay>().AssignMapToPlay(objToLoad); 
        SceneManager.LoadScene("GameScene");
    }
    private void SetMyLv(int num,Object obj)
    {
        myLv = num;
        objToLoad = obj;
    }
}

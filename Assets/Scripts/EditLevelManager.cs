using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Collections;
using UnityEngine.SceneManagement;

public class EditLevelManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public Transform firstPos;
    public InputField inputField;
    public Text fileName;

    public Image btnClear;
    public Image btnWall;
    public Image btnPlayer;
    public Image btnBoxRed;
    public Image btnPointRed;
    public Image btnBoxYellow;
    public Image btnPointYellow;
    public Image btnBoxBlue;
    public Image btnPointBlue;
    public Image btnBoxPurple;
    public Image btnPointPurple;

    public Sprite clear;
    public Sprite wall;
    public Sprite player;
    public Sprite boxRed;
    public Sprite pointRed;
    public Sprite boxYellow;
    public Sprite pointYellow;
    public Sprite boxBlue;
    public Sprite pointBlue;
    public Sprite boxPurple;
    public Sprite pointPurple;

    private int horizontal = 16;
    private int vertical = 12;
    private int[] lastPosPlayer = new int[] { -1, -1 };
    private int selectedSprite = 9;
    private GameObject[,] tiles = new GameObject[16, 12];
    private int[] saveTiles = new int[16 * 12];

    private Color noColor = new Color(0f, 0f, 0f, 0f);
    private Color red = new Color(1f, 0, 0f, 1f);

    private GameObject GoToLoadLv;
    private Level level;
    private HashSet<AuxButtons> abuttons = new HashSet<AuxButtons>();
    private List<AuxButtons> positionButtons = new List<AuxButtons>();

    public GameObject win;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {//4
            Win();//hack
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "CreateLevelScene")
        {
            PaintNewLv();//new scene
        }
        else if (SceneManager.GetActiveScene().name == "GameScene")
        {
            GoToLoadLv = GameObject.Find("MapTpPlay");
            level = GoToLoadLv.GetComponent<MapToPlay>().GetMapSelected();
            lastPosPlayer = level.lastPosPlayer;
            PaintLoadedLevel(GoToLoadLv.GetComponent<MapToPlay>().GetMapSelected());
        }


    }

    public void SelectSprite(int num)
    {
        switch (selectedSprite)
        {
            case 1:
                btnWall.color = noColor;
                break;
            case 2:
                btnBoxPurple.color = noColor;
                break;
            case 3:
                btnPlayer.color = noColor;
                break;
            case 4:
                btnPointBlue.color = noColor;
                break;
            case 5:
                btnPointYellow.color = noColor;
                break;
            case 6:
                btnPointRed.color = noColor;
                break;
            case 7:
                btnPointPurple.color = noColor;
                break;
            case 8:
                btnBoxBlue.color = noColor;
                break;
            case 9:
                btnBoxYellow.color = noColor;
                break;
            case 10:
                btnBoxRed.color = noColor;
                break;
            case -1:
                btnClear.color = noColor;
                break;
        }
        switch (num)
        {
            case 1:
                btnWall.color = red;
                break;
            case 2:
                btnBoxPurple.color = red;
                break;
            case 3:
                btnPlayer.color = red;
                break;
            case 4:
                btnPointBlue.color = red;
                break;
            case 5:
                btnPointYellow.color = red;
                break;
            case 6:
                btnPointRed.color = red;
                break;
            case 7:
                btnPointPurple.color = red;
                break;
            case 8:
                btnBoxBlue.color = red;
                break;
            case 9:
                btnBoxYellow.color = red;
                break;
            case 10:
                btnBoxRed.color = red;
                break;
            case -1:
                btnClear.color = red;
                break;
        }
        selectedSprite = num;
    }

    public void PaintTile(string xy)
    {
        int x = int.Parse(xy.Split(',')[0]);
        int y = int.Parse(xy.Split(',')[1]);

        Image img = tiles[x, y].GetComponent<Image>();
        switch (selectedSprite)
        {
            case 1:
                img.sprite = wall;
                break;
            case 2:
                img.sprite = boxPurple;
                break;
            case 3:
                CheckPlayer(x, y, img);
                break;
            case 4:
                img.sprite = pointBlue;
                break;
            case 5:
                img.sprite = pointYellow;
                break;
            case 6:
                img.sprite = pointRed;
                break;
            case 7:
                img.sprite = pointPurple;
                break;
            case 8:
                img.sprite = boxBlue;
                break;
            case 9:
                img.sprite = boxYellow;
                break;
            case 10:
                img.sprite = boxRed;
                break;
            case -1:
                img.sprite = clear;
                break;
        }
    }
    private void CheckPlayer(int x, int y, Image img)
    {
        if (lastPosPlayer[0] != -1)
        {
            Image lastImgPlayer = tiles[lastPosPlayer[0], lastPosPlayer[1]].GetComponent<Image>();
            lastImgPlayer.sprite = clear;
        }
        lastPosPlayer[1] = y;
        lastPosPlayer[0] = x;
        img.sprite = player;
    }

    public void SaveLevel()
    {
        if (fileName.text == "")
        {
            StartCoroutine(ErrorNeedName());
        } else
        {
            int j = 0;
            for (int x = 0; x < horizontal; x++)
            {
                for (int i = 0; i < vertical; i++)
                {
                    saveTiles[j] = GetTileImage(tiles[x, i].GetComponent<Image>().sprite.name);
                    j++;
                }
            }

            Level lvl = new Level();
            lvl.tiles = saveTiles;
            lvl.lastPosPlayer = lastPosPlayer;
            lvl.mapName = fileName.text;

            string json = JsonUtility.ToJson(lvl);

            string path = "";
            path = "Assets/Resources/LevelsPlayer/" + fileName.text + ".json";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            FileStream stream = new FileStream(path, FileMode.Create);
            stream.Close();
            StreamWriter sw = File.CreateText(path);
            sw.Close();
            File.WriteAllText(path, json);
        }
    }

    private IEnumerator ErrorNeedName()
    {
        Color oldColor = inputField.GetComponent<Image>().color;
        inputField.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(1.0f);
        inputField.GetComponent<Image>().color = oldColor;
    }

    private void PaintNewLv()
    {
        int x, y;
        for (x = 0; x < horizontal; x++)
        {
            for (y = 0; y < vertical; y++)
            {
                tiles[x, y] = Instantiate(tilePrefab, new Vector3(firstPos.position.x + (x * 88), firstPos.position.y - (y * 88), firstPos.position.z), transform.rotation) as GameObject;
                tiles[x, y].transform.SetParent(firstPos);

                if (x == 0 || y == 0 || x == horizontal - 1 || y == vertical - 1)
                {
                    Image img = tiles[x, y].GetComponent<Image>();
                    img.sprite = wall;
                }
                else
                {
                    Button butt = tiles[x, y].GetComponent<Button>();
                    string num = x.ToString() + "," + y.ToString();
                    butt.onClick.AddListener(() => { this.PaintTile(num); });
                }
            }
        }
    }


    public void PaintLoadedLevel(Level lv)
    {
        int x, y, j = 0;
        for (x = 0; x < horizontal; x++)
        {
            for (y = 0; y < vertical; y++)
            {
                tiles[x, y] = Instantiate(tilePrefab, new Vector3(firstPos.position.x + (x * 88), firstPos.position.y - (y * 88), firstPos.position.z), transform.rotation) as GameObject;
                tiles[x, y].transform.SetParent(firstPos);
                Image img = tiles[x, y].GetComponent<Image>();
                if (x == 0 || y == 0 || x == horizontal - 1 || y == vertical - 1)
                {
                    img.sprite = wall;
                }
                else
                {
                    PaintTilesLoadedLv(lv.tiles[j], img,x,y);
                }
                j++;
            }
        }
    }



    public void PaintTilesLoadedLv(int num, Image img,int x, int y)
    {
        switch (num)
        {
            case 0:
                img.sprite = player;
                break;
            case 1:
                img.sprite = wall;
                break;
            case 2:
                img.sprite = boxPurple;
                break;
            case 4:
                img.sprite = pointBlue;
                SavePoints(x, y, img);
                break;
            case 5:
                img.sprite = pointYellow;
                SavePoints(x, y, img);
                break;
            case 6:
                img.sprite = pointRed;
                SavePoints(x, y, img);
                break;
            case 7:
                img.sprite = pointPurple;
                SavePoints(x, y, img);
                break;
            case 8:
                img.sprite = boxBlue;
                break;
            case 9:
                img.sprite = boxYellow;
                break;
            case 10:
                img.sprite = boxRed;
                break;
            case -1:
                img.sprite = clear;
                break;
        }
    }
    public int GetTileImage(string SokoName)
    {
        switch (SokoName)
        {
            case "Preview_Sokoban_15"://wall
                return 1;
            case "UISprite"://blanco
                return -1;
            case "Preview_Sokoban_0"://ninot
                return 0;
            case "Preview_Sokoban_4"://Yellow box
                return 9;
            case "Preview_Sokoban_12":// p yellow
                return 5;
            case "Preview_Sokoban_2":// red box
                return 10;
            case "Preview_Sokoban_13"://p red
                return 6;
            case "Preview_Sokoban_8"://purple box
                return 2;
            case "Preview_Sokoban_10"://p purple
                return 7;
            case "Preview_Sokoban_6":// blue box
                return 8;
            case "Preview_Sokoban_11"://p blue
                return 4;
        }
        return 0;
    }



    public void MovePJ(int direction)
    {
        switch (direction)
        {
            case 1://parriba
                if (CheckTileDirection(lastPosPlayer[0], lastPosPlayer[1] - 1, 1))
                {
                    selectedSprite = 3;
                    PaintTile(lastPosPlayer[0] + "," + (lastPosPlayer[1] - 1).ToString());
                }

                break;
            case 2://pabajo
                if (CheckTileDirection(lastPosPlayer[0], lastPosPlayer[1] + 1, 2))
                {
                    selectedSprite = 3;
                    PaintTile(lastPosPlayer[0] + "," + (lastPosPlayer[1] + 1).ToString());
                }
                break;
            case 3://izk
                if (CheckTileDirection(lastPosPlayer[0] - 1, lastPosPlayer[1], 3))
                {
                    selectedSprite = 3;
                    PaintTile(lastPosPlayer[0] - 1 + "," + (lastPosPlayer[1]).ToString());
                }
                break;
            case 4://dcha
                if (CheckTileDirection(lastPosPlayer[0] + 1, lastPosPlayer[1], 4))
                {
                    selectedSprite = 3;
                    PaintTile(lastPosPlayer[0] + 1 + "," + (lastPosPlayer[1]).ToString());
                }
                break;
        }
        CheckIfBoxOnButton();

    }
    private bool CheckTileDirection(int x, int y, int direction)
    {

        Image img = tiles[x, y].GetComponent<Image>();

        bool ret = false;
        switch (img.sprite.name)
        {
            case "Preview_Sokoban_15"://wall
                return false;
            case "Preview_Sokoban_10"://p purple
            case "Preview_Sokoban_12":// p yellow
            case "Preview_Sokoban_13"://p red
            case "Preview_Sokoban_11"://p blue
                AuxButtons abuttonsobj = new AuxButtons();//save in hashset the old tile 
                abuttonsobj.x = x;
                abuttonsobj.y = y;
                abuttonsobj.name_tile = img.sprite.name;
                return true;
            case "UISprite"://blanco
                return true;
            case "Preview_Sokoban_4"://Yellow box
            case "Preview_Sokoban_2":// red box
            case "Preview_Sokoban_8"://purple box
            case "Preview_Sokoban_6":// blue box
                
                return CheckSecondTile(x, y, direction, img.sprite.name);
                break;
        }


        return ret;
    }


    private bool CheckSecondTile(int x, int y, int direction, string tileName)
    {
        Image img2; // next tile of image according direction
        switch (direction)
        {
            case 1:
                y = y - 1;
                break;
            case 2:
                y = y + 1;
                break;
            case 3:
                x = x - 1;
                break;
            case 4:
                x = x + 1;
                break;
            default:
                img2 = tiles[x, y].GetComponent<Image>();
                break;
        }
        img2 = tiles[x, y].GetComponent<Image>();
        switch (img2.sprite.name)
        {
            case "UISprite"://blanco
                selectedSprite = GetTileImage(tileName);
                PaintTile(x + "," + y);
                return true;
            case "Preview_Sokoban_10"://p purple
            case "Preview_Sokoban_12":// p yellow
            case "Preview_Sokoban_13"://p red
            case "Preview_Sokoban_11"://p blue
                selectedSprite = GetTileImage(tileName);
                PaintTile(x + "," + y);

                return true;

                break;
            case "Preview_Sokoban_15"://wall
            case "Preview_Sokoban_4"://Yellow box
            case "Preview_Sokoban_2":// red box
            case "Preview_Sokoban_8"://purple box
            case "Preview_Sokoban_6":// blue box
                return false;
        }
        return true;
    }

    private void SavePoints(int x, int y, Image img)
    {
        AuxButtons abuttonsobj = new AuxButtons();//save in hashset the old tile 
        abuttonsobj.x = x;
        abuttonsobj.y = y;
        abuttonsobj.name_tile = img.sprite.name;
        positionButtons.Add(abuttonsobj);
    }

    private void CheckIfBoxOnButton()
    {
        int cont = 0;
        for(int i = 0; i < positionButtons.Count; i++)
        {
            AuxButtons abuttonsobj = new AuxButtons();//save in hashset the old tile 
            if (tiles[positionButtons[i].x, positionButtons[i].y].GetComponent<Image>().sprite.name == "UISprite")
            {
                selectedSprite = GetTileImage(positionButtons[i].name_tile);
                PaintTile(positionButtons[i].x + "," + positionButtons[i].y);
            }
            else
            {
                switch (positionButtons[i].name_tile)
                {
                    case "Preview_Sokoban_10"://p purple
                        if (CheckBoxOnPoint(i, "Preview_Sokoban_8")){
                            cont++;
                        }
                        break;
                    case "Preview_Sokoban_12":// p yellow
                        if (CheckBoxOnPoint(i, "Preview_Sokoban_4")){
                            cont++;
                        }
                        break;
                    case "Preview_Sokoban_13"://p red
                        if (CheckBoxOnPoint(i, "Preview_Sokoban_2")){
                            cont++;
                        }
                        break;
                    case "Preview_Sokoban_11"://p blue
                        if (CheckBoxOnPoint(i, "Preview_Sokoban_6")){
                            cont++;
                        }
                        break;
                }
            }
        }
        if (cont == positionButtons.Count)
        {
            Win();
        }

    }

    private bool CheckBoxOnPoint(int i, string box)
    {
        if (tiles[positionButtons[i].x, positionButtons[i].y].GetComponent<Image>().sprite.name == box)
        {
            return true;
        }
        return false;
    }

    private void Win()
    {
        win.SetActive(true);
        if (GoToLoadLv.GetComponent<MapToPlay>().historyLevelsCount >=9)
        {
            GameObject objA = GameObject.Find("nextLevel");
            objA.SetActive(false);
        }
    }

}



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level 
{
    public LevelDataSerializable data = new LevelDataSerializable();
    public int[] tiles;
    public int[] lastPosPlayer;
    public string mapName;

    public void LoadData()//cargar
    {
        mapName = data.mapName;
        lastPosPlayer = data.lastPosPlayer;
        tiles = data.tiles;
    }

    public void StoreData()//guardar
    {
        data.tiles = tiles;
        data.lastPosPlayer = lastPosPlayer;
        data.mapName = mapName;
    }
}

[Serializable]//Indica a unity que esto se guarda. 
public class LevelDataSerializable
{
    public int[] tiles;//Guardar posicion de elementos
    public int[] lastPosPlayer;//Guardar posicion jugador
    public string mapName;//Nombre del mapa
}

public class AuxButtons
{
    public string name_tile;
    public int x;
    public int y;
}

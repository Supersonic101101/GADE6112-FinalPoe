using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour
{
    public const int SIZE = 20;

    Unit[] units;
    Building[] buildings;

    Random random = new Random();

    string[,] map;
    string[] factions = { "Shirai-Ryu", "Lin-Kuei", "Wizards"};

    int numUnits;
    int numBuildings;

    public Map(int numUnits, int numBuildings)
    {
        this.numUnits = numUnits;
        this.numBuildings = numBuildings;

        Reset();
    }

    public Unit[] Units
    {
        get { return units; }
    }

    public Building[] Buildings
    {
        get { return buildings; }
    }

    public int Size
    {
        get { return SIZE; }
    }

    public void Reset()
    {
        map = new string[SIZE, SIZE];
        InitializeUnits();
        InitializeBuildings();
        UpdateMap(UnitAndBuildingManager.manager);
    }

    public void InitializeUnits()
    {
        units = new Unit[numUnits];

        for (int i = 0; i < units.Length; i++)
        {
            int x = Random.Range(0, SIZE);
            int y = Random.Range(0, SIZE);
            int factionIndex = Random.Range(0, 3);
            int unitType = Random.Range(0, 3);

            while (map[x, y] != null)
            {
                x = Random.Range(0, SIZE);
                y = Random.Range(0, SIZE);
            }

            if (unitType == 0)
            {
                units[i] = new MeleeUnit(x, y, factions[factionIndex]);
            }
            else
            {
                units[i] = new RangedUnit(x, y, factions[factionIndex]);
            }
            
        }
    }

    public void InitializeBuildings()
    {
        buildings = new Building[numBuildings];

        for (int i = 0; i < buildings.Length; i++)
        {
            int x = Random.Range(0, SIZE);
            int y = Random.Range(0, SIZE);
            int factionIndex = Random.Range(0, 2);
            int buildingType = Random.Range(0, 2);

            while (map[x, y] != null)
            {
                x = Random.Range(0, SIZE);
                y = Random.Range(0, SIZE);
            }

            if (buildingType == 0)
            {
                buildings[i] = new ResourceBuilding(x, y, factions[factionIndex]);
            }
            else
            {
                buildings[i] = new FactoryBuilding(x, y, factions[factionIndex]);
            }
            map[x, y] = buildings[i].Faction[0] + "/";
        }
    }

    public void AddUnit(Unit unit)
    {
        //We can use Array.Resize, but let's do it ourselves
        Unit[] resizeUnits = new Unit[units.Length + 1];

        for (int i = 0; i < units.Length; i++)
        {
            resizeUnits[i] = units[i];
        }
        resizeUnits[resizeUnits.Length - 1] = unit;
        units = resizeUnits;

        //It would make sense to use List instead - Lists can change size dynamically
    }

    public void AddBuilding(Building building)
    {
        Array.Resize(ref buildings, buildings.Length + 1);
        buildings[buildings.Length - 1] = building;
    }

    public void UpdateMap(UnitAndBuildingManager manager)
    {
        for (int y = 0; y < SIZE; y++)
        {
            for (int x = 0; x < SIZE; x++)
            {
                map[x, y] = "   ";
            }
        }

        foreach (Unit unit in units)
        {
            map[unit.X, unit.Y] =  "|" + unit.Faction[0];
        }

        foreach (Building building in buildings)
        {
            map[building.X, building.Y] = "|" + building.Faction[0];
        }
    }

    public string GetMapDisplay()
    {
        string mapString = "";
        for (int y = 0; y < SIZE; y++)
        {
            for (int x = 0; x < SIZE; x++)
            {
                mapString += map[x, y];
            }
            mapString += "\n";
        }
        return mapString;
    }

    public void Clear()
    {
        units = new Unit[0];
        buildings = new Building[0];
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

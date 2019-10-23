using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    const string SHIRAI_RYU = "Shirai-Ryu";
    const string LIN_KUEI = "Lin-Kuei";
    const string WIZARDS = "Wizards";
    public static Random random = new Random();

    const string UNITS_FILENAME = "units.txt";
    const string BUIDLINGS_FILENAME = "buildings.txt";
    const string ROUND_FILENAME = "rounds.txt";

    Map map;
    UnitAndBuildingManager manager;
    bool isGameOver = false;
    string winningFaction = "";
    int round = 0;

    string[] factions = { SHIRAI_RYU, LIN_KUEI, WIZARDS };



    public GameManager()
    {

        map = new Map(10, 10);
    }

    public bool IsGameOver
    {
        get { return isGameOver; }
    }

    public string WinningFaction
    {
        get { return winningFaction; }
    }

    public int Round
    {
        get { return round; }
    }

    public void GameLoop()
    {
        UpdateUnits();
        UpdateBuildings();
        map.UpdateMap(manager);
        round++;
    }

    void UpdateUnits()
    {
        foreach (Unit unit in map.Units)
        {
            //ignore this unit if it is destroyed
            if (unit.IsDestroyed)
            {

                continue;
            }

            Unit closestUnit = unit.GetClosestUnit(map.Units);
            if (closestUnit == null)
            {
                //if a unit has not target it means the game has ended
                isGameOver = true;
                winningFaction = unit.Faction;
                map.UpdateMap(manager);
                return;
            }

            double healthPercentage = unit.Health / unit.MaxHealth;
            if (healthPercentage <= 0.25)
            {
                unit.RunAway();
            }
            else if (unit.IsInRange(closestUnit))
            {
                unit.Attack(closestUnit);
            }
            else
            {
                unit.Move(closestUnit);
            }
            StayInBounds(unit, map.Size);
        }
    }

    void UpdateBuildings()
    {
        foreach (Building building in map.Buildings)
        {

            if (building is FactoryBuilding)
            {
                FactoryBuilding factoryBuilding = (FactoryBuilding)building;

                if (round % factoryBuilding.ProductionSpeed == 0)
                {
                    Unit newUnit = factoryBuilding.SpawnUnit();
                    map.AddUnit(newUnit);
                }
            }
            else if (building is ResourceBuilding)
            {
                ResourceBuilding resourceBuilding = (ResourceBuilding)building;
                resourceBuilding.GenerateResources();
            }
        }
    }

    public int NumUnits
    {
        get { return map.Units.Length; }
    }

    public int NumBuildings
    {
        get { return map.Buildings.Length; }
    }

    public string MapDisplay
    {
        get { return map.GetMapDisplay(); }
    }

    public string GetUnitInfo()
    {
        string unitInfo = "";
        foreach (Unit unit in map.Units)
        {
            unitInfo += unit + Environment.NewLine;
        }
        return unitInfo;
    }

    public string GetBuildingsInfo()
    {
        string buildingsInfo = "";
        foreach (Building building in map.Buildings)
        {
            buildingsInfo += building + Environment.NewLine;
        }
        return buildingsInfo;
    }

    public void Reset()
    {
        map.Reset();
        isGameOver = false;
        round = 0;
    }

    public void SaveGame()
    {
        Save(UNITS_FILENAME, map.Units);
        Save(BUIDLINGS_FILENAME, map.Buildings);
        SaveRound();
    }

    public void LoadGame()
    {
        map.Clear();
        Load(UNITS_FILENAME);
        Load(BUIDLINGS_FILENAME);
        LoadRound();
        map.UpdateMap(manager);
    }

    private void Load(string filename)
    {
        FileStream inFile = new FileStream(filename, FileMode.Open, FileAccess.Read);
        StreamReader reader = new StreamReader(inFile);
        string recordIn;

        recordIn = reader.ReadLine();
        while (recordIn != null)
        {
            int length = recordIn.IndexOf(",");

            string firstField = recordIn.Substring(0, length);

            switch (firstField)
            {
                case "Melee": map.AddUnit(new MeleeUnit(recordIn)); break;
                case "Ranged": map.AddUnit(new RangedUnit(recordIn)); break;
                case "Factory": map.AddBuilding(new FactoryBuilding(recordIn)); break;
                case "Resource": map.AddBuilding(new ResourceBuilding(recordIn)); break;
            }

            recordIn = reader.ReadLine();
        }
        reader.Close();
        inFile.Close();
    }

    private void Save(string filename, object[] objects)
    {
        FileStream outFile = new FileStream(filename, FileMode.Create, FileAccess.Write);
        StreamWriter writer = new StreamWriter(outFile);
        foreach (object obj in objects)
        {
            if (obj is Unit)
            {
                Unit unit = (Unit)obj;
                writer.WriteLine(unit.Save());
            }
            else if (obj is Building)
            {
                Building unit = (Building)obj;
                writer.WriteLine(unit.Save());
            }
        }
        writer.Close();
        outFile.Close();
    }

    private void SaveRound()
    {
        FileStream outFile = new FileStream(ROUND_FILENAME, FileMode.Create, FileAccess.Write);
        StreamWriter writer = new StreamWriter(outFile);
        writer.WriteLine(round);
        writer.Close();
        outFile.Close();
    }

    private void LoadRound()
    {
        FileStream inFile = new FileStream(ROUND_FILENAME, FileMode.Open, FileAccess.Read);
        StreamReader reader = new StreamReader(inFile);
        round = int.Parse(reader.ReadLine());
        reader.Close();
        inFile.Close();
    }

    private void StayInBounds(Unit unit, int size)
    {
        Vector3 unitPosition = unit.transform.position;
        if (unitPosition.x < 0)
        {
            unitPosition.x = 0;
        }
        else if (unitPosition.x >= size)
        {
            unitPosition.x = size - 1;
        }

        if (unitPosition.y < 0)
        {
            unitPosition.y = 0;
        }
        else if (unitPosition.y >= size)
        {
            unitPosition.y = size - 1;
        }
    }

    public float mapWidth;
    public float mapHeight;
    public GameObject meleeUnit;
    public GameObject rangedUnit;
    public GameObject wizard;
    public GameObject factoryBuilding;
    public GameObject resourceBuilding;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f), 0);
        Instantiate(meleeUnit, position, Quaternion.identity);
        Vector3 position2 = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f), 0);
        Instantiate(rangedUnit, position2, Quaternion.identity);
        Vector3 position3 = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(-5.0f, 5.0f), 0);
        Instantiate(wizard, position3, Quaternion.identity);
        Vector3 position4 = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(-5.0f, 5.0f), 0);
        Instantiate(resourceBuilding, position4, Quaternion.identity);
        Vector3 position5 = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-4.0f, 4.0f), 0);
        Instantiate(factoryBuilding, position5, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CopyBall()
    {
        void CopyBall()
        {
            Vector3 position = Vector3.left * Random.Range(-5f, 5f);
            GameObject meleeUnits = Instantiate(meleeUnit, position, Quaternion.identity);
            Vector3 position2 = Vector3.left * Random.Range(-5f, 5f);
            GameObject rangedUnits = Instantiate(rangedUnit, position, Quaternion.identity);
            Vector3 position3 = Vector3.left * Random.Range(-6f, 6f);
            GameObject wizardUnits = Instantiate(wizard, position, Quaternion.identity);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

enum FactoryType
{
    MELEE,
    RANGED
}

public class FactoryBuilding : Building
{
    private FactoryType type;

    private int productionSpeed;
    private int spawnY;
    private string v;

    public void InitBuilding(int x, int y, string faction) 
    {
        base.InitBuilding(x, y, 100, faction, '~');
        if (y >= Map.SIZE - 1)
        {
            spawnY = y - 1;
        }
        else
        {
            spawnY = y + 1;
        }
        type = (FactoryType)Random.Range(0, 2);
        productionSpeed = Random.Range(3, 7);
    }

    public FactoryBuilding(string values)
    {
        string[] parameters = values.Split(',');

        x = int.Parse(parameters[1]);
        y = int.Parse(parameters[2]);
        health = int.Parse(parameters[3]);
        maxHealth = int.Parse(parameters[4]);
        type = (FactoryType)int.Parse(parameters[5]);
        productionSpeed = int.Parse(parameters[6]);
        spawnY = int.Parse(parameters[7]);
        faction = parameters[8];
        //symbol = parameters[9][0];
        isDestroyed = parameters[10] == "True" ? true : false;
    }

    public FactoryBuilding(int x, int y, string v)
    {
        this.x = x;
        this.y = y;
        this.v = v;
    }

    public int ProductionSpeed
    {
        get { return productionSpeed; }
    }

    public override void Destroy()
    {
        isDestroyed = true;
        //symbol = '_';
    }

    public override string Save()
    {
        return string.Format($"Factory,{x},{y},{health},{maxHealth},{(int)type},{productionSpeed},{spawnY},{faction},{isDestroyed}");
    }

    public Unit SpawnUnit()
    {
        Unit unit;
        if (type == FactoryType.MELEE)
        {
            unit = new MeleeUnit(x, spawnY, faction);
        }
        else
        {
            unit = new RangedUnit(x, spawnY, faction);
        }
        return unit;
    }

    string GetFactoryTypeName()
    {
        return new string[] { "Melee", "Ranged" }[(int)type];
    }

    public override string ToString()
    {
        return
            "------------------------------------------" + Environment.NewLine +
            "Factory Building (" + "/" + faction[0] + ")" + Environment.NewLine +
            "------------------------------------------" + Environment.NewLine +
            "Type: " + GetFactoryTypeName() + Environment.NewLine +
            base.ToString() + Environment.NewLine;
    }
}

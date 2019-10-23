using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeleeUnit : Unit
{
    private string v;
    private string recordIn;

    public void InitUnit(int x, int y, string faction) { base.InitUnit(x, y, 200, 2, 20, 1, faction, '*', "Samurai"); }
    
    public void InitUnit(string values) { base.InitUnit(values); }

    public MeleeUnit(int x, int y, string v)
    {
        this.x = x;
        this.y = y;
        this.v = v;
    }

    public MeleeUnit(string recordIn)
    {
        this.recordIn = recordIn;
    }

    public override string Save()
    {
        return string.Format($"Melee,{x},{y},{health},{maxHealth},{speed},{attack},{attackRange},{faction},{name},{isDestroyed}");
    }

    internal override double GetDistance(Target target)
    {
        throw new NotImplementedException();
    }
}

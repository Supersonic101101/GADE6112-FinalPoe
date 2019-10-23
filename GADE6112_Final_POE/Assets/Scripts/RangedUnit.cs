using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

class RangedUnit : Unit
{
    private string recordIn;
    private string v;

    public RangedUnit(string recordIn)
    {
        this.recordIn = recordIn;
    }

    public RangedUnit(int x, int y, string v)
    {
        this.x = x;
        this.y = y;
        this.v = v;
    }

    public void InitRangeUnit(int x, int y, string faction) { base.InitUnit(x, y, 100, 2, 20, 3, faction, '*', "Gunman"); }

    
    internal override double GetDistance(Target target)
    {
        throw new NotImplementedException();
    }
    public override string Save()
    {
        return string.Format(
            $"Ranged,{x},{y},{health},{maxHealth},{speed},{attack},{attackRange}," +
            $"{faction},{symbol},{name},{isDestroyed}"
        );
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wizard : Unit
{
    public Wizard(int x, int y, string faction)
    {
        base.InitUnit(x, y, 100, 1, 15, 5, faction, "Warlock");
    }

    public Wizard(string values)
    {
        base.InitUnit(values);
    }
    internal override double GetDistance(Target target)
    {
        throw new NotImplementedException();
    }
    public override string Save()
    {
        return string.Format(
            $"Wizard,{x},{y},{health},{maxHealth},{speed},{attack},{attackRange}," +
            $"{faction},{name},{isDestroyed}"
        );
    }
}

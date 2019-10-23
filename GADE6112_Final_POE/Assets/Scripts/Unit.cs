using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public abstract class Unit : Target
{
    protected int x;

    internal void InitUnit(int x, int y, int v1, int v2, int v3, int v4, string faction, string v5)
    {
        throw new NotImplementedException();
    }

    protected int y;
    protected int health;
    protected int maxHealth;
    protected int speed;
    protected int attack;
    protected int attackRange;
    protected string faction;
    protected char symbol;
    protected bool isAttacking = false;
    protected string name;

    protected bool isDestroyed = false;
    public static Random random = new Random();
    private object closestTarget;

    public void InitUnit(int x, int y, int health, int speed, int attack, int attackRange, string faction, char symbol, string name)
    {
        transform.position = new Vector3(x, y); 

        this.x = x;
        this.y = y;
        this.health = health;
        maxHealth = health;
        this.speed = speed;
        this.attack = attack;
        this.attackRange = attackRange;
        this.faction = faction;
        //this.symbol = symbol;
        this.name = name;
    }

    public void InitUnit(string values)
    {
        string[] parameters = values.Split(',');
        transform.position = new Vector3(int.Parse(parameters[1]), int.Parse(parameters[2]));
        x = int.Parse(parameters[1]);
        y = int.Parse(parameters[2]);
        health = int.Parse(parameters[3]);
        maxHealth = int.Parse(parameters[4]);
        speed = int.Parse(parameters[5]);
        attack = int.Parse(parameters[6]);
        attackRange = int.Parse(parameters[7]);
        faction = parameters[8];
        //symbol = parameters[9][0];
        name = parameters[10];
        isDestroyed = parameters[11] == "True" ? true : false;
    }

    public abstract string Save();

    public int X
    {
        get { return x; }
        set { x = value; }
    }

    public int Y
    {
        get { return y; }
        set { y = value; }
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
    }

    public string Faction
    {
        get { return faction; }
    }

    //public char Symbol
   //{
       // get { return symbol; }
   // }

    public bool IsDestroyed
    {
        get { return isDestroyed; }
    }

    public string Name
    {
        get { return name; }
    }

    public virtual void Attack(Unit otherUnit)
    {
        isAttacking = true;
        otherUnit.Health -= attack;

        if (otherUnit.Health <= 0)
        {
            otherUnit.Health = 0;
            otherUnit.Destroy();
        }
    }

    internal abstract double GetDistance(Target target);

    public virtual void Destroy()
    {
        Health = 0;
        isDestroyed = true;
        isAttacking = false;
        symbol = 'X';
    }

    public virtual Unit GetClosestUnit(Unit[] units)
    {
        double closestDistance = int.MaxValue;
        Unit closestUnit = null;

        foreach (Unit otherUnit in units)
        {
            // we don't attack ourselves or members of our faction or destroyed units.
            if (otherUnit == this || otherUnit.Faction == faction || otherUnit.IsDestroyed)
            {
                continue;
            }

            double distance = GetDistance(otherUnit);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestUnit = otherUnit;
            }
        }

        return closestUnit;
    }

    public virtual bool IsInRange(Unit otherUnit)
    {
        return GetDistance(otherUnit) <= attackRange;
    }

    public virtual void Move(Unit closestUnit)
    {
        Vector3 direction = (transform.position - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;

        int xDirection = closestUnit.X - X;
        int yDirection = closestUnit.Y - Y;

        if (Math.Abs(xDirection) > Math.Abs(yDirection))
        {
            x += Math.Sign(xDirection);
        }
        else
        {
            y += Math.Sign(yDirection);
        }
    }

    protected double GetDistance(Unit otherUnit)
    {
        double xDistance = otherUnit.X - X;
        double yDistance = otherUnit.Y - Y;
        //return xDistance * xDistance + yDistance * yDistance;
        return Math.Sqrt(xDistance * xDistance + yDistance * yDistance);
    }

    public virtual void RunAway()
    {
        int direction = Random.Range(0, 4);
        if (direction == 0)
        {
            transform.position = new Vector3(1, 0);
        }
        else if (direction == 1)
        {
            x -= 1;
        }
        else if (direction == 2)
        {
            y += 1;
        }
        else
        {
            y -= 1;
        }
    }

    public override string ToString()
    {
        return
            "------------------------------------------" + Environment.NewLine +
            name + " (" + symbol + "/" + faction[0] + ")" + Environment.NewLine +
            "------------------------------------------" + Environment.NewLine +
            "Faction: " + faction + Environment.NewLine +
            "Position: " + x + ", " + y + Environment.NewLine +
            "Health: " + health + " / " + maxHealth + Environment.NewLine;
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

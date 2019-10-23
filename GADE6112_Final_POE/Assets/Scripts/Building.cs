using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Building : Target
{
    
        protected int x;
        protected int y;
        protected int health;
        protected int maxHealth;
        protected string faction;
        //protected char symbol;
        protected bool isDestroyed = false;

        public void InitBuilding(int x, int y, int health, string faction, char symbol)
        {
            this.x = x;
            this.y = y;
            this.health = health;
            this.maxHealth = health;
            this.faction = faction;
            //this.symbol = symbol;
        }

        public Building() { }

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public string Faction
        {
            get { return faction; }
        }

       /* public char Symbol
        {
            get { return symbol; }
        }*/

        public abstract void Destroy();
        public abstract string Save();

        //Deliberately not making ToString() abstract because it's already a virtual method.

        public override string ToString()
        {
            return
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

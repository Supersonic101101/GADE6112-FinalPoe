using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Target : MonoBehaviour
{
    protected int x;
    protected int y;
    protected int health;
    protected int maxHealth;
    protected bool isDestroyed;
    protected string faction;

    int hideChecksBeforeInvisible = 5;
    bool isVisible = true;

    public string Faction
    {
        get { return faction; }
    }

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

    public bool IsDestroyed
    {
        get { return isDestroyed; }
    }

    public bool IsVisible
    {
        get { return isVisible; }
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

    public double GetDistance(Target to)
    {
        //double xDistance = to.X - X;
        //double yDistance = to.Y - Y;
        return Vector3.Distance(transform.position, to.transform.position);
        //return Math.Sqrt(xDistance * xDistance + yDistance * yDistance);
    }

    public virtual void Destroy()
    {
        isDestroyed = true;
    }

    public void CheckHide()
    {
        if (hideChecksBeforeInvisible == 0)
            return;

        hideChecksBeforeInvisible--;
        isVisible = hideChecksBeforeInvisible > 0;
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

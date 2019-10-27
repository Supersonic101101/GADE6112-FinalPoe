using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcBehaviour : MonoBehaviour
{
    public float speed;
    public float moveRate;

    public int dirX;
    public int dirY;

    private float moveCounter;

    private new Rigidbody2D rigidbody2D { get { return GetComponent<Rigidbody2D>() ?? default(Rigidbody2D); } }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidbody2D)
        {
            if(moveCounter > moveRate)
            {
                ChangeDirection();
                moveCounter = 0f;
            }
            Vector2 vel = new Vector2(dirX * speed, dirY * speed);

            rigidbody2D.velocity = Vector2.Lerp(rigidbody2D.velocity, vel, Time.deltaTime * 10f);

            moveCounter += Time.deltaTime;
        }
    }

    private void ChangeDirection()
    {
        dirX = Random.Range(-1, 1);
        dirY = Random.Range(-1, 1);
    }
}

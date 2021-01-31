using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    public float myTTL = 2;
    public Vector2 myVelocity = new Vector2(5, 5);
    public Rigidbody2D myRB;
    public RaycastHit2D inView;

    // Start is called before the first frame update
    void Start()
    {
        myRB = this.gameObject.GetComponent<Rigidbody2D>();

        RaycastHit2D inViewL = Physics2D.Raycast(transform.position, Vector2.left);
        if (inViewL)
        {
            // look for 'player'
            if (inViewL.transform.tag == "Player")
            {
                myVelocity.x = myVelocity.x*-1;
            }
        }
        /*
        RaycastHit2D inViewR = Physics2D.Raycast(transform.position, Vector2.right);
        if (inViewR)
        {
        //planning on having this determin distance from player and lob on the spot
        //- static lob used
            // look for 'player'
            if (inViewR.transform.tag == "Player")
            {
                Debug.Log(inView.distance + " right distance");
              //  myVelocity.x = inView.distance * -1;
            }
        }
        */

      //  myVelocity.y = 6;
        myRB.velocity = myVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        myTTL -= Time.deltaTime;
        if (myTTL < 0)
        {
            Destroy(this.gameObject);
        }
    }
}

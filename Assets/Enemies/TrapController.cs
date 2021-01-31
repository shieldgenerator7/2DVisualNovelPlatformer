using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    public float myTTL = 2;
    public Vector2 myVelocity = new Vector2(5, 5);
   // public float mySpeed = -5;
    public Rigidbody2D myRB;
    public RaycastHit2D inView;

    // Start is called before the first frame update
    void Start()
    {
        myRB = this.gameObject.GetComponent<Rigidbody2D>();
        


      //  Ray2D playerCheck = new Ray2D(transform.position, Vector2.right);
        //inView = Physics2D.Raycast(transform.position, Vector2.left);
        RaycastHit2D inViewL = Physics2D.Raycast(transform.position, Vector2.left);
        
        if (inViewL)
        {
            //Debug.Log(inView.transform.position);
          //  Debug.Log("left");
            // look for 'player'
            if (inViewL.transform.tag == "Player")
            {
             //   Debug.Log(inView.distance);
                myVelocity.x = inView.distance;
            }
           
        }

     //   playerCheck = new Ray2D(transform.position, Vector2.left);
       // inView = Physics2D.Raycast(transform.position, Vector2.left);
        RaycastHit2D inViewR = Physics2D.Raycast(transform.position, Vector2.right);
        if (inViewR)
        {
            // look for 'player'
          //  Debug.Log("Right");
            if (inViewR.transform.tag == "Player")
            {
             //   Debug.Log(inView.distance);
                myVelocity.x = inView.distance * -1;
            }
        }

        myVelocity.y = 6;
        myRB.velocity = myVelocity;
     //   Debug.Log(myRB.velocity);
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

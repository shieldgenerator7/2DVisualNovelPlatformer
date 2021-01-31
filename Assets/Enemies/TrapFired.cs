using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFired : MonoBehaviour
{
    public float myTTL = 2;
    public Vector3 myVelocity = new Vector3 (5,5,0);
    public float mySpeed = -5;
    public Rigidbody myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = this.gameObject.GetComponent<Rigidbody>();
        RaycastHit inView;
        

        Ray playerCheck = new Ray(transform.position, Vector3.right);
        if (Physics.Raycast(playerCheck, out inView))
        {
            // look for 'player'
            if (inView.transform.tag == "Player")
            {
                Debug.Log(inView.distance);
                myVelocity.x = inView.distance;
            }

            
        }
        
        playerCheck = new Ray(transform.position, Vector3.left);
        if (Physics.Raycast(playerCheck, out inView))
        {
            // look for 'player'
            if (inView.transform.tag == "Player")
            {

                myVelocity.x = inView.distance*-1;
            }

            
        }

        myVelocity.y = 6;
        myRB.velocity = myVelocity;
        Debug.Log(myRB);
    }

    // Update is called once per frame
    void Update()
    {
        myTTL -= Time.deltaTime;
        if(myTTL < 0)
        {
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFired : MonoBehaviour
{
    public float myTTL = 3;
    public Vector3 myVelocity = new Vector3 (0,3,0);
    public float myX = 5;
    public Rigidbody myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = this.gameObject.GetComponent<Rigidbody>();
        Ray playerCheck = new Ray(transform.position, Vector3.right);
        RaycastHit inView;
        if (Physics.Raycast(playerCheck, out inView))
        {
            // look for 'player'
            if (inView.transform.tag == "Player")
            {
                myX = myX * -1;
                
            }
            
            myVelocity.x = myX;
         //   myRB.velocity = myX;
            Debug.Log(myRB);
        }
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

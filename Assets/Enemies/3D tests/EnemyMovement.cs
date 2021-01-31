using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    /*
     *          Speed   Run     View/Range    
     * Small:   3       8       15
     * 
     * Shield:  1       3       10
     * 
     * Shoot:   2       0       20
     * 
     */

    public float moveSpeed;
    public float runSpeed;
    public Vector3 moveToPos;
    public float myHeight;
    public Vector3 oldPos;
    private static float nextStep = 1;

    public bool isPatrolling = true;
    public bool isHunting = false;
    public bool isLeft = false;

    public float viewDistance;
    public bool isShort;
    public bool isShield;
    public bool isShoot;

    public GameObject preTrap;
    public float fireTimer = 3f;
    public float timeToFire;
    public bool isArmed = false;
    
    void Awake()
    {
        moveToPos = oldPos = transform.position;
       // moveToPos.y = startingPos.y;
        myHeight = transform.localScale.y + .1f;

    }


    // Update is called once per frame
    void Update()
    {
        Ray playerCheck = new Ray(transform.position, Vector3.forward);
        Ray floorCheck = new Ray(transform.position, Vector3.down);

        if (isShoot && !isArmed)
        {
            timeToFire -= Time.deltaTime;
            if (timeToFire < 0)
            {
                isArmed = true;
            }
        }


        if (isPatrolling)
        {
            //check movement status and move
            

            if (Physics.Raycast(floorCheck, myHeight))
            {
                //march onward
                if (isLeft)
                {
                    moveToPos.x = transform.position.x - 1f;
                    playerCheck = new Ray(transform.position, Vector3.left);
                    oldPos = transform.position;
                }
                else
                {
                    moveToPos.x = transform.position.x + 1f;
                    playerCheck = new Ray(transform.position, Vector3.right);
                    oldPos = transform.position;
                }
            }
            else
            {
                //at edge - stop and turn around
                moveToPos = oldPos;
                nextStep = nextStep * -1;
                isLeft = !isLeft;
            }
            transform.position = Vector3.MoveTowards(transform.position, moveToPos, moveSpeed * Time.deltaTime);
            
            RaycastHit inView;
            if (Physics.Raycast(playerCheck, out inView))
            {
                // look for 'player'
             //   Debug.Log(inView.transform.tag);
            //    Debug.Log(inView.distance);

                if (inView.transform.tag == "Player")
                {
                    if (inView.distance < viewDistance)
                    {
                        isHunting = true;
                        isPatrolling = false;
                    }
                }
            }



        }


        if (isHunting)
        {
            //direction
            if (isLeft)
            {
                moveToPos.x = transform.position.x - 1f;
                playerCheck = new Ray(transform.position, Vector3.left);
            }
            else
            {
                moveToPos.x = transform.position.x + 1f;
                playerCheck = new Ray(transform.position, Vector3.right);
            }

            //Ray floorCheck = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(floorCheck, myHeight))
            { 
                //while on platform...
                transform.position = Vector3.MoveTowards(transform.position, moveToPos, runSpeed * Time.deltaTime);
            }
            else //fall
            {
                moveToPos.y = transform.position.y - 1f;
                transform.position = Vector3.MoveTowards(transform.position, moveToPos, runSpeed * Time.deltaTime);
            }


            RaycastHit inView;
            if (Physics.Raycast(playerCheck, out inView))
            {
                // look for 'player'

                if (inView.transform.tag == "Player")
                {
                    if (inView.distance < viewDistance)
                    {
                        isHunting = true;
                        isPatrolling = false;
                    }

                }
            }
            else
            if (!isShield)
            {
                //only disable charge for non-shields
                isHunting = false;
                isPatrolling = true;
            }
            
            if (isShoot && isArmed)
            {
                //only fire for shoot
                fireAway();
                isArmed = false;
                timeToFire = fireTimer;
            }
        }
    }

    void fireAway()
    {
     //        Instantiate(preTrap, transform.position, Quaternion.identity);
        Debug.Log("fired");
    }

    void takeHit()
    {
        
    }
}


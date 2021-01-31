using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
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
    public Vector2 moveToPos;
    public float myHeight;
    public Vector2 oldPos;
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
    public RaycastHit2D inView;

    void Awake()
    {
        moveToPos = oldPos = transform.position;
        myHeight = transform.localScale.y+1f;
    }


    // Update is called once per frame
    void Update()
    {
        Ray2D playerCheck = new Ray2D(transform.position, Vector2.down);
        Ray2D floorCheck1 = new Ray2D(transform.position, Vector2.down);
        RaycastHit2D floorCheck = Physics2D.Raycast(transform.position, Vector2.down, myHeight);
       // RaycastHit2D inView = Physics2D.Raycast(transform.position, Vector2.right);
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
            //  Debug.Log(floorCheck);
           // RaycastHit2D inView;
            if (floorCheck)
            {
               
                //march onward
                if (isLeft)
                {
                    moveToPos.x = transform.position.x - 1f;
                    playerCheck = new Ray2D(transform.position, Vector2.left);
                    oldPos = transform.position;
                    inView = Physics2D.Raycast(transform.position, Vector2.left);
                }
                else
                {
                    moveToPos.x = transform.position.x + 1f;
                    playerCheck = new Ray2D(transform.position, Vector2.right);
                    oldPos = transform.position;
                    inView = Physics2D.Raycast(transform.position, Vector2.right);


                }
            }
            else
            {
                 //   Debug.Log("false");
                //at edge - stop and turn around
                moveToPos = oldPos;
                nextStep = nextStep * -1;
                isLeft = !isLeft;
                
            }
            transform.position = Vector2.MoveTowards(transform.position, moveToPos, moveSpeed * Time.deltaTime);

           
            if (inView)
            {
              //  Debug.Log(inView.transform.tag);
                // look for 'player'
                //   Debug.Log(inView.transform.tag);
                //    Debug.Log(inView.distance);
              //  Debug.Log(playerCheck);

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
                playerCheck = new Ray2D(transform.position, Vector2.left);
            }
            else
            {
                moveToPos.x = transform.position.x + 1f;
                playerCheck = new Ray2D(transform.position, Vector2.right);
            }

            //Ray floorCheck = new Ray(transform.position, Vector3.down);
            if (floorCheck)
            {
               // //while on platform...
                transform.position = Vector2.MoveTowards(transform.position, moveToPos, runSpeed * Time.deltaTime);
            }
            else //fall
            {
                moveToPos.y = transform.position.y - 1f;
                transform.position = Vector2.MoveTowards(transform.position, moveToPos, runSpeed * Time.deltaTime);
            }


            RaycastHit2D inViewL = Physics2D.Raycast(transform.position, Vector2.left);
            RaycastHit2D inViewR = Physics2D.Raycast(transform.position, Vector2.right);
            if (inViewL || inViewR)
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
 
        Instantiate(preTrap, transform.position, Quaternion.identity);
        Debug.Log("fired");
 
        
    }

    void takeHit()
    {
        //play animation then...
        Destroy(this.gameObject);
    }
}

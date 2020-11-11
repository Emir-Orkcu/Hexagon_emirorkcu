using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public int previous_y;
    public int previous_x;
    public float swipe_a = 0;
    private Vector2 firstTouchPosition;
    private Vector2 secondTouchPosition;
    public int y_Axis; //column
    public int x_Axis; //row
    private GameObject othercube;
    private cubemaker board;
    public int targetX;
    public int targetY;
    private Vector2 tempPosition;
    public bool ismatched = false;
    public float swipe_r = 1f;


    void Start()
    {
        board = FindObjectOfType<cubemaker>();
        //targetX = (int)transform.position.x;
        //targetY = (int)transform.position.y;
        //x_Axis = targetY;
        //y_Axis = targetX;
        //previous_x = x_Axis;
        //previous_y = y_Axis;
    }

    
    void Update()
    {
        FindMatch();

        if (ismatched)
        {
            GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 0f, 1f);

        }


        targetX = y_Axis;
        targetY = x_Axis;
        if (Mathf.Abs(targetX - transform.position.x) > .1)
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if (board.allcubes[y_Axis, x_Axis] != this.gameObject)
            {
                board.allcubes[y_Axis, x_Axis] = this.gameObject;
            }
        }
        else
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
            
              
        }
        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            tempPosition = new Vector2(transform.position.x,targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if (board.allcubes[y_Axis, x_Axis] != this.gameObject)
            {
                board.allcubes[y_Axis, x_Axis] = this.gameObject;
            }
        }
        else
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
            

        }

    }

    public IEnumerator checkmove()
    {

        yield return new WaitForSeconds(.5f);
        if (othercube != null)
        {
            if(!ismatched && !othercube.GetComponent<Cube>().ismatched)
            {
                othercube.GetComponent<Cube>().x_Axis = x_Axis;
                othercube.GetComponent<Cube>().y_Axis = y_Axis;
                x_Axis = previous_x;
                y_Axis = previous_y;
            }
            else
            {
                board.DestroyMatches();
            }
            othercube = null;
        }
        

    }


   private void OnMouseDown()
    {

        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        

    }
    private void OnMouseUp()
    {

        secondTouchPosition= Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SumAngle();
    }
    void SumAngle()
    {
        if (Mathf.Abs(secondTouchPosition.y - firstTouchPosition.y) > swipe_r || Mathf.Abs(secondTouchPosition.x - firstTouchPosition.x) > swipe_r)
        {

            swipe_a = Mathf.Atan2(secondTouchPosition.y - firstTouchPosition.y, secondTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            Moving();

        }
    }

    void Moving()
    {
        if(swipe_a>-45 && swipe_a <= 45 && y_Axis<board.width-1)
        {
            othercube = board.allcubes[y_Axis + 1, x_Axis];
            previous_x = x_Axis;
            previous_y = y_Axis;
            othercube.GetComponent<Cube>().y_Axis -= 1;
            y_Axis += 1;
                //right

        }else if (swipe_a > 45 && swipe_a <= 135 && x_Axis<board.height-1)
        {
            othercube = board.allcubes[y_Axis, x_Axis + 1];
            previous_x = x_Axis;
            previous_y = y_Axis;
            othercube.GetComponent<Cube>().x_Axis -= 1;
            x_Axis += 1;
                //up

        }else if ((swipe_a > 135 || swipe_a <= -135)&& y_Axis>0)
        {
            othercube = board.allcubes[y_Axis - 1, x_Axis];
            previous_x = x_Axis;
            previous_y = y_Axis;
            othercube.GetComponent<Cube>().y_Axis += 1;
            y_Axis -= 1;
                //left

        }else if (swipe_a < -45 && swipe_a >= -135 && x_Axis>0)
        {
            othercube = board.allcubes[y_Axis, x_Axis - 1];
            previous_x = x_Axis;
            previous_y = y_Axis;
            othercube.GetComponent<Cube>().x_Axis += 1;
            x_Axis -= 1;
                //down


        }

        StartCoroutine(checkmove());


    }

    void FindMatch()
    {

        if(y_Axis>0 && y_Axis < board.width - 1)
        {
            GameObject leftcube1 = board.allcubes[y_Axis - 1, x_Axis];
            GameObject rightcube1 = board.allcubes[y_Axis + 1, x_Axis];
            if (leftcube1 != null && rightcube1 != null)
            {

                if (leftcube1.tag == this.gameObject.tag && rightcube1.tag == this.gameObject.tag)
                {
                    leftcube1.GetComponent<Cube>().ismatched = true;
                    rightcube1.GetComponent<Cube>().ismatched = true;
                    ismatched = true;
                }
            }

                
        }
        if (x_Axis > 0 && x_Axis < board.height - 1)
        {
            GameObject upcube1 = board.allcubes[y_Axis , x_Axis+1];
            GameObject downcube1 = board.allcubes[y_Axis , x_Axis-1];
            if (upcube1 != null && downcube1 != null)
            {
                if (upcube1.tag == this.gameObject.tag && downcube1.tag == this.gameObject.tag)
                {
                    upcube1.GetComponent<Cube>().ismatched = true;
                    downcube1.GetComponent<Cube>().ismatched = true;
                    ismatched = true;
                }
            }

        }

    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class cubemaker : MonoBehaviour
{
    public int height;
    public int width;
    public int offset;
    public GameObject background_object;
    public GameObject[] cubes;
    private Background_s[,] allgrounds;
    public GameObject[,] allcubes;
    public int score;
    public Text ScoreBoard;
    //public int HighScore = 0;
    //public Text HighScore_t;
    


    void Start()
    {
        allgrounds = new Background_s[width, height];
        allcubes = new GameObject[width, height];
        SetUp();
        //HighScore_t.text = "Point"+HighScore;


    }

    private void SetUp()
    {
        for(int i=0; i<width; i++)
        {
            for(int a=0; a<height; a++)
            {
                Vector2 Position =new Vector2(i, a+offset);
                GameObject backgrond_sss = Instantiate(background_object, Position,Quaternion.identity) as GameObject;
                backgrond_sss.transform.parent = this.transform;
                backgrond_sss.name = "(" + i + "," + a + ")";
                int cube_use = UnityEngine.Random.Range(0, cubes.Length);

                int maxIt =0;

                while (matchesat(i, a, cubes[cube_use]) && maxIt<100)
                {
                    cube_use = UnityEngine.Random.Range(0, cubes.Length);
                    maxIt++;
                    UnityEngine.Debug.Log(maxIt);
                }
                maxIt = 0;

                GameObject cube = Instantiate(cubes[cube_use], Position, Quaternion.identity);
                cube.GetComponent<Cube>().x_Axis = a;
                cube.GetComponent<Cube>().y_Axis = i;
                cube.transform.parent = this.transform;
                cube.name = "(" + i + "," + a + ")";
                allcubes[i, a] = cube;
            }


        }



    }

    private bool matchesat(int y_Axis, int x_Axis, GameObject piece)
    {
        if(y_Axis>1 && x_Axis > 1)
        {
            if(allcubes[y_Axis-1,x_Axis].tag==piece.tag && allcubes[y_Axis - 2, x_Axis].tag == piece.tag)
            {
                return true;
            }
            if (allcubes[y_Axis , x_Axis-1].tag == piece.tag && allcubes[y_Axis , x_Axis-2].tag == piece.tag)
            {
                return true;
            }
        }else if (y_Axis <= 1 || x_Axis <= 1)
        {
            if (x_Axis > 1)
            {
                if(allcubes[y_Axis,x_Axis-1].tag==piece.tag && allcubes[y_Axis, x_Axis - 2].tag == piece.tag)
                {
                    return true;
                }
            }
            if (y_Axis > 1)
            {
                if (allcubes[y_Axis-1, x_Axis].tag == piece.tag && allcubes[y_Axis-2, x_Axis].tag == piece.tag)
                {
                    return true;
                }
            }
        }
        

        return false;

    }

    private void DestroyAt(int y_Axis, int x_Axis)
    {
        if (allcubes[y_Axis, x_Axis].GetComponent<Cube>().ismatched)
        {
            Destroy(allcubes[y_Axis, x_Axis]);
            allcubes[y_Axis, x_Axis] = null;
        }
    }

    public void DestroyMatches()
    {
        for(int i = 0; i<width; i++)
        {
            for(int a = 0; a<height; a++)
            {
                if (allcubes[i, a] != null)
                {
                    DestroyAt(i, a);
                }
            }

        }
        StartCoroutine(DecreaseRow());
    }
   
    private IEnumerator DecreaseRow()
    {
        int nullcount = 0;
        for (int i =0; i<width; i++)
        {
            for(int a=0; a<height; a++)
            {
                if (allcubes[i, a] == null)
                {
                    nullcount++;
                }else if (nullcount > 0)
                {
                    allcubes[i, a].GetComponent<Cube>().x_Axis -= nullcount;
                    allcubes[i, a] = null;
                }
            }
            nullcount = 0;

        }
        yield return new WaitForSeconds(.4f);
        StartCoroutine(FillBoard());

    }

    private void Refill()
    {
        for(int i=0; i<width; i++)
        {
            for(int a=0; a<height; a++)
            {
                if (allcubes[i, a] == null)
                {
                    Vector2 tempPosition = new Vector2(i, a+offset);
                    int cube_use = UnityEngine.Random.Range(0, cubes.Length);
                    GameObject piece = Instantiate(cubes[cube_use], tempPosition, Quaternion.identity);
                    allcubes[i, a] = piece;
                    piece.GetComponent<Cube>().x_Axis = a;
                    piece.GetComponent<Cube>().y_Axis = i;
                    score = score + 5;
                    UnityEngine.Debug.Log("Score:" + score);
                    //if (HighScore <= score)
                    //{
                      //  HighScore = score;
                    //}

                }
            }
        }
    }

    private bool MatchesOnBoard()
    {
        for(int i=0; i<width; i++)
        {
            for(int a=0; a<height; a++)
            {
                if(allcubes[i,a]!= null)
                {
                    if (allcubes[i, a].GetComponent<Cube>().ismatched)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private IEnumerator FillBoard()
    {
        Refill();
        yield return new WaitForSeconds(.5f);

        while (MatchesOnBoard())
        {
            yield return new WaitForSeconds(.5f);
            DestroyMatches();
        }
    }

    void Update()
    {
       ScoreBoard.text = "S: "+score;
        

    }

}

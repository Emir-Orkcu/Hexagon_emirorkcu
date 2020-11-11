using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Random;

public class cubspawn : MonoBehaviour
{
    public GameObject[] cubes;
    public int width, height;
    public static GameObject[,] object_cubes;

    void Start()
    {
        object_cubes = new GameObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cube_make(x, y);
            }
        }

    }

    public void cube_make(int x, int y)
    {
        GameObject random_cube_object = random_cube_maker();
        GameObject new_cube = GameObject.Instantiate(random_cube_maker(), new Vector2(x, y + 10), Quaternion.identity);
        cubes cube = new_cube.GetComponent<cubes>();
        cube.renk = random_cube_object.name;
        cube.location(x, y);
        object_cubes[x, y] = new_cube;

    }

    public GameObject random_cube_maker()
    {
        int rand = UnityEngine.Random.Range(0, cubes.Length);
        return cubes[rand];
    }







    void Update()
    {

    }
}
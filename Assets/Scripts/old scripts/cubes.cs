using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
public class cubes : MonoBehaviour
{
    float x, y;
    bool dusus = true;
    GameObject choosenone;

    public static cubes first_choosen;
    public static cubes second_choosen;
    public Vector3 target_location;
    public bool variation = false;
    public List<cubes> cubes_x_axis;
    public List<cubes> cubes_y_axis;

    public string renk;




    void Start()
    {
        choosenone = GameObject.FindGameObjectWithTag("secim");
    }
    public void location(float _x, float _y)
    {
        x = _x;
        y = _y;



    }

    void Update()
    {
        if (dusus)
        {
            if (transform.position.y - y < 0.2f)
            {
                dusus = false;
                transform.position = new Vector3(x, y, 0);
            }

            transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, 0), Time.deltaTime * 2.5f);
        }
        if (variation)
        {
            changing();
        }
    }

    void OnMouseDown()
    {

        choosenone.transform.position = transform.position;
        cube_control();


    }

    void cube_control()
    {
        if (first_choosen == null)
        {
            first_choosen = this;
        }

        else
        {
            second_choosen = this;
            if (first_choosen != second_choosen)
            {
                float fark_X = Mathf.Abs(first_choosen.x - second_choosen.x);
                float fark_y = Mathf.Abs(first_choosen.y - second_choosen.y);

                if (fark_X + fark_y == 1)
                {
                    first_choosen.target_location = second_choosen.transform.position;
                    second_choosen.target_location = first_choosen.transform.position;

                    first_choosen.variation = true;
                    second_choosen.variation = true;

                    variable_changing();
                    first_choosen.x_axis_control();
                    first_choosen.y_axis_control();
                    second_choosen.x_axis_control();
                    second_choosen.y_axis_control();

                    //StartCoroutine(first_choosen.destroy_cubes());
                    //StartCoroutine(second_choosen.destroy_cubes());


                    first_choosen = null;


                }
                else
                {
                    first_choosen = second_choosen;

                }
            }



            second_choosen = null;



        }


    }

    void variable_changing()
    {
        second_choosen = cubspawn.object_cubes[(int)first_choosen.x, (int)first_choosen.y].GetComponent<cubes>();
        first_choosen = cubspawn.object_cubes[(int)second_choosen.x, (int)second_choosen.y].GetComponent<cubes>();


        float first_choosen_x = first_choosen.x;
        float first_choosen_y = first_choosen.y;

        first_choosen.x = second_choosen.x;
        first_choosen.y = second_choosen.y;

        second_choosen.x = first_choosen_x;
        second_choosen.y = first_choosen_y;


    }





    void changing()
    {

        transform.position = Vector3.Lerp(transform.position, target_location, 0.1f);

    }


    void x_axis_control()
    {
        for (int i = (int)x + 1; i <= cubspawn.object_cubes.GetLength(0); i++)
        {

           cubes cube_right = cubspawn.object_cubes[i, (int)y].GetComponent<cubes>();
            if (renk == cube_right.renk)
            {

                cubes_x_axis.Add(cube_right);

            }
            else
            {

                break;

            }


        }
        for (int i = (int)x - 1; i > 0; i--)
        {

            cubes cube_right = cubspawn.object_cubes[i, (int)y].GetComponent<cubes>();
            if (renk == cube_right.renk)
            {

                cubes_x_axis.Add(cube_right);

            }
            else
            {

                break;

            }


        }
    }

    void y_axis_control()
    {
        for (int i = (int)y + 1; i <= cubspawn.object_cubes.GetLength(1); i++)
        {

            cubes cube_right = cubspawn.object_cubes[(int)x, i].GetComponent<cubes>();
            if (renk == cube_right.renk)
            {

                cubes_x_axis.Add(cube_right);

            }
            else
            {

                break;

            }


        }
        for (int i = (int)y - 1; i >= 0; i--)
        {

            cubes cube_right = cubspawn.object_cubes[(int)x, i].GetComponent<cubes>();
            if (renk == cube_right.renk)
            {

                cubes_x_axis.Add(cube_right);

            }
            else
            {

                break;

            }


        }
    }
    IEnumerator destroy_cubes()
    {
        yield return new WaitForSeconds(0.3f);
        if (cubes_x_axis.Count > 2 || cubes_y_axis.Count > 2)
        {
            
            if (cubes_x_axis.Count > 2)
            {

                for (int i = 0; i < cubes_x_axis.Count; i++)
                {
                    //cubspawn.object_cubes[first_choosen.y].Remove(first_choosen.x + i,first_choosen.y);
                    //cubspawn.object_cubes[first_choosen.y].RemoveAt(first_choosen.x + i); 
                    //cubes_x_axis[i].gameObject.SetActive(false);
                    Destroy(cubes_x_axis[i].gameObject);

                }

            }
            else
            {

                foreach (var item in cubes_y_axis)
                {
                    //item.gameObject.SetActive(false);
                    Destroy(item.gameObject);
                }

            }
        }


    }
}

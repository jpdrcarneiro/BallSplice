using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLiteDatabase;

public class BallMovement : MonoBehaviour {

    public GameObject[] balls;

    public int number = 0;




    // Use this for initialization
    void Start () {
        balls = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject ball in balls)
        {
            ball.transform.localPosition = new Vector3(0, 0, 0);

            float strength = 200f;
            float x = strength * Random.Range(-1, 1);
            float y = strength * Random.Range(-1, 1);
            float z = 0f;
            Vector3 forceDirection = new Vector3(x, y, z);

            ball.GetComponent<Rigidbody>().AddForce(forceDirection, ForceMode.Force);
            ball.GetComponent<Rigidbody>().AddTorque(forceDirection);
        }


    }
	
	// Update is called once per frame
	void Update () {
        balls = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject ball in balls)
        {
            float position = ball.transform.position.z; 
            if (position != 0)
            {
                Vector3 zConstant = new Vector3(ball.transform.position.x, ball.transform.position.y, 0.00f);
                ball.transform.position = zConstant;
            }
        }    

	}
    private void OnCollisionEnter(Collision collision)
    {
        number = number +1;
       // Debug.Log(number);
        foreach(GameObject ball in balls)
        {

            float xSpeed = ball.GetComponent<Rigidbody>().velocity.x;
            float ySpeed = ball.GetComponent<Rigidbody>().velocity.y;
            float zSpeed = ball.GetComponent<Rigidbody>().velocity.z;

            float strength = 100f * number;
            float x = xSpeed + 5 *number * Time.deltaTime;
            if (x < 5f && x >0f)
            {
                x = x + 3;
            }else if (x <0f && x > -5f)
            {
                x = x - 3;
            }else if (x > 13f)
            {
                x = x - 3;
            }else if (x < -13f)
            {
                x = x + 3;
            }
            else if (x > 25f)
            {
                x = x - 15f;
            }
            else if (x < -25)
            {
                x = x + 15;
            }
            else
            {
                x = 10f;
            }

            float y = ySpeed + 5 * number * Time.deltaTime;
            if (y < 5f && y > 0f)
            {
                y = y + 3;
            }
            else if (y < 0f && y > -5f)
            {
                y = y - 3;
            }
            else if (y > 13f)
            {
                y = y - 3;
            }
            else if (y < -13f)
            {
                y = y + 3;
            }
            else if (y > 25f)
            {
                y = y - 15f;
            }
            else if (y < -25)
            {
                y = y + 15;
            }
            else
            {
                y = 10f;
            }

            float z = zSpeed;
            Vector3 forceDirection = new Vector3(x, y, z);
            Debug.Log(forceDirection);
            if (xSpeed < ySpeed)
            {
                ball.GetComponent<Rigidbody>().AddForce(100f*(Random.Range(1, -1)), 0f, 0f);
            }else
            {
                ball.GetComponent<Rigidbody>().AddForce(0f, 100f * (Random.Range(1, -1)), 0f);
            }

            ball.GetComponent<Rigidbody>().velocity = forceDirection;

           // ball.GetComponent<Rigidbody>().AddForce(forceDirection, ForceMode.Force);
           // ball.GetComponent<Rigidbody>().AddTorque(forceDirection);
        }
    }



    void hitMovement() {

    }

}

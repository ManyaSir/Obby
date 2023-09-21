using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{

    public GameObject player;
    public KeyCode forward;
    public KeyCode back;
    public KeyCode right;
    public KeyCode left;
    public KeyCode jump;
    public float speed;
    public float jumpForce;
    private float yCurrRot;
    private float defaultspeed;
    public static bool jumpagree = true;
    public static Collision coll;
    public float speedRotate;

    void Awake()
    {
        defaultspeed = speed;
    }
    void Update()
    {
        yCurrRot = player.transform.rotation.eulerAngles.y;
    }

    void FixedUpdate()
    {
        if (jumpagree && speed < defaultspeed)
        {
            speed += 100000;
            player.GetComponent<Rigidbody>().drag = 8f;
            player.GetComponent<Rigidbody>().mass = 8f;
        }
        if (player.GetComponent<Rigidbody>().velocity.magnitude <= 3f)
        {
            if (Input.GetKey(forward))
            {
                player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(0f, yCurrRot, 0f), speedRotate * Time.deltaTime);
                if (coll == null)
                {
                    jumpagree = false; speed -= 100000;
                    player.GetComponent<Rigidbody>().drag = 1f;
                    player.GetComponent<Rigidbody>().mass = 30f;
                }
                else { jumpagree = true; }
            }
            else if (Input.GetKey(back))
            {
                player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(0f, yCurrRot, 0f), speedRotate * Time.deltaTime);
                player.GetComponent<Rigidbody>().AddForce(-player.transform.forward * speed * Time.deltaTime);
                if (coll == null)
                {
                    jumpagree = false; speed -= 100000;
                    player.GetComponent<Rigidbody>().drag = 1f;
                    player.GetComponent<Rigidbody>().mass = 30f;
                }
                else { jumpagree = true; }
            }
            if (Input.GetKey(right))
            {
                player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(0f, yCurrRot, 0f), speedRotate * Time.deltaTime);
                player.GetComponent<Rigidbody>().AddForce(player.transform.right * speed * Time.deltaTime);
                if (coll == null)
                {
                    jumpagree = false; speed -= 100000;
                    player.GetComponent<Rigidbody>().drag = 1f;
                    player.GetComponent<Rigidbody>().mass = 30f;
                }
                else { jumpagree = true; }
            }
            else if (Input.GetKey(left))
            {
                player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(0f, yCurrRot, 0f), speedRotate * Time.deltaTime);
                player.GetComponent<Rigidbody>().AddForce(-player.transform.right * speed * Time.deltaTime);
                if (coll == null)
                {
                    jumpagree = false; speed -= 100000;
                    player.GetComponent<Rigidbody>().drag = 1f;
                    player.GetComponent<Rigidbody>().mass = 30f;
                }
                else { jumpagree = true; }
            }
            else if (Input.GetKey(jump) && jumpagree)
            {
                jumpagree = false;
                player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(0f, yCurrRot, 0f), speedRotate * Time.deltaTime);
                player.GetComponent<Rigidbody>().AddForce(Vector3.up * 3000);
                speed -= 100000;
                player.GetComponent<Rigidbody>().drag = 1f;
                player.GetComponent<Rigidbody>().mass = 30f;
            }
        }

        coll = null;
    }
}
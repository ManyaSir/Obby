using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    public GameObject player;
    public KeyCode forward;
    public KeyCode back;
    public KeyCode right;
    public KeyCode left;
    public KeyCode jump;
    public float speed;
    private float yCurrRot;
    private float defaultspeed;
    public static bool jumpagree = true;
    public static Collision coll;
    public float doubleClickTime = 0.2f;
    private float lastClickTime = -1.0f;
    public Transform target;
    public float distance = 3.0f;
    public float sensitivity = 2.0f;
    public float zoomSpeed = 2.0f;
    public float minDistance = 2.0f;
    public float maxDistance = 25.0f;
    public LayerMask obstacles;
    private float xAngle = 0.0f;
    private float yAngle = 0.0f;
    public float speedRotate;

    void Start()
    {
        defaultspeed = speed;
        Vector3 angles = transform.eulerAngles;
        xAngle = angles.y;
        yAngle = angles.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.timeSinceLevelLoad - lastClickTime < doubleClickTime)
            {
                if (Cursor.lockState == CursorLockMode.Locked)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
            lastClickTime = Time.timeSinceLevelLoad;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance = Mathf.Clamp(distance - scroll * zoomSpeed, minDistance, maxDistance);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            xAngle += mouseX;
            yAngle -= mouseY;
            yAngle = Mathf.Clamp(yAngle, -90.0f, 90.0f);
        }

        Quaternion rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + target.position;

        RaycastHit hit;
        if (Physics.Raycast(target.position, position - target.position, out hit, distance, obstacles))
        {
            position = hit.point;
        }
        yCurrRot = transform.eulerAngles.y;
        transform.rotation = rotation;
        transform.position = new Vector3(position.x, position.y+4f, position.z);
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
                if (coll == null){ jumpagree = false;                 speed -= 100000;
                player.GetComponent<Rigidbody>().drag = 1f;
                player.GetComponent<Rigidbody>().mass = 30f;}else{jumpagree = true;}
            }
            else if (Input.GetKey(back))
            {
                player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(0f, yCurrRot, 0f), speedRotate * Time.deltaTime);
                player.GetComponent<Rigidbody>().AddForce(-player.transform.forward * speed * Time.deltaTime);
                if (coll == null){ jumpagree = false;                 speed -= 100000;
                player.GetComponent<Rigidbody>().drag = 1f;
                player.GetComponent<Rigidbody>().mass = 30f;}else{jumpagree = true;}
            }
            if (Input.GetKey(right))
            {
                player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(0f, yCurrRot, 0f), speedRotate * Time.deltaTime);
                player.GetComponent<Rigidbody>().AddForce(player.transform.right * speed * Time.deltaTime);
                if (coll == null){ jumpagree = false;                 speed -= 100000;
                player.GetComponent<Rigidbody>().drag = 1f;
                player.GetComponent<Rigidbody>().mass = 30f;}else{jumpagree = true;}
            }
            else if (Input.GetKey(left))
            {
                player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(0f, yCurrRot, 0f), speedRotate * Time.deltaTime);
                player.GetComponent<Rigidbody>().AddForce(-player.transform.right * speed * Time.deltaTime);
                if (coll == null){ jumpagree = false;                 speed -= 100000;
                player.GetComponent<Rigidbody>().drag = 1f;
                player.GetComponent<Rigidbody>().mass = 30f;}else{jumpagree = true;}
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

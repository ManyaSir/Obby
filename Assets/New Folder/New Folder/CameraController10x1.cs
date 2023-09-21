using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController10x1 : MonoBehaviour
{
    public float doubleClickTime = 0.2f; // �����, � ������� �������� ������ ��������� ������� ����
    private float lastClickTime = -1.0f; // ����� ���������� �����
    public Transform target; // Transform �������, ������ �������� ������ ��������� ������
    public float distance = 3.0f; // ���������� �� ������ �� �������
    public float sensitivity = 2.0f; // ���������������� ���� ��� �������� ������
    public float zoomSpeed = 2.0f; // �������� �����������/��������� ������
    public float minDistance = 2.0f; // ����������� ���������� �� ������ �� �������
    public float maxDistance = 25.0f; // ������������ ���������� �� ������ �� �������
    public LayerMask obstacles; // ���� ��������, ����� ������� ������ �� ����� ������

    private float xAngle = 0.0f; // ���� �������� ������ �� �����������.
    private float yAngle = 0.0f; // ���� �������� ������ �� ���������
    private Transform Check;
    public GameObject player;
    public KeyCode forward;
    public KeyCode back;
    public KeyCode right;
    public KeyCode left;
    public KeyCode jump;
    public float speed;
    private bool jumpagree = true;
    public GameObject camera;
    public float yR;

    // ��� ������� ������� ������ ��������� ���� �������� ������ � ��������� ������
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        xAngle = angles.y;
        yAngle = angles.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Check = player.transform;
    }

    // ��������� ������� � ���������� ������ ������ ����.
    void LateUpdate()
    {

        // ��������� �� ������� ������� ����� ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.timeSinceLevelLoad - lastClickTime < doubleClickTime)
            {
                // ������� ���� - ������ ��������� �������
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

        // �����������/��������� ������ � ������� �������� ����
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance = Mathf.Clamp(distance - scroll * zoomSpeed, minDistance, maxDistance);

        // �������� ������ � ������� ������
        if (Input.GetKey(KeyCode.Mouse1))
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            xAngle += mouseX;
            yAngle -= mouseY;
            yAngle = Mathf.Clamp(yAngle, -90.0f, 90.0f);
        }

        // ������������ ����� ������� � ���������� ������
        Quaternion rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + target.position;

        // ���������, �� ��������� �� ������ ������ ��������
        RaycastHit hit;
        if (Physics.Raycast(target.position, position - target.position, out hit, distance, obstacles))
        {
            position = hit.point;
        }

        // ��������� ������� � ���������� ������
        transform.rotation = rotation;
        transform.position = position;
        yR = transform.rotation.y;
    }

    void Update()
    {
        if (player.GetComponent<Rigidbody>().velocity.magnitude <= 3f)
        {
            if (Input.GetKey(forward))
            {
                player.transform.rotation = Quaternion.Euler(0f, yR, 0f);
                player.GetComponent<Rigidbody>().AddForce(player.transform.forward * speed * Time.deltaTime);
            }
            else if (Input.GetKey(back))
            {
                player.transform.rotation = Quaternion.Euler(0f, transform.rotation.y, 0f);
                player.GetComponent<Rigidbody>().AddForce(-player.transform.forward * speed * Time.deltaTime);
            }
            if (Input.GetKey(right))
            {
                player.transform.rotation = Quaternion.Euler(0f, transform.rotation.y, 0f);
                player.GetComponent<Rigidbody>().AddForce(player.transform.right * speed * Time.deltaTime);
            }
            else if (Input.GetKey(left))
            {
                player.transform.rotation = Quaternion.Euler(0f, transform.rotation.y, 0f);
                player.GetComponent<Rigidbody>().AddForce(-player.transform.right * speed * Time.deltaTime);
            }
            if (Input.GetKey(jump) && jumpagree)
            {
                jumpagree = false;
                player.transform.rotation = Quaternion.Euler(0f, transform.rotation.y, 0f);
                player.GetComponent<Rigidbody>().AddForce(Vector3.up * 1800);
                speed -= 200;
                player.GetComponent<Rigidbody>().drag = 0f;
                player.GetComponent<Rigidbody>().mass = 15f;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        jumpagree = true;
        speed += 200;
        player.GetComponent<Rigidbody>().drag = 7f;
        player.GetComponent<Rigidbody>().mass = 8f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            Check.position = other.gameObject.transform.position;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Obstacles")
        {
            player.transform.position = Check.position;
        }
    }
}
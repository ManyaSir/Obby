using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HideObjects : MonoBehaviour
{
    public GameObject[] alwaysHiddenObjects;
    public GameObject[] distanceDependentObjects;
    public float hidingDistance = 50f;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        foreach (GameObject obj in alwaysHiddenObjects)
        {
            obj.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void Update()
    {
        foreach (GameObject obj in distanceDependentObjects)
        {
            if (Vector3.Distance(obj.transform.position, playerTransform.position) <= hidingDistance)
            {
                obj.SetActive(true);
            }
            else
            {
                obj.SetActive(false);
            }
        }
    }
}

// ������ ������������� �������:

// 1. �������� ������ ������ �� ����� � �������� ���� ������ ��� ���������.
// 2. ���������� � ������ alwaysHiddenObjects ��� �� �������, ������� ������ ���� ������ ������.
// 3. ���������� � ������ distanceDependentObjects ��� �� �������, ������� ������ ����������/���������� � ����������� �� ���������� �� ������.
// 4. ��������� �������� hidingDistance � ������������ � ������ �������������.
// 5. ��������� ����� � ��������� ������ �������.
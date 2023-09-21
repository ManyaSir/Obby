using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class grnd : MonoBehaviour
{
    public Transform teleportLocation; // �������� ����� ��� ������������ ������

    void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("Player") && other.CompareTag("Obstacles"))
        {
            // ��������������� ������ � �������� �����
            this.transform.position = teleportLocation.position;
            this.transform.rotation = teleportLocation.rotation;

            // ����� ����� �������� �������������� �������� ����� ������������

            // ������ ������������ ����� ����� ������������
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
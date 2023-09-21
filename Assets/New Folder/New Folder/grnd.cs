using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class grnd : MonoBehaviour
{
    public Transform teleportLocation; // заданное место дл€ телепортации игрока

    void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("Player") && other.CompareTag("Obstacles"))
        {
            // “елепортировать игрока в заданное место
            this.transform.position = teleportLocation.position;
            this.transform.rotation = teleportLocation.rotation;

            // ћожно также добавить дополнительные действи€ после телепортации

            // ѕример перезагрузки сцены после телепортации
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
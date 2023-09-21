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

// Пример использования скрипта:

// 1. Создайте пустой объект на сцене и добавьте этот скрипт как компонент.
// 2. Перетащите в список alwaysHiddenObjects все те объекты, которые должны быть всегда скрыты.
// 3. Перетащите в список distanceDependentObjects все те объекты, которые должны появляться/скрываться в зависимости от расстояния до игрока.
// 4. Настройте значение hidingDistance в соответствии с вашими потребностями.
// 5. Запустите сцену и проверьте работу скрипта.
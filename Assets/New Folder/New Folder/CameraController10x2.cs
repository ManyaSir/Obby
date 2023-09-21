using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController10x2 : MonoBehaviour
{
    public float doubleClickTime = 0.2f; // Время, в течение которого должен произойти двойной клик
    private float lastClickTime = -1.0f; // Время последнего клика
    public Transform target; // Transform объекта, вокруг которого должна крутиться камера
    public float distance = 3.0f; // Расстояние от камеры до объекта
    public float sensitivity = 2.0f; // Чувствительность мыши для вращения камеры
    public float zoomSpeed = 2.0f; // Скорость приближения/отдаления камеры
    public float minDistance = 2.0f; // Минимальное расстояние от камеры до объекта
    public float maxDistance = 25.0f; // Максимальное расстояние от камеры до объекта
    public LayerMask obstacles; // Слои объектов, через которые камера не может пройти

    private float xAngle = 0.0f; // Угол вращения камеры по горизонтали.
    private float yAngle = 0.0f; // Угол вращения камеры по вертикали

    // При запуске скрипта задаем начальный угол вращения камеры и фиксируем курсор
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        xAngle = angles.y;
        yAngle = angles.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Обновляем позицию и ориентацию камеры каждый кадр.
    void LateUpdate()
    {

        // Проверяем на двойное нажатие левой кнопки мыши
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.timeSinceLevelLoad - lastClickTime < doubleClickTime)
            {
                // Двойной клик - меняем состояние курсора
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

        // Приближение/отдаление камеры с помощью колесика мыши
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance = Mathf.Clamp(distance - scroll * zoomSpeed, minDistance, maxDistance);

        // Вращение камеры с помощью клавиш
        if (Input.GetKey(KeyCode.Mouse1))
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            xAngle += mouseX;
            yAngle -= mouseY;
            yAngle = Mathf.Clamp(yAngle, -90.0f, 90.0f);
        }

        // Рассчитываем новую позицию и ориентацию камеры
        Quaternion rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + target.position;

        // Проверяем, не находится ли камера внутри объектов
        RaycastHit hit;
        if (Physics.Raycast(target.position, position - target.position, out hit, distance, obstacles))
        {
            position = hit.point;
        }

        // Обновляем позицию и ориентацию камеры
        transform.rotation = rotation;
        transform.position = position;
    }
}
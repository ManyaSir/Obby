using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playercs4 : MonoBehaviour
{
    // Клавиши на клавиатуре.
    public KeyCode upArrow;
    public KeyCode downArrow;
    public KeyCode rightArrow;
    public KeyCode leftArrow;
    public KeyCode jumpKey;
    public KeyCode restartKey;
    public KeyCode previousSceneKey;
    public KeyCode nextSceneKey;

    // Векторы движения для перемещения по вертикали, горизонтали и для прыжка.
    public Vector3 moveVectorVertical = new Vector3(0, 0, 1);
    public Vector3 moveVectorHorizontal = new Vector3(1, 0, 0);
    public Vector3 jumpVector = new Vector3(0, 1, 0);

    private Rigidbody rb;
    private Collider col;

    private void Start()
    {
        // Получаем компонент Rigidbody объекта
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            // Проверяем, нажата ли клавиша для перемещения вверх и добавляем соответствующий вектор скорости
            if (Input.GetKey(upArrow)) { rb.velocity += moveVectorVertical; }

            // Проверяем, нажата ли клавиша для перемещения вниз и вычитаем соответствующий вектор скорости
            if (Input.GetKey(downArrow)) { rb.velocity -= moveVectorVertical; }

            // Проверяем, нажата ли клавиша для перемещения вправо и добавляем соответствующий вектор скорости
            if (Input.GetKey(rightArrow)) { rb.velocity += moveVectorHorizontal; }

            // Проверяем, нажата ли клавиша для перемещения влево и вычитаем соответствующий вектор скорости
            if (Input.GetKey(leftArrow)) { rb.velocity -= moveVectorHorizontal; }

            // Проверяем, нажата ли клавиша для прыжка
            if (Input.GetKey(jumpKey))
            {
                // Проверяем, на земле ли объект перед прыжком
                if (IsGrounded())
                {
                    // Присваиваем нужный вектор скорости для прыжка
                    rb.velocity = jumpVector;
                }
            }
        }

        if (Input.GetKey(restartKey)) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }

        if (Input.GetKey(previousSceneKey)) { if (SceneManager.GetActiveScene().buildIndex > 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); }

        if (Input.GetKey(nextSceneKey)) { if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
    }

    private void Awake()
    {
        if (SceneManager.sceneCountInBuildSettings <= 0) Debug.LogError("No scene in build settings");

        // Проверяем, заданы ли корректные KeyCode
        if (upArrow == KeyCode.None || downArrow == KeyCode.None || rightArrow == KeyCode.None || leftArrow == KeyCode.None || jumpKey == KeyCode.None || restartKey == KeyCode.None || previousSceneKey == KeyCode.None || nextSceneKey == KeyCode.None)
        {
            Debug.LogError("KeyCode not assigned");
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Проверяем, есть ли предустановленные сцены в настройках сборки
        if (SceneManager.sceneCountInBuildSettings <= 0) Debug.LogError("No scene in build settings");
    }

    private bool IsGrounded()
    {
        if (col == null)
        {
            Debug.LogError("Collider not found on object");
            return false;
        }

        return Physics.Raycast(transform.position, Vector3.down, col.bounds.extents.y + 0.1f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("Player") && other.CompareTag("end"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
//            if (Input.GetKey(previousSceneKey )) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); }
//            if (Input.GetKey(nextSceneKey)) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
        }
    }
}

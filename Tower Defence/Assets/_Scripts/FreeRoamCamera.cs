using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FreeRoamCamera : MonoBehaviour
{
    public float sensitivity = 100f; // Camera sensitivity
    public float speed = 10f; // Movement speed

    private float rotationX = 0f; // X rotation
    private float rotationY = 0f; // Y rotation

    // Get the camera component and URP settings
    private Camera cam;

    bool canMove = true;


  

    #region Subscriptions
    private void OnEnable()
    {
        Actions.OnPlayerClickNoDragging += CanMove;

    }
    private void OnDisable()
    {
        Actions.OnPlayerClickNoDragging -= CanMove;

    }
    #endregion

    private void Start()
    {
        // Get the camera and URP settings
        cam = GetComponent<Camera>();


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rotationX = transform.rotation.x;
        rotationY = transform.rotation.y;
    }




    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Mouse1))
            canMove = !canMove;

        if (!canMove)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;



            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                canMove = true;

            return; }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Rotate the camera based on mouse input
        rotationX += Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        rotationY += Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;
       
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0f);

        // Move the camera based on keyboard input
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction -= transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction -= transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += transform.right;
        }

        transform.position += direction.normalized * speed * Time.deltaTime;
       
    }


    void CanMove()
    {
        canMove = true;
    }
}

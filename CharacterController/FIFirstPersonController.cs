using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIFirstPersonController : MonoBehaviour, FIInterfaceCharacterController {

    public Camera Cam;
    public GameObject CameraPlaceholder;

    [Header("Options")]
    public float speed = 5f;
    public float sensitivityX;
    public float sensitivityY;
    public float MinAngle;
    public float MaxAngle;

    FICameraController m_camController;
    float rotateXAccumulation;
    Rigidbody rb;

    private void Start()
    {
        m_camController = new FICameraController(Cam, CameraPlaceholder);
        m_camController.Init();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        m_camController.Update();
        Move();
    }

    void Move()
    {
        MoveCharacter();
        CameraRotationBasedOnMouse();
    }

    void CameraRotationBasedOnMouse()
    {
        float rotateX = Input.GetAxisRaw("Mouse Y");
        float nextRotationAccumulation = rotateXAccumulation + rotateX * -1 * sensitivityX;
        if (nextRotationAccumulation < MinAngle && nextRotationAccumulation > MaxAngle * -1)
        {
            rotateXAccumulation = nextRotationAccumulation;
            Cam.transform.Rotate(new Vector3(rotateX * -1, 0, 0) * sensitivityX);
        }
        float rotateY = Input.GetAxisRaw("Mouse X");
        rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(0, rotateY, 0) * sensitivityY));
    }

    void MoveCharacter()
    {
        Vector3 currentPosition = gameObject.transform.position;
        Vector3 _moveForward = gameObject.transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 _moveSide = gameObject.transform.right * Input.GetAxisRaw("Horizontal");
        Vector3 _move = (_moveForward.normalized + _moveSide).normalized * speed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + _move);
    }

    

    void LookAt(Vector3 look)
    {

    }

}

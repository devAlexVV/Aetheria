using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody _rb;

    #region
    private Camera _cam;
    private CameraMovement _cm;
    private Vector3 camFwd;
    #endregion

    #region
    [Range(1.0f, 10.0f)]
    public float run_speed;
    [Range(1.0f, 10.0f)]
    public float backwards_run_speed;
    [Range(1.0f, 10.0f)]
    public float strafe_speed;
    [Range(1.0f, 10.0f)]
    public float rotation_speed;
    [Range(1.0f, 10.0f)]
    public float jump_force;
    #endregion

    #region Animations
    private MyTPCharacter tpc;
    private bool combatRunning = false;
    private bool combatStrafeLeft = false;
    private bool combatStrafeRight = false;
    private bool combatBackwards = false;
    private bool jump = false;
    #endregion

    private void Awake()
    {
        tpc = FindAnyObjectByType<MyTPCharacter>();
        _cm = GetComponent<CameraMovement>();
        _cam = _cm.getCamera();
        _rb = GetComponent <Rigidbody>();

    }

    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        jump = Input.GetButtonDown("Jump");


        //calcular la direccion donde la camara esta viendo
        camFwd = Vector3.Scale(_cam.transform.forward, new Vector3(1, 1, 1)).normalized;
        Vector3 camFlatFwd = Vector3.Scale(_cam.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 flatRight = new Vector3(_cam.transform.right.x, 0, _cam.transform.right.z);

        Vector3 m_CharForward = Vector3.Scale(camFlatFwd, new Vector3(1, 0, 1)).normalized;
        Vector3 m_charRight = Vector3.Scale(flatRight, new Vector3(1, 0, 1)).normalized;

        float r_speed;
        Vector3 move = Vector3.zero;
        if (_cm.type == CameraMovement.CAMERA_TYPE.LOCKED)
        {
            r_speed = (v > 0) ? run_speed : backwards_run_speed;
            move = v * m_CharForward * run_speed + h * m_charRight * strafe_speed;
        }

        transform.position += move * Time.deltaTime;

        if (jump)
        {
            _rb.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
        }

        //actualizar animaciones
        if (_cm.type == CameraMovement.CAMERA_TYPE.LOCKED)
        {
            combatRunning = (v > 0 && h == 0);
            combatBackwards = (v < 0 && h == 0);
            combatStrafeLeft = (h < 0);
            combatStrafeRight = (h > 0);
        }

    }

    void Update()
    {

        PlayerStateMachine();

        tpc.GetBodyAnimator().SetBool("combatRunning", combatRunning);
        tpc.GetBodyAnimator().SetBool("strafeRight", combatStrafeRight);
        tpc.GetBodyAnimator().SetBool("strafeLeft", combatStrafeLeft);
        tpc.GetBodyAnimator().SetBool("backwards", combatBackwards);
        tpc.GetBodyAnimator().SetBool("jump", jump);

    }

    void PlayerStateMachine()
    {
    }
}

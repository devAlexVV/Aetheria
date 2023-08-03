using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEditor;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Camera _cam;

    public enum CAMERA_TYPE {LOCKED}

    public CAMERA_TYPE type = CAMERA_TYPE.LOCKED;

    [SerializeField]
    [Range(0.1f, 2.0f)]
    private float sensitivity;
    [SerializeField]
    private bool invertXAxis;
    [SerializeField]
    private bool invertYAxis;

    public Transform lookAt;


    private void Awake()
    {
        if (type == CAMERA_TYPE.LOCKED)
        {
            _cam.transform.parent = transform;
        }
    }


    private void FixedUpdate()
    {
        // leer input
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        // configuracion
        h = (invertXAxis) ? (-h) : h;
        v = (invertYAxis) ? (-v) : v;

        if (h!=0)
            //transformar si la cam esta rotando en Horizontal que cambie la direccion en la que ve el jugador
        { if (type == CAMERA_TYPE.LOCKED) transform.Rotate(Vector3.up, h * 90 * sensitivity * Time.deltaTime);
        
        }

        if (v != 0)
            //mover la camara en vertical
        {
            _cam.transform.RotateAround(transform.position, transform.right, v * 90 * sensitivity * Time.deltaTime);
        }
        //hacer que la cam mire hacia el jugador
        _cam.transform.LookAt(lookAt);

    }

    internal Camera getCamera()
    {
        return _cam ? _cam : Camera.main;
    }
}

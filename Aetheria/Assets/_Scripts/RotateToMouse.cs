using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    public Camera cam;
    public float maxLenght;

    private Ray rayMouse;
    private Vector3 pos;
    private Vector3 direction;
    private Quaternion rotation;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cam != null) {
            RaycastHit hit;
            var mousePos = Input.mousePosition;
            rayMouse = cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit, maxLenght))
            {
                rotateToMouseDirection(gameObject, hit.point);
            }
            else {
                var pos = rayMouse.GetPoint(maxLenght);
                rotateToMouseDirection(gameObject, pos);
            }

        }
        else
        {
            Debug.Log("No Camera");
        }
    }

    void rotateToMouseDirection(GameObject obj, Vector3 destination) {
        direction = destination - obj.transform.position;
        rotation = Quaternion.LookRotation(direction);
        Quaternion offset = Quaternion.Euler(0f, -90f, 0f);
        rotation *= offset;

        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }

    public Quaternion getRotation() {
        Quaternion offset = Quaternion.Euler(0f, 90f, 0f);
        rotation *= offset;
        return rotation;
    }
}

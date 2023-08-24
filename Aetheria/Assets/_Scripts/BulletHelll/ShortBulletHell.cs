using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.Pool;
public class ShortBulletHell : MonoBehaviour
{
    public GameObject firepoint;
    public List<GameObject> vfx = new List<GameObject>();
    public int amountBullets = 4;
    private GameObject effectToSpawn;
    private float timeTofire = 0;
    [SerializeField]
    public int proyectileType = 0;
    private ObjectPool<GameObject> _proyectilPool;

    private float timeSinceLast = 0f;
    public float fireRate = 0.5f;

    void Start()
    {
        effectToSpawn = vfx[proyectileType];
        _proyectilPool = new ObjectPool<GameObject>(() => {
            return Instantiate(effectToSpawn, firepoint.transform.position, Quaternion.identity);
        }, proyectil => {
            proyectil.gameObject.SetActive(true);
        }, proyectil => {
            proyectil.gameObject.SetActive(false);
        }, proyectil => {
            Destroy(effectToSpawn.gameObject);
        }, false, 50, 100);
    }

    // Update is called once per frame
    void Update()
    {

        timeSinceLast += Time.deltaTime;

        if (timeSinceLast >= fireRate)
        {
            Debug.Log("HELLO");
            spawnVfx();
            timeSinceLast = 0f;
        }

            //timeTofire = Time.time + 1 / vfx[proyectileType].GetComponent<ProyectileMove>().fireRate;
    }

    void spawnVfx()
    {

        if (firepoint != null)
        {

            int anglesRotation = Mathf.RoundToInt(360 / amountBullets);
            int shootingAngle = 0;
            for (int i = 0; i < amountBullets; i++)
            {
                
                Quaternion rotationMod = Quaternion.Euler(0f, shootingAngle, 0);
                spawnBullet(rotationMod);
                shootingAngle += anglesRotation;

            }

        }
        else
        {
            Debug.Log("No Firepoint");
        }

    }

    void spawnBullet(Quaternion rotationModifier)
    {

        GameObject vfx;
        vfx = _proyectilPool != null ? _proyectilPool.Get() : Instantiate(effectToSpawn, firepoint.transform.position, Quaternion.identity);
        Quaternion rotation = transform.rotation * rotationModifier;
        vfx.GetComponent<ProyectileMove>().direccion = (rotation * new Vector3(1, 0, 0)).normalized;
        /*
        if (rotateToMouse != null)
        {
        // vfx.transform.localRotation = rotateToMouse.getRotation();
         //   vfx.transform.localRotation = firepoint.transform.rotation;
        }
        */
        vfx.GetComponent<ProyectileMove>().Init(destroyBullet);
    }

    private void destroyBullet(ProyectileMove bullet)
    {
        if (_proyectilPool != null)
        {
            bullet.gameObject.transform.position = firepoint.transform.position;
            bullet.gameObject.transform.rotation = effectToSpawn.transform.rotation;
            bullet.gameObject.transform.localRotation = effectToSpawn.transform.localRotation;

            bullet.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            bullet.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, 0f);
            bullet.speed = effectToSpawn.GetComponent<ProyectileMove>().speed;
            _proyectilPool.Release(bullet.gameObject);
        }
        else
        {
            Destroy(bullet.gameObject);
        }

    }
}

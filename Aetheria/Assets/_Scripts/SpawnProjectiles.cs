using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnProjectiles : MonoBehaviour
{
    public GameObject firepoint;
    public List<GameObject> vfx = new List<GameObject>();

    private GameObject effectToSpawn;
    public RotateToMouse rotateToMouse;
    private float timeTofire = 0;
    [SerializeField]
    public int proyectileType = 0;
    private ObjectPool<GameObject> _proyectilPool;
    void Start()
    {
        effectToSpawn = vfx[proyectileType];
        _proyectilPool = new ObjectPool<GameObject>(() => {
            return Instantiate(effectToSpawn, firepoint.transform.position, Quaternion.identity);
        },proyectil => {
            proyectil.gameObject.SetActive(true);
        }, proyectil => {
            proyectil.gameObject.SetActive(false);
        }, proyectil => {
            Destroy(effectToSpawn.gameObject);
        },false,50,100);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(firepoint.transform.position);
        if (Input.GetMouseButton(0) && Time.time >= timeTofire) {
            timeTofire = Time.time + 1 / vfx[proyectileType].GetComponent<ProyectileMove>().fireRate;
            spawnVfx();
        }
    }

    void spawnVfx() {
        GameObject vfx;
        if (firepoint != null)
        {
            vfx = _proyectilPool != null? _proyectilPool.Get(): Instantiate(effectToSpawn, firepoint.transform.position, Quaternion.identity);
            vfx.transform.localRotation = Quaternion.identity;
            if (rotateToMouse != null) {
                vfx.transform.localRotation = rotateToMouse.getRotation();
            }
            vfx.GetComponent<ProyectileMove>().Init(destroyBullet);
        }
        else {
            Debug.Log("No Firepoint");
        }
        
    }

    private void destroyBullet(ProyectileMove bullet) {
        if (_proyectilPool != null)
        {
            bullet.gameObject.transform.position = firepoint.transform.position;
            bullet.gameObject.transform.rotation = effectToSpawn.transform.rotation;
            bullet.gameObject.transform.localRotation = effectToSpawn.transform.localRotation;
            bullet.speed = effectToSpawn.GetComponent<ProyectileMove>().speed;
            _proyectilPool.Release(bullet.gameObject);
        }
        else {
            Destroy(bullet.gameObject);
        }
        
    }
}

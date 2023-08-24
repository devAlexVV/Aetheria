using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProyectileMove : MonoBehaviour
{
    private Action<ProyectileMove> _destroyAction;
    public float speed;
    public float fireRate;
    public GameObject muzzlePrefab;
    public GameObject hitPrefab;
    public Vector3 direccion;
    public float bulletLifeTime = 5;
    float timeSinceShot = 0f;
    bool bulletShoot = false;
    private void Awake()
    {
        direccion = transform.forward;
    }
    public void Init(Action<ProyectileMove> destroyAction)
    {
        _destroyAction = destroyAction;
    }
    void Start()
    {
        if (muzzlePrefab != null) {
            var muzzleVFX = Instantiate(muzzlePrefab, transform.position,Quaternion.identity);
            muzzleVFX.transform.forward = gameObject.transform.forward;
            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
            if (psMuzzle != null) {
                Destroy(muzzleVFX, psMuzzle.main.duration);
            }
            else{
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }
    }

    void Update()
    {

        bulletShoot = true;
        timeSinceShot += Time.deltaTime;
        if (speed != 0) {
            transform.position += direccion * (speed*Time.deltaTime);
        }
        else
        {
            Debug.Log("No Speed");
        }

        if (!gameObject.activeSelf) {
            timeSinceShot = 0;
        }
        
        if (timeSinceShot >= bulletLifeTime) { 
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
      

        ContactPoint contact = other.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if (hitPrefab != null) {
            var hitVFX = Instantiate(hitPrefab, pos,rot);
            var psMuzzle = hitVFX.GetComponent<ParticleSystem>();
            if (psMuzzle != null)
            {
                Destroy(hitVFX, psMuzzle.main.duration);
            }
            else
            {
                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVFX, psChild.main.duration);
            }

        }

        if (!other.gameObject.CompareTag("nonColision")) {
            // Destroy(gameObject);
            /*
            gameObject.transform.position = originalTransform.position;
            gameObject.transform.rotation = originalTransform.rotation;
            gameObject.transform.localScale = originalTransform.localScale;
            */
            _destroyAction(this);
        }
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    float health = 100.0F;

    [SerializeField]
    public Image LifeBar = null;
    private float ActualLife;
    public float MaximumLife;

    private void Start()
    {
        LifeBar.type = Image.Type.Filled;
    }
    void Update()
    {
        checkLifeBar();
        if (Input.GetKeyDown(KeyCode.V)) {
            TakeDamage(10);
        }
        
    }

    void checkLifeBar() {
        if (LifeBar != null)
        {
            ActualLife = health;
            LifeBar.fillAmount = ActualLife / MaximumLife;
        }
    }

    //public void ReduceLife(float damageAmount)
    //{
    // ActualLife -= damageAmount;
    // ActualLife = Mathf.Clamp(ActualLife, 0f, MaximumLife); 
    //}

    public void TakeDamage(float damage)
    {
        health -= Mathf.Abs(damage);
        ActualLife = Mathf.Clamp(ActualLife, 0f, MaximumLife);
        if (health <= 0)
        {
            checkLifeBar();
            Destroy(gameObject);
        }
    }
}

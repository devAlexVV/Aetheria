using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    public Image LifeBar;
    private float ActualLife;
    public float MaximumLife;


    void Update()
    {
        LifeBar.fillAmount = ActualLife / MaximumLife;
        
    }

    //public void ReduceLife(float damageAmount)
    //{
       // ActualLife -= damageAmount;
       // ActualLife = Mathf.Clamp(ActualLife, 0f, MaximumLife); 
    //}
}

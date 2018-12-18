using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    public float time;
    private void OnEnable()
    {
        Invoke("Actice", time);
    }
    private void Actice()
    {
        gameObject.SetActive(false);
    }
}


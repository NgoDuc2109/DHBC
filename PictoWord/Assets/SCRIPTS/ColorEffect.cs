using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorEffect : MonoBehaviour {

    public static ColorEffect instance;

    private void Awake()
    {
        instance = this;
    }

    public void changColor()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Share : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SHARE()
    {
        ShareRateAds.shareRateAds.Share();
    }
    public void Rate()
    {
        ShareRateAds.shareRateAds.Rate();
    }
}


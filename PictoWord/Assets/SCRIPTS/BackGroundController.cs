using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackGroundController : MonoBehaviour {
    public Image backGroud;
    public Image WIN;
    public Sprite[] BG;
    public Sprite[] BGWIN;
    public GameObject Character, Answer;
    int n;
	void Start () {
        n = Random.Range(0, BG.Length);
        backGroud.sprite = BG[n];
        WIN.sprite = BGWIN[n];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

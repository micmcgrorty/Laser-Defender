using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

	[SerializeField] float backgroundScrollSpeed = 0.5f;
	Material backgroundMaterial;
	Vector2 offset;

	// Use this for initialization
	void Start () {
		backgroundMaterial = GetComponent<Renderer>().material;
		offset = new Vector2(0f, backgroundScrollSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		backgroundMaterial.mainTextureOffset += offset * Time.deltaTime;
	}
}

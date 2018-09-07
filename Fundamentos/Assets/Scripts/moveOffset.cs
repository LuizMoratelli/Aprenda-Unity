using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveOffset : MonoBehaviour {

	private MeshRenderer _meshRenderer;
    private Material _material;
    private Texture _texture;

    private float offsetX;
    private float offsetY;
    private float divisor = 2000;
    public float offsetIncrementX = 1;
    public float offsetIncrementY = 0;
    public string sortingLayer = "Background";
    public int orderInLayer = 0;

	void Start () {
		_meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.sortingLayerName = sortingLayer;
        _meshRenderer.sortingOrder = orderInLayer;

        _material = _meshRenderer.material;

        divisor = 1/divisor;
	}
	
	void FixedUpdate () {
        offsetX += (offsetIncrementX * divisor);
        offsetY += (offsetIncrementY * divisor);
        _material.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
	}
}

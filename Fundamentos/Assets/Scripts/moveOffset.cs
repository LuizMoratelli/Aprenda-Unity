using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveOffset : MonoBehaviour {

	private MeshRenderer _meshRenderer;
    private Material _material;
    private Texture _texture;

    private float offset;
    private float divisor = 1000;
    public float offsetIncrement = 1;
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
        offset += (offsetIncrement * divisor);
        _material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
	}
}

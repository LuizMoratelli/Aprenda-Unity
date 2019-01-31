using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOffset : MonoBehaviour
{
    #region Sub-Classes and Structs
	#endregion

	#region Public Members
	#endregion

	#region Private Members
    private Material material;
    private float offset;

    [SerializeField] private float velocidade;
    [SerializeField] private string sortingLayer;
    [SerializeField] private int sortingOrder;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #region Unity Default Methods
    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.sortingLayerName = sortingLayer;
        renderer.sortingOrder = sortingOrder;
        material = renderer.material;
    }

    private void Update()
    {
        offset += 0.001f;
        material.SetTextureOffset("_MainTex", new Vector2(offset * velocidade, 0));
    }
    #endregion
    #endregion
}

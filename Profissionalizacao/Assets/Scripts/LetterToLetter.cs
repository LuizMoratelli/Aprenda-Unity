using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterToLetter : MonoBehaviour
{
    #region Sub-Classes and Structs
	#endregion

	#region Public Members
	#endregion

	#region Private Members
    [SerializeField] private Text campoTexto;
    [SerializeField] private string[] frases;
    [Range(0, 1)] [SerializeField] private float delayBtwLetters = 0.1f;
    [Range(0, 5)] [SerializeField] private float delayBtwFrases = 0.5f;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #region Unity Default Methods
    private void Start()
    {
        StartCoroutine("TypeLetter", frases);
    }
    #endregion

    private IEnumerator TypeLetter(string[] texto)
    {
        for (int _frase = 0; _frase < texto.Length; _frase++)
        {
            campoTexto.text = "";
            for (int _letra = 0; _letra < texto[_frase].Length; _letra++)
            {
                campoTexto.text += texto[_frase][_letra]; 
                yield return new WaitForSeconds(delayBtwLetters);
            }
            yield return new WaitForSeconds(delayBtwFrases);
        }
    }
    #endregion
}

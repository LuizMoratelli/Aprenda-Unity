using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[System.Serializable]
public class DialogueText
{
    public string text;
    public UnityEvent AfterTalk;
}

[System.Serializable]
public class Dialogue
{
    public int idLevel;
    public DialogueText[] texts;
    public bool wasTalked;
}

public class CutsceneController : MonoBehaviour
{
    #region Sub-Classes and Structs
    #endregion

    #region Public Members
    #endregion

    #region Private Members
    [SerializeField] private GameObject pointToGameObjectEffect;
    [SerializeField] private Text dialogueBox;
    [SerializeField] private float timeBtwLetters;
    [SerializeField] private float timeBtwLines;
    [SerializeField] private Dialogue[] dialogues;
    private PlayerController playerController;
    #endregion

    #region Public Methods
    public void PointToGameObject(string targetTag)
    {
        GameObject[] _targets = GameObject.FindGameObjectsWithTag(targetTag);

        foreach (GameObject _target in _targets)
        {
            GameObject _effect = Instantiate(pointToGameObjectEffect);
            _effect.transform.SetParent(_target.transform);
            _effect.transform.position = _target.transform.position;
        }
    }

    public void DisableDialogue()
    {
        if (GameController.Instance.CurrentIdLevel > 0)
        {
            dialogueBox.gameObject.transform.parent.gameObject.SetActive(false);
            ReturnDialogueOfScene().wasTalked = true;
            StopCoroutine("Dialogue");
            playerController.CurrentPlayerState = PlayerState.playing;
        }
    }

    public void EnableDialogue()
    {
        if (GameController.Instance.CurrentIdLevel > 0)
        {
            dialogueBox.gameObject.transform.parent.gameObject.SetActive(true);
            StartCoroutine("Dialogue");
        }

    }
    #endregion

    #region Private Methods
    #region Unity Default Methods
    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        EnableDialogue();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            DisableDialogue();
        }
    }
    #endregion
    private IEnumerator Dialogue()
    {
        if (!ReturnDialogueOfScene().wasTalked)
        {
            playerController.CurrentPlayerState = PlayerState.teleporting;
            if (ReturnDialogueOfScene().texts != null)
            {
                foreach (DialogueText _lineOfDialogue in ReturnDialogueOfScene().texts)
                {
                    dialogueBox.text = "";
                    foreach (char _letter in _lineOfDialogue.text)
                    {
                        dialogueBox.text += _letter;
                        yield return new WaitForSeconds(timeBtwLetters);
                    }

                    if (_lineOfDialogue.AfterTalk != null)
                    {
                        _lineOfDialogue.AfterTalk.Invoke();
                    }

                    yield return new WaitForSeconds(timeBtwLines);
                }
            }
            ReturnDialogueOfScene().wasTalked = true;
        }

        DisableDialogue();
        playerController.CurrentPlayerState = PlayerState.playing;
    }

    private Dialogue ReturnDialogueOfScene()
    {
        foreach (Dialogue _dialogue in dialogues)
        {
            if (_dialogue.idLevel.Equals(GameController.Instance.CurrentIdLevel))
            {
                return _dialogue;
            }
        }

        return null;
    }
    #endregion
}

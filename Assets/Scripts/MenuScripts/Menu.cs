using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

//Class responsible to handle all UI interactions on game menu
public class Menu : MonoBehaviour
{
    private LoadingScreen loading;

    [Header("Sounds")]
    [SerializeField] AudioClip clip;
    private AudioSource audioSource;

    [Header("Save Popup")]
    [SerializeField] GameObject savePopUp;

    private void Awake()
    {
        loading = GetComponent<LoadingScreen>();
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        audioSource.PlayOneShot(clip);
        loading.LoadLevel();
    }

    public void ShowSavePopUp()
    {
        savePopUp.SetActive(true);
        StartCoroutine(TurnOffSavePopup());
    }

    private IEnumerator TurnOffSavePopup()
    {
        yield return new WaitForSeconds(2);
        savePopUp.SetActive(false);
    }

    #region Configuration Panel
    [Header("Configuration Attributes")]
    [SerializeField] RectTransform configRect;
    [SerializeField] float posX = 500;

    private bool isConfigOpen = false;
    private bool isConfigOpening = false;
    private bool isConfigClosing = false;

    public void ShowConfig()
    {
        audioSource.PlayOneShot(clip);
        if (!isConfigOpen)
            StartCoroutine(OpenHistory());
        else
            StartCoroutine(CloseHistory());
    }

    #region Open Configuration
    IEnumerator OpenHistory()
    {
        if (isConfigOpening)
            yield break;

        isConfigOpening = true;
        configRect.DOAnchorPos(new Vector2(configRect.anchoredPosition.x - posX, 0), 0.4f);
        yield return new WaitForSeconds(0.4f);
        isConfigOpening = false;
        isConfigOpen = true;
    }
    #endregion

    #region Close Configuration
    IEnumerator CloseHistory()
    {
        if (isConfigClosing)
            yield break;

        isConfigClosing = true;
        configRect.DOAnchorPos(new Vector2(configRect.anchoredPosition.x + posX, 0), 0.4f);
        yield return new WaitForSeconds(0.4f);
        isConfigClosing = false;
        isConfigOpen = false;
    }
    #endregion

    #endregion
}



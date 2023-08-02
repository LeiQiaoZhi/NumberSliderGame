using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class Popup : MonoBehaviour
{
    [SerializeField] GameObject titlebar;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] Animator animator;

    [SerializeField] RectTransform buttonArea;
    [SerializeField] private GameObject buttonPrefab;


    private void Awake()
    {
    }

    public void Init(string _title, string _text, Color? _titleColor)
    {
        if (_title == "")
        {
            titlebar.SetActive(false);
        }
        else
        {
            titlebar.SetActive(true);
            titleText.text = _title;
            titleText.color = _titleColor.GetValueOrDefault(Color.white);
        }
        messageText.text = _text;
    }

    public void AddButton(string _text, Color? _color, UnityAction _callback)
    {
        var buttonObject = Instantiate(buttonPrefab, buttonArea);
        buttonObject.SetActive(true);
        buttonObject.GetComponent<Image>().color = _color.GetValueOrDefault(Color.white);
        buttonObject.GetComponentInChildren<TextMeshProUGUI>().text = _text;
        buttonObject.GetComponent<Button>().onClick.AddListener(_callback);
    }
    
    public void AddCancelButton(string _text, Color? _color)
    {
        var buttonObject = Instantiate(buttonPrefab, buttonArea);
        buttonObject.SetActive(true);
        buttonObject.GetComponent<Image>().color = _color.GetValueOrDefault(Color.white);
        buttonObject.GetComponentInChildren<TextMeshProUGUI>().text = _text;
        buttonObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            Destroy(gameObject);
        });
    }
}
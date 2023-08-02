using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

// SINGLETON

public class MessageManager : MonoBehaviour
{
    [SerializeField] private bool testMessages;
    [SerializeField] GameObject messageObjectPrefab;
    [SerializeField] GameObject popupObjectPrefab;
    [SerializeField] private GameObject messageCanvas;

    public static MessageManager instance;


    private void Start()
    {
        if (testMessages)
        {
            // test message
            DisplayMessage("Test Message", null, 2);

            // test popup
            var popup = DisplayPopup("TITLE", "test message");
            popup.AddButton("Confirm", null, () =>
            {
                XLogger.Log(Category.UI, "confirm button pressed");
                Destroy(gameObject);
            });
            popup.AddButton("Cancel", null, () =>
            {
                XLogger.Log(Category.UI, "cancel button pressed");
                Destroy(gameObject);
            });
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DisplayMessage(string _text, Color? _textColor = null, float? _duration = null)
    {
        GameObject messageGo = Instantiate(messageObjectPrefab, messageCanvas.transform);
        Message m = messageGo.GetComponent<Message>();
        m.Init(_text, _textColor, _duration);
        Destroy(messageGo, 10f);
    }

    public Popup DisplayPopup(string _title = "", string _text = "", Color? _titleColor = null)
    {
        GameObject messageGo = Instantiate(popupObjectPrefab, messageCanvas.transform);
        Popup m = messageGo.GetComponent<Popup>();
        m.Init(_title, _text, _titleColor);
        return m;
    }
}
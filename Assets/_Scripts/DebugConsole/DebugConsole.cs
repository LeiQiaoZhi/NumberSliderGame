using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugConsole : MonoBehaviour
{
    public KeyCode consoleKey;
    public GameObject console;
    public TMP_InputField inputField;
    public RectTransform historyContainer;
    public GameObject historyTextPrefab;

    List<DebugCommand> commandList_;

    private void Awake()
    {
        console.SetActive(false);

        // stores all the available commands in a list
        commandList_ = new List<DebugCommand>
        {
            DebugCommandList.testCommand,
            DebugCommandList.quitCommand,
            DebugCommandList.helpCommand,
            DebugCommandList.unlockAllLevelsCommand,
            DebugCommandList.resetAllLevelsCommand
        };
    }

    void Update()
    {
        if (Input.GetKeyDown(consoleKey))
        {
            console.SetActive(!console.activeSelf);
            if (console.activeSelf)
            {
                inputField.Select();
            }
        }

        if (console.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                OnCommandEnter();
            }
        }
    }

    public void OnCommandEnter()
    {
        var inputText = inputField.text;
        if (inputText == "")
            return;
        HandleInput(inputText);
        
        // clear text
        inputField.text = "";
        // inputField.Select();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void HandleInput(string _input)
    {
        bool valid = false;
        string result = "";
        // check if the input contains any valid commands
        for (int i = 0; i < commandList_.Count; i++)
        {
            if (_input.Contains(commandList_[i].commandName))
            {
                result = commandList_[i].Raise(this);
                valid = true;
                XLogger.Log(Category.DebugConsole, result);
                break;
            }
        }

        if (!valid)
        {
            result = $"Command \"{_input}\" not found.";
            XLogger.LogWarning(Category.DebugConsole, result);
        }

        var history = Instantiate(historyTextPrefab, historyContainer);
        var tmp = history.GetComponent<TextMeshProUGUI>();
        tmp.text = result;
        tmp.color = valid ? Color.green : Color.yellow;
    }
}
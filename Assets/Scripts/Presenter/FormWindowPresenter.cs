using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FormWindowPresenter
{
    private FormWindowModel model;
    private FormWindowView view;

    public FormWindowPresenter(FormWindowModel model, FormWindowView view)
    {
        this.model = model;
        this.view = view;
        LoadPreviousState();
    }

    private void LoadPreviousState()
    {
        LoadPreviousInputs();
        LoadCurrentInputFromPlayerPrefs();

        if (model.PreviousTextInputs.Count > 0)
        {
            foreach (string str in model.PreviousTextInputs)
            {
                view.InitNewResultObject(str);
            }
        }
    }

    public void ResultButtonClick(string input, Action<string,bool> onClickResultButtonAction)
    {
        string currentInput = input;

        if (IsValidInput(input))
        {
            onClickResultButtonAction?.Invoke(input,true);
        }
        else
        {
            view.InitNewResultObject($"{currentInput}=ERROR");
            model.AddToPreviousTextInputs($"{currentInput}=ERROR");
            model.SetInputField($"{currentInput}");
            onClickResultButtonAction?.Invoke(input,false);
        }
    }

    public void SetResultFromCalculator(string value)
    {
        model.AddToPreviousTextInputs(value);
        model.SetInputField("");
    }

    private bool IsValidInput(string input)
    {
        string pattern = @"^\d+\+\d+$";
        return System.Text.RegularExpressions.Regex.IsMatch(input, pattern);
    }

    private void LoadPreviousInputs()
    {
        string previousInputs = PlayerPrefs.GetString("inputHistory", "");

        if (!string.IsNullOrEmpty(previousInputs))
        {
            JsonData jsonData = JsonUtility.FromJson<JsonData>(previousInputs);

            if (jsonData != null && jsonData.data != null)
            {
                List<string> resultsArray = jsonData.data.Select(d => d.result).ToList();
                model.UpdatePreviousTextInputs(resultsArray);
            }
            else model.UpdatePreviousTextInputs(new List<string>());
        }
        else model.UpdatePreviousTextInputs(new List<string>());
    }

    private void SaveInputHistory()
    {
        List<ResultData> results = new List<ResultData>();

        foreach (string input in model.PreviousTextInputs)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                results.Add(new ResultData {result = input});
            }
        }

        JsonData jsonData = new JsonData {data = results};
        string jsonString = JsonUtility.ToJson(jsonData, true);
        PlayerPrefs.SetString("inputHistory", jsonString);
        PlayerPrefs.Save();
    }

    public void InputChanged(string newValue)
    {
        model.SetTextInput(newValue);
        model.SetInputField(newValue);
    }

    private void SaveCurrentInputToPlayerPrefs()
    {
        PlayerPrefs.SetString("previousInput", model.CurrentTextInput);
        PlayerPrefs.Save();
    }

    private void LoadCurrentInputFromPlayerPrefs()
    {
        string str = PlayerPrefs.GetString("previousInput", "");
        model.SetTextInput(str);
        model.SetInputField(str);
    }

    public void ExitFromApp()
    {
        SaveCurrentInputToPlayerPrefs();
        SaveInputHistory();
    }
}
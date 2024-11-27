using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FormWindowView : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button resultButton;
    [SerializeField] private GameObject resultTextPrefab; 
    [SerializeField] private Transform resultTextDirectory;
    [SerializeField] private GameObject canvas;

    private FormWindowPresenter presenter;
    private Action<string,bool> onClickResultButtonAction;
    
    public TMP_InputField InputField
    {
        get => inputField;
        set => inputField = value;
    }

    public void Init(FormWindowPresenter calculatorPresenter,Action<string,bool> onClickResultButtonAction)
    {
        presenter = calculatorPresenter;
        this.onClickResultButtonAction = onClickResultButtonAction;
    }
    private void Start()
    {
        resultButton.onClick.AddListener(OnCalculateButtonClicked);
        inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
        Application.quitting += OnApplicationQuit;
    }
    private void OnCalculateButtonClicked()
    {
        presenter.ResultButtonClick(inputField.text,onClickResultButtonAction);
    }
    private void OnInputFieldValueChanged(string newValue)
    {
        presenter.InputChanged(newValue);
    }
    public void InitNewResultObject(string text)
    {
        GameObject newRes = Instantiate(resultTextPrefab, resultTextDirectory);
        newRes.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void SetResultFromCalculator(string text)
    {
        presenter.SetResultFromCalculator(text);
    }
    private void OnApplicationQuit()
    {
        presenter.ExitFromApp();
    }

    public void ShowFormPanel()
    {
        canvas.SetActive(true);
    }
    public void HideFormPanel()
    {
        canvas.SetActive(false);
    }
}

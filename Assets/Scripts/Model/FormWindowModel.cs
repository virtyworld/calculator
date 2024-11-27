using System.Collections.Generic;

public class FormWindowModel
{
    private FormWindowView formWindowView;
    private string currentTextResult;
    private string currentTextInput;
    private List<string> previousTextInputs;
    public string CurrentTextInput => currentTextInput;
    public List<string> PreviousTextInputs => previousTextInputs;

    public FormWindowModel(FormWindowView formWindowView)
    {
        this.formWindowView = formWindowView;
    }

    public void SetTextInput(string text)
    {
        currentTextInput = text;
    }

    public void UpdatePreviousTextInputs(List<string> textArray)
    {
        previousTextInputs = textArray;
    }

    public void AddToPreviousTextInputs(string text)
    {
        previousTextInputs.Add(text);
    }

    public void SetInputField(string text)
    {
        formWindowView.InputField.text = text;
    }
}
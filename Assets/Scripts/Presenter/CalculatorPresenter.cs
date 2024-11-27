using System;

public class CalculatorPresenter
{
    private CalculatorModel model;

    public CalculatorPresenter(CalculatorModel model)
    {
        this.model = model;
    }

    public void Calculate(string input,Action <string> onCalculatedAction)
    {
        string currentInput = input;
    
        if (IsValidInput(input))
        {
            int result = AdditionOfNumbers(input);
            model.SetCurrentTextResult($"{currentInput}={result}");
            onCalculatedAction?.Invoke($"{currentInput}={result}");
        }
        else
        {
            model.SetCurrentTextResult($"{currentInput}=ERROR");
            onCalculatedAction?.Invoke($"{currentInput}=ERROR");
        }
    }

    private bool IsValidInput(string input)
    {
        string pattern = @"^\d+\+\d+$";
        return System.Text.RegularExpressions.Regex.IsMatch(input, pattern);
    }

    private int AdditionOfNumbers(string input)
    {
        string[] numbers = input.Split('+');
        int number1 = int.Parse(numbers[0]);
        int number2 = int.Parse(numbers[1]);
        return number1 + number2;
    }
}


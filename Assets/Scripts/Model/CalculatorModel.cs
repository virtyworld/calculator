
public class CalculatorModel
{
    private CalculatorView calculatorView;
    private string currentTextResult;
    

    public void Init(CalculatorView calculatorView)
    {
        this.calculatorView = calculatorView;
    }

    public void SetCurrentTextResult(string text)
    {
        currentTextResult = text;
    } 
  
}
using Calculator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

public class CalculatorController : Controller
{
    private readonly CalculatorModel _model = new CalculatorModel();
    private static List<string> _history = new();

    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.History = _history;
        return View();
    }

    [HttpPost]
    public IActionResult Index(string operation, double number1, double number2)
    {
        try
        {
            double result = operation switch
            {
                "Add" => _model.Add(number1, number2),
                "Subtract" => _model.Subtract(number1, number2),
                "Multiply" => _model.Multiply(number1, number2),
                "Divide" => _model.Divide(number1, number2),
                _ => throw new InvalidOperationException("Invalid operation")
            };

            string expression = $"{number1} {GetSymbol(operation)} {number2} = {result}";
            _history.Add(expression);
            ViewBag.Result = expression;
        }
        catch (Exception ex)
        {
            ViewBag.Result = $"Error: {ex.Message}";
        }

        ViewBag.Number1 = number1;
        ViewBag.Number2 = number2;
        ViewBag.Operation = operation;
        ViewBag.History = _history;

        return View();
    }
    [HttpPost]
    public IActionResult ClearHistory()
    {
        // Clear the history
        _history.Clear();
        return RedirectToAction("Index");
    }
    private string GetSymbol(string op) => op switch
    {
        "Add" => "+",
        "Subtract" => "-",
        "Multiply" => "×",
        "Divide" => "÷",
        _ => "?"
    };
}

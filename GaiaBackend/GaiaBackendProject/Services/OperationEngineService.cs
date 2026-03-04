using CalculationProject.Interfaces;
using CalculationProject.Models;
using Microsoft.Extensions.Options;
using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

public class OperationEngineService: IOperationEngine
{
    private OperationConfig _config;

    private Dictionary<string, string> _expressionCache = new Dictionary<string, string>();
    private Dictionary<string, string> _stringExpressionCache = new Dictionary<string, string>();   
    public OperationEngineService(IOptionsMonitor<OperationConfig> monitor)
    {
        _config = monitor.CurrentValue;

        BuildCache(_config);

        monitor.OnChange(newConfig =>
        {
            ValidateConfig(newConfig);
            _config = newConfig;
            BuildCache(newConfig);
        });
    }

    private void BuildCache(OperationConfig config)
    {
        _expressionCache = config.Arithmetic
        .ToDictionary(x => x.Code, x => x.Expression);

        _stringExpressionCache = config.String
            .ToDictionary(x => x.Code, x => x.Expression);
    }

    private void ValidateConfig(OperationConfig config)
    {
        foreach (var op in config.Arithmetic)
        {
            var test = new NCalc.Expression(op.Expression);
            test.Parameters["a"] = 1;
            test.Parameters["b"] = 1;
            test.Evaluate();
        }
    }
   
    public string ExecuteArithmetic(string code, double a, double b)
    {
        if (code == "Divide" && b == 0)
        {
            return "Cannot divide by zero"; 
        }
        if (!_expressionCache.ContainsKey(code))
            throw new Exception("Operation not found");

        var expression = new  NCalc.Expression(_expressionCache[code]);
        expression.Parameters["a"] = a;
        expression.Parameters["b"] = b;

        return expression.Evaluate().ToString();
    }
    public List<OperationDefinition> GetOperations()
    {
        return _config.Arithmetic
      .Concat(_config.String)
      .ToList();
    }
    public bool IsArithmeticOperation(string code)
    {
        return _config.Arithmetic.Any(o => o.Code == code);
    }
    public string ExecuteString(string methodName, string a, string b)
    {
        if (!_config.String.Any(x => x.Code == methodName))
            throw new Exception("Operation not allowed");

        var stringType = typeof(string);

        var staticMethod = stringType.GetMethod(
            methodName,
            BindingFlags.Public | BindingFlags.Static,
            null,
            new[] { typeof(string), typeof(string) },
            null);

        if (staticMethod != null && staticMethod.ReturnType == typeof(string))
            return staticMethod.Invoke(null, new object[] { a, b })?.ToString();

        var instanceMethod1 = stringType.GetMethod(
            methodName,
            BindingFlags.Public | BindingFlags.Instance,
            null,
            new[] { typeof(string) },
            null);

        if (instanceMethod1 != null && instanceMethod1.ReturnType == typeof(string))
        {
            var resultA = instanceMethod1.Invoke(a, new object[] { b })?.ToString();
            return resultA;
        }

        var instanceMethod0 = stringType.GetMethod(
            methodName,
            BindingFlags.Public | BindingFlags.Instance,
            null,
            Type.EmptyTypes,
            null);

        if (instanceMethod0 != null && instanceMethod0.ReturnType == typeof(string))
        {
            var resultA = instanceMethod0.Invoke(a, null)?.ToString();
            var resultB = instanceMethod0.Invoke(b, null)?.ToString();
            return $"{resultA} {resultB}";
        }

        throw new Exception("Method not found or unsupported signature");
    }
}
using System.Linq.Expressions;
using System.Reflection;

namespace ElectronicCad.MVVM.Properties.Helpers;

/// <summary>
/// Contains helper method with reflection.
/// </summary>
internal static class ReflectionHelper
{
    /// <summary>
    /// Find property information in the lambda expression.
    /// </summary>
    /// <param name="lambdaExpression">Lambda expression.</param>
    /// <exception cref="ArgumentException">Occurs when expression doesn't contain to level property.</exception>
    public static PropertyInfo FindPropertyInfo(LambdaExpression lambdaExpression)
    {
        Expression expressionToCheck = lambdaExpression.Body;
        while (true)
        {
            switch (expressionToCheck)
            {
                case MemberExpression { Member: var member, Expression.NodeType: ExpressionType.Parameter or ExpressionType.Convert }:
                    if (member is PropertyInfo propertyInfo)
                    {
                        return propertyInfo;
                    }
                    break;
                case UnaryExpression { Operand: var operand }:
                    expressionToCheck = operand;
                    break;
                default:
                    throw new ArgumentException(
                        $"Expression '{lambdaExpression}' must resolve to top-level member and not any child object's properties.",
                        nameof(lambdaExpression));
            }
        }
    }

}
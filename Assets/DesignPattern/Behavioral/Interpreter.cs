using System;
/// <summary>
/// 解释器模式（类）
/// </summary>
public class Interpreter
{
    //多元加法解释器

    /// <summary>
    /// 表达式抽象
    /// </summary>
    public interface Expression
    {
        /// <summary>
        /// 解释方法
        /// </summary>
        /// <returns>解释后需返回的对象(本例中为计算后的值)</returns>
        int Interpret(string value);
    }

    /// <summary>
    /// 值表达式
    /// </summary>
    /// <remarks>终结符表达式(即不调用其他表达式的表达式)</remarks>
    public class ValueExpression : Expression
    {
        /// <summary>
        /// 解释值表达式
        /// </summary>
        public int Interpret(string value)
        {
            return Convert.ToInt32(value);
        }
    }

    /// <summary>
    /// 加法表达式
    /// </summary>
    /// <remarks>非终结符表达式(即需调用其他表达式的表达式)</remarks>
    public class AddExpression : Expression
    {
        private Expression Expression;

        public AddExpression(Expression expression)
        {
            this.Expression = expression;
        }

        /// <summary>
        /// 解释加法表达式
        /// </summary>
        public int Interpret(string value)
        {
            var values = value.Split('+');

            int sum = 0;
            foreach (var val in values)
            {
                sum += Expression.Interpret(val);
            }

            return sum;
        }
    }

    /// <summary>
    /// 计算解释器
    /// </summary>
    public class CalcInterpreter
    {
        private Expression Expression;

        public CalcInterpreter()
        {
            var valueExpression = new ValueExpression();
            Expression = new AddExpression(valueExpression);
        }

        public int Execute(string expressionStr)
        {
            return Expression.Interpret(expressionStr);
        }
    }

    /*
        调用方式：
        var calc = new CalcInterpreter();
        calc.Execute("1+2+3+4+5");
     */
}

using UnityEngine;
/// <summary>
/// 策略模式
/// </summary>
public class Strategy
{
    /// <summary>
    /// 抽象图表
    /// </summary>
    /// <remarks>抽象策略</remarks>
    public interface IChart
    {
        /// <summary>
        /// 渲染图表
        /// </summary>
        void Render();
    }

    /// <summary>
    /// 饼图
    /// </summary>
    /// <remarks>具体策略类</remarks>
    public class PieChart : IChart
    {
        public void Render()
        {
            Debug.Log("Render PieChart");
        }
    }

    /// <summary>
    /// 线图
    /// </summary>
    /// <remarks>具体策略类</remarks>
    public class LineChart : IChart
    {
        public void Render()
        {
            Debug.Log("Render LineChart");
        }
    }

    /// <summary>
    /// 渲染环境类
    /// </summary>
    public class RenderContext
    {
        /// <summary>
        /// 实现图表对象
        /// </summary>
        public IChart Chart { get; set; }

        public void SetChart(IChart Chart)
        {
            this.Chart = Chart;
        }

        /// <summary>
        /// 执行当前图表对象渲染
        /// </summary>
        public void Render()
        {
            this.Chart.Render();
        }
    }

    /*
        调用方式：
        RenderContext context = new RenderContext();
        context.SetChart(new PieChart());
        context.Render();
     */
}

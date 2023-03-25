using UnityEngine;
/// <summary>
/// 桥接模式
/// </summary>
public class Bridge
{
    /// <summary>
    /// 颜色
    /// </summary>
    public interface Color
    {
        string GetColor();
    }

    /// <summary>
    /// 红色
    /// </summary>
    /// <remarks>
    /// 扩展颜色
    /// </remarks>
    public class Red : Color
    {
        public string GetColor()
        {
            return "Red";
        }
    }

    /// <summary>
    /// 蓝色
    /// </summary>
    /// <remarks>
    /// 扩展颜色
    /// </remarks>
    public class Blue : Color
    {
        public string GetColor()
        {
            return "Blue";
        }
    }

    /// <summary>
    /// 材质
    /// </summary>
    public interface Texture
    {
        string GetTexture();
    }

    /// <summary>
    /// 棉质
    /// </summary>
    /// <remarks>
    /// 扩展材质
    /// </remarks>
    public class Cotton : Texture
    {
        public string GetTexture()
        {
            return "Cotton";
        }
    }

    /// <summary>
    /// 涤纶
    /// </summary>
    /// <remarks>
    /// 扩展材质
    /// </remarks>
    public class Polyester : Texture
    {
        public string GetTexture()
        {
            return "Polyester";
        }
    }

    /// <summary>
    /// 商品抽象
    /// </summary>
    public abstract class Commodity
    {
        protected Color Color { get; set; }

        protected Texture Texture { get; set; }

        public void SetColor(Color color)
        {
            this.Color = color;
        }

        public void SetTexture(Texture texture)
        {
            this.Texture = texture;
        }

        public abstract void Print();
    }

    /// <summary>
    /// A商品
    /// </summary>
    /// <remarks>
    /// 扩展抽象
    /// </remarks>
    public class ACommodity : Commodity
    {
        public override void Print()
        {
            Debug.Log($"A:{Color.GetColor()}:{Texture.GetTexture()}");
        }
    }

    /// <summary>
    /// B商品
    /// </summary>
    /// <remarks>
    /// 扩展抽象
    /// </remarks>
    public class BCommodity : Commodity
    {
        public override void Print()
        {
            Debug.Log($"B:{Color.GetColor()}:{Texture.GetTexture()}");
        }
    }

    /*
        调用方式：
        Color color = new Red();
        Texture texture = new Polyester();
        Commodity commodity = new ACommodity();
        commodity.SetColor(color);
        commodity.SetTexture(texture);
        commodity.Print();
     */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polymorphism : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //参数多态
        ParameterPolymorphism();
        //包含多态
        ContainPolymorphism();
        //过载多态
        OverloadingPolymorphism();
        //强制多态
        ForcedPolymorphism();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract class Shape
    {
        public abstract void draw();
    }

    public class Rectangle : Shape
    {
        public override void draw()
        {
            Debug.Log("绘制矩形");
        }
    }

    public class Circle : Shape
    {
        public override void draw()
        {
            Debug.Log("绘制圆形");
        }
    }

    public void drawShape(Shape shape)
    {
        shape.draw();
    }

    public void ParameterPolymorphism()
    {
        Shape rectangle = new Rectangle();
        Shape circle = new Circle();

        drawShape(rectangle); // 输出：绘制矩形
        drawShape(circle); // 输出：绘制圆形
    }





    public class Triangle : Shape
    {
        public override void draw()
        {
            Debug.Log("绘制三角形");
        }
    }


    public class DrawingBoard
    {
        public void drawShape(Shape shape)
        {
            shape.draw();
        }
    }

    public void ContainPolymorphism()
    {
        DrawingBoard drawingBoard = new DrawingBoard();

        // 创建不同类型的图形对象
        Shape rectangle = new Rectangle();
        Shape circle = new Circle();
        Shape triangle = new Triangle();

        // 绘制图形
        drawingBoard.drawShape(rectangle); // 输出：绘制矩形
        drawingBoard.drawShape(circle); // 输出：绘制圆形
        drawingBoard.drawShape(triangle); // 输出：绘制三角形
    }



    public class Calculator
    {
        public int add(int a, int b)
        {
            return a + b;
        }

        public double add(double a, double b)
        {
            return a + b;
        }

        public int add(int a, int b, int c)
        {
            return a + b + c;
        }


    }

    public void OverloadingPolymorphism()
    {
        Calculator calculator = new Calculator();

        int sum1 = calculator.add(2, 3); // 输出：5
        double sum2 = calculator.add(2.5, 3.7); // 输出：6.2
        int sum3 = calculator.add(2, 3, 4); // 输出：9
    }


    public class Animal
    {
        public virtual void makeSound()
        {
            Debug.Log("动物发出声音");
        }
    }

    public class Dog : Animal
    {
        public override void makeSound()
        {
            Debug.Log("狗发出汪汪声");
        }
    }

    public class Cat : Animal
    {
        public override void makeSound()
        {
            Debug.Log("猫发出喵喵声");
        }
    }


    public void animalSound(Animal animal)
    {
        animal.makeSound();
    }

    public void ForcedPolymorphism()
    {
        Animal animal1 = new Dog();
        Animal animal2 = new Cat();

        animalSound(animal1); // 输出：狗发出汪汪声
        animalSound(animal2); // 输出：猫发出喵喵声
    }
}



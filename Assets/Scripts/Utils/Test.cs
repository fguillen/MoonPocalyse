using System.Collections.Generic;

public abstract class FruitBox<T>
{
    public T item;

    public static T ChooseFirst(List<FruitBox<T>> fruitBoxes)
    {
        return fruitBoxes[0].item;
    }
}

public class Apple
{
}

public class AppleBox : FruitBox<Apple>
{
}

public class FruitShop
{
    List<AppleBox> appleBoxes = new List<AppleBox>();

    public void Main()
    {
        AppleBox appleBox = new AppleBox();
        appleBoxes.Add(appleBox);

        // AppleBox.ChooseFirst((List<FruitBox<Apple>>)appleBoxes);
    }
}

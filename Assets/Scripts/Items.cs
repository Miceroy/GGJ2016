using UnityEngine;
using System.Collections;

public interface ActionInterface
{
    void doAction(GameObject otherObject);
}


public class Items {


    public enum ItemType
    {
        FLOWER,
        LAST_ITEM_TYPE
    }
}

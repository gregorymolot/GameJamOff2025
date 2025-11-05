using System;
using UnityEngine;

public static class EventManager
{
    public static readonly ItemEvents Items = new ItemEvents();
    public class ItemEvents
    {
        public Action<InteractableItem> ShowItem;
        public Action Return;
    }
}

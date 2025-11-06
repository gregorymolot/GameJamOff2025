using System;
using System.Collections.Generic;
using UnityEngine;

public class UnlockEvent<ItemKey>
{
    public Action<ItemKey> unlockAction;
}

public static class EventManager
{
    public static readonly ItemEvents Items = new ItemEvents();
    public static readonly UnlockEvents Unlocks = new UnlockEvents();
    public class ItemEvents
    {
        public Action<InteractableItem> ShowItem;
        public Action Return;
    }

    public class UnlockEvents
    {

        private Dictionary<ItemKey, UnlockEvent<ItemKey>> mapUnlock = new Dictionary<ItemKey, UnlockEvent<ItemKey>>();

        public UnlockEvent<ItemKey> OnUnlockEvent(ItemKey key)
        {
            mapUnlock.TryAdd(key, new UnlockEvent<ItemKey>());
            return mapUnlock[key];

        }
    }
}

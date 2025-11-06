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

        private Dictionary<Clues, UnlockEvent<Clues>> mapUnlock = new Dictionary<Clues, UnlockEvent<Clues>>();

        public UnlockEvent<Clues> OnUnlockEvent(Clues key)
        {
            mapUnlock.TryAdd(key, new UnlockEvent<Clues>());
            return mapUnlock[key];

        }
    }
}

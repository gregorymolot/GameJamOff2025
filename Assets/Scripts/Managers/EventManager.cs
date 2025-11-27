using System;
using System.Collections.Generic;
using UnityEngine;

public class UnlockEvent<ItemKey>
{
    public Action<ItemKey> unlockAction;
}

public class IdentifyEvent<Name>
{
    public Action<Name> identifyAction;
}

public static class EventManager
{
    public static readonly ItemEvents Items = new ItemEvents();
    public static readonly UnlockEvents Unlocks = new UnlockEvents();
    public static readonly GameEvents Game = new GameEvents();
    public class ItemEvents
    {
        public Action<DiscoverableItem> ShowItem;
        public Action Return;

        public Action<bool> ToggleSwitch;
    }

    public class GameEvents
    {
        public Action BeginGame;
    }

    public class UnlockEvents
    {
        public Action<Clues> Unlock;

        public Action<Name> Interacted;

        public Action NewUnlock;
    }

    public class IdentifyEvents
    {

        private Dictionary<Clues, IdentifyEvent<Name>> mapIdentify = new Dictionary<Clues, IdentifyEvent<Name>>();

        public IdentifyEvent<Name> OnIdentifyEvent(Clues key)
        {
            mapIdentify.TryAdd(key, new IdentifyEvent<Name>());
            return mapIdentify[key];
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;



public class CharacterProfile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
        [SerializedDictionary("Clues", "Profile")]
        public SerializedDictionary<Unlockable, string> profile;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

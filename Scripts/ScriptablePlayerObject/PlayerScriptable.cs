using System.Collections.Generic;
using UnityEngine;


namespace ScriptablePlayerObject
{
    
    [CreateAssetMenu(menuName = "Player Creator/Create")]
    public class PlayerScriptable : ScriptableObject
    {
        public Color PlayerColor;
        public GameObject PlayerPrefab;
        public PlayerType PlayerTypes;
        public enum PlayerType
        {
            Enemy,
            Player
        };
        

    }
}

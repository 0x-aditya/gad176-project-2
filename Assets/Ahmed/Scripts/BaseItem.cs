using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthGame.Shop
{
    /// <summary>
    /// Abstract base for all in-game items.
    /// </summary>
    public abstract class BaseItem : ScriptableObject
    {
        [SerializeField, Tooltip("Unique identifier for this item.")]
        private int _id;
        [SerializeField, Tooltip("Name shown in UI.")]
        private string _displayName;
        [SerializeField, Tooltip("Cost in game currency.")]
        private float _price;

        /// <summary>Gets the unique ID.</summary>
        public int Id => _id;
        /// <summary>Gets the display name.</summary>
        public string DisplayName => _displayName;
        /// <summary>Gets the price.</summary>
        public float Price => _price;

        /// <summary>
        /// Called when the player uses this item.
        /// </summary>
        public abstract void Use(PlayerController player);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthGame.Shop
{
    /// <summary>
    /// Holds and manages player items.
    /// </summary>
    public class Inventory
    {
        private readonly Dictionary<int, BaseItem> _items = new Dictionary<int, BaseItem>();

        /// <summary>Adds an item, if not already present.</summary>
        public bool AddItem(BaseItem item)
        {
            if (item == null)
            {
                Debug.LogWarning("[Inventory] Null item.");
                return false;
            }
            if (_items.ContainsKey(item.Id))
            {
                Debug.LogWarning($"[Inventory] Already have {item.DisplayName}.");
                return false;
            }
            _items[item.Id] = item;
            Debug.Log($"[Inventory] Added {item.DisplayName}.");
            return true;
        }

        /// <summary>Removes an item by ID.</summary>
        public bool RemoveItem(int itemId)
        {
            if (!_items.ContainsKey(itemId))
            {
                Debug.LogWarning($"[Inventory] No item ID {itemId}.");
                return false;
            }
            _items.Remove(itemId);
            Debug.Log($"[Inventory] Removed ID {itemId}.");
            return true;
        }

        /// <summary>Gets an item by ID.</summary>
        public BaseItem GetItem(int itemId)
        {
            _items.TryGetValue(itemId, out var item);
            return item;
        }

        /// <summary>All items in inventory.</summary>
        public IReadOnlyCollection<BaseItem> GetAllItems() => _items.Values;
    }
}

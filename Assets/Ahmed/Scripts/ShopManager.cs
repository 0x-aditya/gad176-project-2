using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace StealthGame.Shop
{
    public class ShopManager : MonoBehaviour
    {
        [Header("Shop Config")]
        [SerializeField, Tooltip("Items you can buy here.")]
        private BaseItem[] _catalogue;
        [SerializeField, Tooltip("How close the player must be.")]
        private float _interactRadius = 3f;
        [SerializeField, Tooltip("Key to open shop.")]
        private KeyCode _interactKey = KeyCode.E;

        private PlayerController _player;
        private ShopUI _ui;

        private void Start()
        {
            _player = FindObjectOfType<PlayerController>();
            Debug.Assert(_player != null, "[ShopManager] No player found.");
            _ui = FindObjectOfType<ShopUI>();
            Debug.Assert(_ui != null, "[ShopManager] No ShopUI found.");

            _ui.Initialize(this, _catalogue, _player);
        }

        private void Update()
        {
            // 1) Check for player in range
            Collider[] hits = Physics.OverlapSphere(transform.position, _interactRadius);
            bool playerNearby = false;
            foreach (var c in hits)
                if (c.GetComponent<PlayerController>() != null)
                    playerNearby = true;

            if (playerNearby)
            {
                Debug.Log("[ShopManager] Player is within interact radius.");
                // 2) Check for keypress
                if (Input.GetKeyDown(_interactKey))
                {
                    Debug.Log("[ShopManager] Interact key pressed. Attempting to ToggleShop().");
                    _ui.ToggleShop();
                }
            }
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _interactRadius);
        }

        /// <summary>
        /// Called by UI when an item button is clicked.
        /// </summary>
        public void BuyItem(BaseItem item)
        {
            if (item == null) return;
            if (_player.SpendCurrency(item.Price) && _player.Inventory.AddItem(item))
            {
                Debug.Log($"[Shop] Bought {item.DisplayName} for {item.Price}");
                _ui.UpdateCurrencyDisplay(_player.Currency);
            }
            else
            {
                Debug.LogWarning($"[Shop] Could not buy {item.DisplayName}");
            }
        }
    }
}


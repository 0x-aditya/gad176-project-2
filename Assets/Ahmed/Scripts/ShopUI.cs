using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StealthGame.Shop
{
    public class ShopUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject _shopPanel;
        [SerializeField] private Transform _itemListContainer;
        [SerializeField] private Button _itemButtonPrefab;
        [SerializeField] private TMP_Text _currencyText;

        [Header("Purchase Feedback")]
        [SerializeField] private TMP_Text _confirmationText;
        [SerializeField, Tooltip("Seconds before the confirmation message disappears.")]
        private float _messageDuration = 2f;

        private ShopManager _shopManager;
        private BaseItem[] _catalogue;
        private PlayerController _player;
        private bool _isOpen;
        private Coroutine _hideCoroutine;

        /// <summary>
        /// Called by ShopManager at Start.
        /// </summary>
        public void Initialize(ShopManager manager, BaseItem[] catalogue, PlayerController player)
        {
            _shopManager = manager;
            _catalogue = catalogue;
            _player = player;

            PopulateShop();
            UpdateCurrencyDisplay(_player.Currency);

            // Hide UI by default
            _shopPanel.SetActive(false);
            if (_confirmationText != null)
                _confirmationText.gameObject.SetActive(false);
        }

        /// <summary>
        /// Clears old buttons and instantiates one per catalogue item.
        /// </summary>
        private void PopulateShop()
        {
            foreach (Transform child in _itemListContainer)
                Destroy(child.gameObject);

            foreach (var item in _catalogue)
            {
                var btn = Instantiate(_itemButtonPrefab, _itemListContainer);
                var label = btn.GetComponentInChildren<TMP_Text>();
                if (label != null)
                    label.text = $"{item.DisplayName} ({item.Price:F0})";

                // Instrumented listener:
                btn.onClick.AddListener(() =>
                {
                    Debug.Log($"[ShopUI] Button for '{item.DisplayName}' clicked");
                    _shopManager.BuyItem(item);
                });
            }
        }

        /// <summary>
        /// Toggle shop panel on/off.
        /// </summary>
        public void ToggleShop()
        {
            _isOpen = !_isOpen;
            _shopPanel.SetActive(_isOpen);
        }

        /// <summary>
        /// Update the currency display (top of panel).
        /// </summary>
        public void UpdateCurrencyDisplay(float amount)
        {
            if (_currencyText != null)
                _currencyText.text = $"Currency: {amount:F0}";
        }

        /// <summary>
        /// Show a temporary “You purchased X!” message.
        /// </summary>
        public void ShowConfirmation(string message)
        {
            if (_confirmationText == null) return;

            _confirmationText.text = message;
            _confirmationText.gameObject.SetActive(true);

            if (_hideCoroutine != null)
                StopCoroutine(_hideCoroutine);
            _hideCoroutine = StartCoroutine(HideAfterDelay());
        }

        private IEnumerator HideAfterDelay()
        {
            yield return new WaitForSeconds(_messageDuration);
            _confirmationText.gameObject.SetActive(false);
        }
    }
}





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

        [Header("Interact Prompt")]
        [SerializeField] private TMP_Text _interactPromptText;

        [Header("Purchase Feedback")]
        [SerializeField] private TMP_Text _confirmationText;
        [SerializeField, Tooltip("Seconds before the confirmation message disappears.")]
        private float _messageDuration = 2f;

        private ShopManager _shopManager;
        private BaseItem[] _catalogue;
        private PlayerController _player;
        private bool _isOpen;
        private Coroutine _hideCoroutine;

        // Expose for ShopManager
        public bool IsOpen => _isOpen;

        public void Initialize(ShopManager manager, BaseItem[] catalogue, PlayerController player)
        {
            _shopManager = manager;
            _catalogue = catalogue;
            _player = player;

            PopulateShop();
            UpdateCurrencyDisplay(_player.Currency);

            // Hide everything initially
            _shopPanel.SetActive(false);
            _confirmationText?.gameObject.SetActive(false);
            _interactPromptText?.gameObject.SetActive(false);
        }

        private void PopulateShop()
        {
            foreach (Transform child in _itemListContainer)
                Destroy(child.gameObject);

            foreach (var item in _catalogue)
            {
                var btn = Instantiate(_itemButtonPrefab, _itemListContainer);
                var label = btn.GetComponentInChildren<TMP_Text>();
                if (label != null) label.text = $"{item.DisplayName} ({item.Price:F0})";
                else Debug.LogError("[ShopUI] No TMP_Text on ItemButton!");

                btn.onClick.AddListener(() => {
                    Debug.Log($"[ShopUI] Button for {item.DisplayName} clicked");
                    _shopManager.BuyItem(item);
                });
            }
        }

        public void ToggleShop()
        {
            _isOpen = !_isOpen;
            _shopPanel.SetActive(_isOpen);

            // Cursor
            Cursor.visible = _isOpen;
            Cursor.lockState = _isOpen
                ? CursorLockMode.None
                : CursorLockMode.Locked;

            // Hide prompt when shop is open
            _interactPromptText?.gameObject.SetActive(false);

            // Optionally disable player movement here...
        }

        public void UpdateCurrencyDisplay(float amount)
        {
            if (_currencyText != null)
                _currencyText.text = $"Currency: {amount:F0}";
        }

        /// <summary> Show or hide the “Press E…” prompt. </summary>
        public void ShowInteractPrompt(bool show)
        {
            if (_interactPromptText != null && !_isOpen)
                _interactPromptText.gameObject.SetActive(show);
        }

        /// <summary> Show a generic purchase confirmation. </summary>
        public void ShowConfirmation()
        {
            if (_confirmationText == null) return;

            _confirmationText.text = "You have purchased an item!";
            _confirmationText.gameObject.SetActive(true);

            if (_hideCoroutine != null) StopCoroutine(_hideCoroutine);
            _hideCoroutine = StartCoroutine(HideAfterDelay());
        }

        private IEnumerator HideAfterDelay()
        {
            yield return new WaitForSeconds(_messageDuration);
            _confirmationText.gameObject.SetActive(false);
        }
    }
}





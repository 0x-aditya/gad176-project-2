using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthGame.Shop
{
    /// <summary>
    /// Equippable weapon item.
    /// </summary>
    [CreateAssetMenu(fileName = "NewWeaponItem", menuName = "StealthGame/Items/Weapon")]
    public class WeaponItem : BaseItem
    {
        [SerializeField, Tooltip("Damage dealt by this weapon.")]
        private float _damage;

        /// <summary>Gets weapon damage.</summary>
        public float Damage => _damage;

        public override void Use(PlayerController player)
        {
            if (player == null) return;
            player.EquipWeapon(this);
            Debug.Log($"[Shop] Equipped weapon: {DisplayName} (Damage: {Damage})");
        }
    }
}

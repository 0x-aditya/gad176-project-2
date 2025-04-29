using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthGame.Shop
{
    /// <summary>
    /// Usable gadget item.
    /// </summary>
    [CreateAssetMenu(fileName = "NewGadgetItem", menuName = "StealthGame/Items/Gadget")]
    public class GadgetItem : BaseItem
    {
        public override void Use(PlayerController player)
        {
            if (player == null) return;
            player.ActivateGadget(this);
            Debug.Log($"[Shop] Activated gadget: {DisplayName}");
        }
    }
}

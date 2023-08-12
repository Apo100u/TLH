using System;
using TLH.Gameplay;
using TMPro;
using UnityEngine;

namespace TLH.Playground
{
    public class TargetDummy : MonoBehaviour
    {
        [SerializeField] private TMP_Text hpText;
        [SerializeField] private Health health;

        private void Update()
        {
            hpText.text = $"{health.CurrentHealth}/{health.MaxHealth}";
        }
    }
}
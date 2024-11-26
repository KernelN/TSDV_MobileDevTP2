using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheWasteland.Shop
{
    public abstract class BuyableUI : MonoBehaviour, EventManager.IEventListener
    {
        [SerializeField] internal TMPro.TextMeshProUGUI titleText;
        [SerializeField] internal TMPro.TextMeshProUGUI rewardText;
        [SerializeField] internal TMPro.TextMeshProUGUI costText;
        [SerializeField, Min(1)] int coinCost = 1;
        EventManager.EventManager eventManager;
        internal List<object> data;
        internal int costMultiplier = 1;
        
        const string UPDATE_UI = "UpdateBuyable";
        
        internal virtual void Start()
        {
            eventManager = EventManager.EventManager.inst;
            eventManager.AddListener(UPDATE_UI, this);
            
            data = new List<object>();
            data.Add(coinCost);
            costText.text = coinCost.ToString("000");
        }
        public void Buy()
        {
            data[0] = coinCost * costMultiplier;
            eventManager.TriggerEvent("BuyItem", data.ToArray());
            eventManager.TriggerEvent(UPDATE_UI, null);
        }
        public void OnEventRaised(object[] data)
        {
            SetUI();
        }
        internal abstract void SetUI();
    }
}

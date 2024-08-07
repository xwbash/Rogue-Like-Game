using System.Collections.Generic;
using System.Reflection;
using Rune.Scripts.ScriptableObjects;
using Rune.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;
using VContainer;

namespace Rune.Scripts.Services
{
    public class AbilityService
    {
        public UnityEvent<CardData> OnAbilitySelected = new UnityEvent<CardData>();
        
        private System.Random _random = new System.Random();
        private CardBalanceData _cardBalanceData;

        [Inject]
        private void Construct(CardBalanceData cardBalanceData)
        {
            _cardBalanceData = cardBalanceData;
        }
        
        public void SetNewAbilityCard(CardData cardData)
        {
            OnAbilitySelected.Invoke(cardData);
        }

        public List<CardData> GetNewCardData()
        {
            List<CardData> wholeDeckData = new List<CardData>();

            for (int i = 0; i < 3; i++)
            {
                wholeDeckData.Add(CreateCard());
            }

            return wholeDeckData;
        }

        private CardData CreateCard()
        {
            CardData cardData = new CardData();
            bool isCardEmpty = true;
            
            foreach (var cardAdjustmentData in _cardBalanceData.CardAdjustmentData)
            {
                switch (cardAdjustmentData.AdjustmentType)
                {
                    case AdjustmentType.Damage:
                    {
                        if (CalculatePercentage(cardAdjustmentData.Percentage))
                        {
                            isCardEmpty = false;
                            cardData.Damage = (int)Random.Range(cardAdjustmentData.Value.x, cardAdjustmentData.Value.y);
                        }
                        break;
                    }
                    case AdjustmentType.BulletSpeed:
                    {
                        if (CalculatePercentage(cardAdjustmentData.Percentage))
                        {
                            isCardEmpty = false;
                            cardData.BulletSpeed = Random.Range(cardAdjustmentData.Value.x, cardAdjustmentData.Value.y);
                        }
                        break;
                    }
                    case AdjustmentType.Experiment:
                    {
                        if (CalculatePercentage(cardAdjustmentData.Percentage))
                        {
                            isCardEmpty = false;
                            cardData.ExperimentAmount = Random.Range(cardAdjustmentData.Value.x, cardAdjustmentData.Value.y);
                        }
                        break;
                    }
                    case AdjustmentType.Health:
                    {
                        if (CalculatePercentage(cardAdjustmentData.Percentage))
                        {
                            isCardEmpty = false;
                            cardData.Health = (int)Random.Range(cardAdjustmentData.Value.x, cardAdjustmentData.Value.y);
                        }
                        break;
                    }                    
                    case AdjustmentType.Range:
                    {
                        if (CalculatePercentage(cardAdjustmentData.Percentage))
                        {
                            isCardEmpty = false;
                            cardData.Range = (int)Random.Range(cardAdjustmentData.Value.x, cardAdjustmentData.Value.y);
                        }
                        break;
                    }                   
                    case AdjustmentType.Speed:
                    {
                        if (CalculatePercentage(cardAdjustmentData.Percentage))
                        {
                            isCardEmpty = false;
                            cardData.Speed = Random.Range(cardAdjustmentData.Value.x, cardAdjustmentData.Value.y);
                        }
                        break;
                    }                    
                    case AdjustmentType.GunSpeed:
                    {
                        if (CalculatePercentage(cardAdjustmentData.Percentage))
                        {
                            isCardEmpty = false;
                            cardData.GunSpeed = Random.Range(cardAdjustmentData.Value.x, cardAdjustmentData.Value.y);
                        }
                        break;
                    }
                    case AdjustmentType.EnemyDamage:
                    {
                        if (CalculatePercentage(cardAdjustmentData.Percentage))
                        {
                            isCardEmpty = false;
                            cardData.EnemyDamageDecreasePercentage = Random.Range(cardAdjustmentData.Value.x, cardAdjustmentData.Value.y);
                        }
                        break;
                    }
                    case AdjustmentType.EnemySpeed:
                    {
                        if (CalculatePercentage(cardAdjustmentData.Percentage))
                        {
                            isCardEmpty = false;
                            cardData.EnemySpeedDecreasePercentage = Random.Range(cardAdjustmentData.Value.x, cardAdjustmentData.Value.y);
                        }
                        break;
                    }
                    case AdjustmentType.EnemyBulletSpeed:
                    {
                        if (CalculatePercentage(cardAdjustmentData.Percentage))
                        {
                            isCardEmpty = false;
                            cardData.EnemyBulletSpeedDecreasePercentage = Random.Range(cardAdjustmentData.Value.x, cardAdjustmentData.Value.y);
                        }
                        break;
                    }
                }
            }


            if (isCardEmpty)
            {
                cardData.Health = Random.Range(1, 50);
            }
            
            return cardData;
        }

        private bool CalculatePercentage(int percentage)
        {
            return _random.Next(0, 100) < percentage;
        }

        public List<string> GetDescription(CardData cardData)
        {
            List<string> descriptions = new List<string>();

            if (cardData.Health > 0)
            {
                descriptions.Add("Player Health Increase: " + cardData.Health);
            }
            if (cardData.Speed > 0)
            {
                descriptions.Add("Player Speed Increase: " + cardData.Speed + "%");
            }
            if (cardData.GunSpeed > 0)
            {
                descriptions.Add("Gun Speed Increase: " + cardData.GunSpeed);
            }
            if (cardData.BulletSpeed > 0)
            {
                descriptions.Add("Bullet Speed Increase: " + cardData.BulletSpeed);
            }
            if (cardData.Damage > 0)
            {
                descriptions.Add("Damage Increase: " + cardData.Damage);
            }
            if (cardData.Range > 0)
            {
                descriptions.Add("Range Increase: " + cardData.Range);
            }
            if (cardData.EnemySpeedDecreasePercentage > 0)
            {
                descriptions.Add("Enemy Speed Decrease: " + cardData.EnemySpeedDecreasePercentage + "%");
            }
            if (cardData.EnemyDamageDecreasePercentage > 0)
            {
                descriptions.Add("Enemy Damage Decrease: " + cardData.EnemyDamageDecreasePercentage + "%");
            }
            if (cardData.EnemyBulletSpeedDecreasePercentage > 0)
            {
                descriptions.Add("Enemy Bullet Speed Decrease: " + cardData.EnemyBulletSpeedDecreasePercentage + "%");
            }
            if (cardData.ExperimentAmount > 0)
            {
                descriptions.Add("Experiment Amount Increase: " + cardData.ExperimentAmount);
            }

            return descriptions;
        }
    }
}
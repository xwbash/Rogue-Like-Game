using System.Collections.Generic;
using System.Reflection;
using Rune.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Rune.Scripts.Services
{
    public class AbilityService
    {
        public UnityEvent<CardData> OnAbilitySelected = new UnityEvent<CardData>();
        
        private System.Random _random = new System.Random();
        private int _maxAbilityAmount = 3;
        private float _factor = .8f;
        private List<byte> _percentages = new List<byte>()
        {
            30,        // public int Health = -1;
            20,       // public int Speed = -1;
            5,       // public float GunSpeed = -1;
            15,     // public float BulletSpeed = -1;
            15,    // public int Damage = -1;
            15,   // public float Range = -1;
            3,   // public float EnemySpeedDecreasePercentage = -1;
            3,  // public float EnemyDamageDecreasePercentage = -1;
            3, // public float EnemyBulletSpeedDecreasePercentage = -1;
            10,// public int ExperimentAmount = -1;
        };
        
        public void SetNewAbilityCard(CardData cardData)
        {
            OnAbilitySelected.Invoke(cardData);
        }

        public List<CardData> GetNewCardData()
        {
            List<CardData> wholeDeckData = new List<CardData>();

            for (int i = 0; i < 3; i++)
            {
                CardData cardData = new CardData();
                int abilityAmount = 0;
                
                if (_random.Next(0, 100) < _percentages[0] && abilityAmount < _maxAbilityAmount)
                {
                    abilityAmount += 1;
                    cardData.Health = (int)(Random.Range(20,100) * _factor); 
                }

                if (_random.Next(0, 100) < _percentages[1] && abilityAmount < _maxAbilityAmount)
                {
                    abilityAmount += 1;
                    cardData.Speed = (Random.Range(0.1f, 1.0f) * _factor); 
                }

                if (_random.Next(0, 100) < _percentages[2] && abilityAmount < _maxAbilityAmount)
                {
                    abilityAmount += 1;
                    cardData.GunSpeed = Random.Range(0.5f, 1.0f) * _factor; 
                }
                
                if (_random.Next(0, 100) < _percentages[3] && abilityAmount < _maxAbilityAmount)
                {
                    abilityAmount += 1;
                    cardData.BulletSpeed = Random.Range(2f,10f) * _factor; 
                }

                if (_random.Next(0, 100) < _percentages[4] && abilityAmount < _maxAbilityAmount)
                {
                    abilityAmount += 1;
                    cardData.Damage = (int)(Random.Range(15, 50) * _factor); 
                }
                
                if (_random.Next(0, 100) < _percentages[5] && abilityAmount < _maxAbilityAmount)
                {
                    abilityAmount += 1;
                    cardData.Range = Random.Range(1, 5); 
                }
                
                if (_random.Next(0, 100) < _percentages[6] && abilityAmount < _maxAbilityAmount)
                {
                    abilityAmount += 1;
                    cardData.EnemyBulletSpeedDecreasePercentage = Random.Range(.7f, .99f); 
                }

                if (_random.Next(0, 100) < _percentages[7] && abilityAmount < _maxAbilityAmount)
                {
                    abilityAmount += 1;
                    cardData.EnemyDamageDecreasePercentage = Random.Range(.7f, .99f); 
                }
                
                if (_random.Next(0, 100) < _percentages[8] && abilityAmount < _maxAbilityAmount)
                {
                    abilityAmount += 1;
                    cardData.EnemySpeedDecreasePercentage = Random.Range(.7f, .99f); 
                }
                
                if (_random.Next(0, 100) < _percentages[9] && abilityAmount < _maxAbilityAmount)
                {
                    abilityAmount += 1;
                    cardData.ExperimentAmount = Random.Range(.5f, .10f); 
                }

                if (abilityAmount <= 0)
                {
                    cardData.Health = (int)(Random.Range(20,100) * _factor); 
                }
                
                wholeDeckData.Add(cardData);
            }

            return wholeDeckData;
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
using System;
using UnityEngine;

namespace _Project.Scripts.WalletSystem
{
    public class Wallet
    {
        private int _value = 0;    
        public event Action<int> OnScoreAdded;
        public event Action OnScoreAddedNotArg;
        public event Action<int> OnScoreRemove;
        public event Action OnScoreRemoveNotArg;
        public event Action OnFailRemoveScore;
        
        public int Value => _value;

        public Wallet(int value = 0)
        {
            _value = value;
        }

        public void AddValue(int value)
        {
            Debug.Log("WalletAddValue");
            _value +=  value;   
            OnScoreAdded?.Invoke(value);
            OnScoreAddedNotArg?.Invoke();
        }

        public bool CheckValue(int value)
        {
            if (_value>=value)
            {
                return true;
            }
            else
            {    
                return false;
            }     
        }

        public bool TryRemoveValue(int value)
        {
            if (_value>=value)
            {
                Remove(value); 
                return true;
            }
            else
            {    
                OnFailRemoveScore?.Invoke();
                return false;
            }
        }

        private void Remove(int value)
        {
            _value -=  value;   
            OnScoreRemove?.Invoke(value);
            OnScoreRemoveNotArg?.Invoke();
        }
    }
}
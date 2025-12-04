using ModestTree.Util;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Scripts.Ui.TowerHolderUi
{
    public class TowerHolderView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _towerIcon;
        [SerializeField] private TMP_Text _towerCost;


        public void Setup(Sprite icon, string cost)
        {
            _towerIcon.sprite = icon;
            _towerCost.text = cost;
        }

        public void AddActionOnClick(UnityAction action)
        {
            _button.onClick.AddListener(action);       
        }

        public void SetInteractable(bool interactable)
        {
            _button.interactable = interactable; 
        }
    }
}
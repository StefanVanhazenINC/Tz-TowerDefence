using TMPro;
using UnityEngine;

namespace _Project.Scripts.Ui.WalletUi
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _value;

        public void SetValue(string value)
        {
            _value.text = value;
        }
    }
}
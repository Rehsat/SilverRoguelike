using UnityEngine.UI;
using UnityEngine;

public class RingsWidget : MonoBehaviour
{
    [SerializeField] private RingsComponent _rings;
    [SerializeField] private Text _ringsText;
    private void Awake()
    {
        _rings.OnRingsChange += UpdateRingsText;
    }
    private void UpdateRingsText()
    {
        _ringsText.text = _rings.Rings.ToString();
    }
    private void OnDestroy()
    {
        _rings.OnRingsChange -= UpdateRingsText;
    }
}

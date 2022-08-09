using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class Blinker : MonoBehaviour
{
    [SerializeField] private int _blinksCount;
    [SerializeField] private float _blinkAlpha;
    [SerializeField] private float _waitDelta;

    [SerializeField] private bool _onStart;

    [SerializeField] private UnityEvent _onBlinkingEnd;
    private void Start()
    {
        if(_onStart)
        {
            StartBlinking();
        }
    }
    public void StartBlinking()
    {
        StartCoroutine(Blinking());
    }

    private IEnumerator Blinking()
    {
        var sprite = GetComponent<SpriteRenderer>();
        var color = sprite.material.color;
        var blinkColor = new Color(color.r, color.g, color.b,_blinkAlpha);

        for (int i = 1; i < _blinksCount+1; i++)
        {
            sprite.color = color;
            yield return new WaitForSeconds(_waitDelta / i);
            sprite.color = blinkColor; 
            yield return new WaitForSeconds(_waitDelta / i);
        }

        _onBlinkingEnd?.Invoke();
    }
}

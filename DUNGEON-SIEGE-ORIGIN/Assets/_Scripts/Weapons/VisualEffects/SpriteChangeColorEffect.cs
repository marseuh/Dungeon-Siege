using NaughtyAttributes;
using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteChangeColorEffect : MonoBehaviour, IAbilityVisualEffect
{
    [SerializeField] private float _playDuration = 0.4f;
    [OnValueChanged("DisplayPlayColor")]
    [SerializeField] private Color _playColor = Color.white;
    [OnValueChanged("DisplayPreviewColor")]
    [SerializeField] private Color _previewColor;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = _previewColor;
    }

    private IEnumerator ChangeSpriteColorForPlayDuration()
    {
        _renderer.color = _playColor;
        yield return new WaitForSeconds(_playDuration);
        _renderer.color = _previewColor;
    }

    public void Play()
    {
        StartCoroutine(ChangeSpriteColorForPlayDuration());
    }

    [Button]
    private void DisplayPlayColor()
    {
        if (_renderer == null)
        {
            _renderer = GetComponent<SpriteRenderer>();
        }
        _renderer.color = _playColor;

    }

    [Button]
    private void DisplayPreviewColor()
    {
        if (_renderer == null)
        {
            _renderer = GetComponent<SpriteRenderer>();
        }
        _renderer.color = _previewColor;
    }
}
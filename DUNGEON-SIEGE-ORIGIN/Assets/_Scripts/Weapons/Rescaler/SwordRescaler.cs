using NaughtyAttributes;
using UnityEngine;

public class SwordRescaler : MonoBehaviour, IRescaler
{
    [SerializeField] private float _ownerWidth;

    [BoxGroup("Debug")]
    [SerializeField] private float _debugRange;
    [BoxGroup("Debug")]
    [SerializeField] private Material _debugMaterial;

    private Material _material;
    private bool _matIsValid = false;

    private void Awake()
    {
        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null )
        {
            _matIsValid = false;
            return;
        }
        _material = _spriteRenderer.material;
        _matIsValid = true;
    }

    public void Rescale(float weaponRange)
    {
        if (!_matIsValid)
        {
            Debug.LogWarning("Sprite renderer's material is not valid");
            return;
        }

        float slashCut = _material.GetFloat("_XCut");

        float scale = weaponRange / (1.0f - slashCut);

        Vector3 newPos = transform.localPosition;
        newPos.x = -scale * slashCut;

        float slashWidth = _ownerWidth / (2 * scale * Mathf.Sin(Mathf.Acos(-slashCut)));

        transform.localPosition = newPos;
        transform.localScale = scale * Vector3.one;
        _material.SetFloat("_YScale", slashWidth);
    }

    [Button]
    public void DebugRescale()
    {
        _material = _debugMaterial;
        _matIsValid = true;
        Rescale(_debugRange);
        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.material = _material;
    }
}

using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TileAnimations))]
public class Tile : MonoBehaviour
{
    public bool IsEmpty
    {
        get => _isEmpty;
        set
        {
            _isEmpty = value;
            _animations.BlendColor(value ? _filledColor : _emptyColor, value ? _emptyColor : _filledColor);
        }
    }

    public void SetPathDirection(Vector2Int dir)
    {
        _text.transform.localRotation = GetDirectionAngle(dir);
        _text.gameObject.SetActive(true);
    }

    public void ClearPathDirection()
    {
        _text.gameObject.SetActive(false);
    }

    public Vector2Int Position { get; set; }

    public TileAnimations Animations => _animations;
    public TileButton Button => _button;

    private void Awake()
    {
        _animations = GetComponent<TileAnimations>();
        _button = GetComponent<TileButton>();
    }

    private Quaternion GetDirectionAngle(Vector2Int dir)
    {
        if (dir == Vector2Int.right)
            return Quaternion.Euler(90, 0, 0);
        if (dir == Vector2Int.left)
            return Quaternion.Euler(90, 180, 0);
        if (dir == Vector2Int.up)
            return Quaternion.Euler(90, 270, 0);
        if (dir == Vector2Int.down)
            return Quaternion.Euler(90, 90, 0);
        return Quaternion.identity;
    }

    [SerializeField] private Color _emptyColor;
    [SerializeField] private Color _filledColor;
    [SerializeField] private TMP_Text _text;
    
    private TileAnimations _animations;
    private TileButton _button;
    private bool _isEmpty = true;
}

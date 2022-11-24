using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TileButton : MonoBehaviour
{
    public UnityAction Pressed;
    public UnityAction<bool> Hovered;
    public Tile Tile => _tile;

    private void OnMouseDown()
    {
        Pressed?.Invoke();
        // _tile.Animations.SetSelected(false);
    }

    private void OnMouseEnter()
    {
        Hovered?.Invoke(true);
        _tile.Animations.SetSelected(true);
    }
    
    private void OnMouseExit()
    {
        Hovered?.Invoke(false);
        _tile.Animations.SetSelected(false);
    }

    private void Awake()
    {
        _tile = GetComponent<Tile>();
    }

    private Tile _tile;
}

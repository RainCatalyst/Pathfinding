using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        _tiles.TileClicked += (_) => FindPath();
    }

    private void FindPath()
    {
        List<Tile> path = _pathfinding.FindPath(_tiles.GetTile(_startPos.x, _startPos.y), _tiles.GetTile(_endPos.x, _endPos.y));
        _tiles.ClearTiles();
        for (int i = 0; i < path.Count - 1; i++)
        {
            print(path[i].Position);
            path[i].SetPathDirection(path[i + 1].Position - path[i].Position);
            path[i].Animations.FadeText(i * 0.025f);
        }
    }

    [SerializeField] private Vector2Int _startPos;
    [SerializeField] private Vector2Int _endPos;
    [SerializeField] private TileManager _tiles;
    [SerializeField] private PathfindingManager _pathfinding;
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TileManager : MonoBehaviour
{
    public UnityAction<Tile> TileClicked;
    
    public Tile GetTile(int x, int y) => _tiles[x, y];

    public Vector2Int Size => _gridSize;
    
    public List<Tile> GetNeighbours(Vector2Int pos)
    {
        List<Tile> tiles = new();
        if (pos.x - 1 > 0 && _tiles[pos.x - 1, pos.y].IsEmpty)
            tiles.Add(_tiles[pos.x - 1, pos.y]);
        if (pos.x + 1 < _gridSize.x && _tiles[pos.x + 1, pos.y].IsEmpty)
            tiles.Add(_tiles[pos.x + 1, pos.y]);
        if (pos.y - 1 > 0 && _tiles[pos.x, pos.y - 1].IsEmpty)
            tiles.Add(_tiles[pos.x, pos.y - 1]);
        if (pos.y + 1 < _gridSize.y && _tiles[pos.x, pos.y + 1].IsEmpty)
            tiles.Add(_tiles[pos.x, pos.y + 1]);
        return tiles;
    }

    public void ClearTiles()
    {
        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                _tiles[x, y].ClearPathDirection();
            }
        }
    }

    private void Start()
    {
        _tiles = new Tile[_gridSize.x, _gridSize.y];
        Vector3 offset = transform.position - new Vector3(_gridSize.x * 0.5f, 0, _gridSize.y * 0.5f) * _tileSize;
        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                Vector3 pos = offset + new Vector3(x, 0, y) * _tileSize;
                var tile = Instantiate(_tilePrefab, pos, Quaternion.identity, transform);
                tile.Position = new Vector2Int(x, y);
                _tiles[x, y] = tile;
                tile.Button.Pressed += () => OnTileClicked(tile);
            }
        }
    }

    private void OnTileClicked(Tile tile)
    {
        tile.IsEmpty = !tile.IsEmpty;
        TileClicked?.Invoke(tile);
    }

    private Tile[,] _tiles;

    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private float _tileSize;
}

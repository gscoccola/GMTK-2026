using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour, IStateSaver
{


    [Header("Parameters")]
    [SerializeField] private string SaveId;
    [SerializeField] private float _updateTime = 1f;


    [Header("References")]
    [SerializeField] private Tilemap _solidTilemap;
    [SerializeField] private Tile _plantTile;
    [SerializeField] private Tile _connectionTile;


    private TileType[,] _tileTypeGrid;
    private Directions[,] _spreadDirections;
    [SerializeField][ReadOnly] private List<Tile> _plantSources = new List<Tile>();
    [SerializeField][ReadOnly] private List<Tile> _plantConnections = new List<Tile>();
    private BoundsInt _bounds;
    private float _updateTimer;
    private Vector3Int _gridOrigin;
    private TilemapSavedState State;

    #region Setup

    public void SaveState()
    {
        PersistentData.Instance.SaveTilemapState(State, SaveId);
    }
    private void Awake()
    {
        PersistentData.Instance.AddSaver(this);
        GenerateMovementGrid();
    }

    private void GenerateMovementGrid()
    {
        /*_cellSize = _solidTilemap.cellSize.x;
        _cellSizeInverse = 1f / _cellSize;*/
        _bounds = _solidTilemap.cellBounds;
        TileBase[] solidTiles = _solidTilemap.GetTilesBlock(_bounds);
        _tileTypeGrid = new TileType[_bounds.size.x, _bounds.size.y];
        _spreadDirections = new Directions[_bounds.size.x, _bounds.size.y];

        int tileCount = _solidTilemap.GetTiles(_bounds, out Tilemap.PositionArray positions, out Tilemap.TileArray tiles);

        _gridOrigin = positions[0];
        for (int x = 0; x < _bounds.size.x; x++)
        {
            for (int y = 0; y < _bounds.size.y; y++)
            {
                _spreadDirections[x, y] = new Directions();
                TileBase wallTile = solidTiles[x + y * _bounds.size.x];
                if (wallTile != null)
                {
                    if (wallTile.name.Contains("building"))
                    {
                        _tileTypeGrid[x, y] = TileType.Building;
                    }
                    else if (wallTile.name.Contains("sand"))
                    {
                        _tileTypeGrid[x, y] = TileType.Sand;
                    }
                    else if (wallTile.name.Contains("resource_0"))
                    {
                        _tileTypeGrid[x, y] = TileType.Resource0;
                    }
                    else if (wallTile.name.Contains("resource_1"))
                    {
                        _tileTypeGrid[x, y] = TileType.Resource1;
                    }
                    else if (wallTile.name.Contains("resource_2"))
                    {
                        _tileTypeGrid[x, y] = TileType.Resource2;
                    }
                    else if (wallTile.name.Contains("resource_3"))
                    {
                        _tileTypeGrid[x, y] = TileType.Resource3;
                    }
                    else if (wallTile.name.Contains("plant"))
                    {
                        _tileTypeGrid[x, y] = TileType.PlantSource;
                        _plantSources.Add((Tile)wallTile);
                    }
                    else if (wallTile.name.Contains("connection"))
                    {
                        _tileTypeGrid[x, y] = TileType.PlantConnection;
                        //_plantConnections.Add((Tile)wallTile);
                    }
                    else
                    {
                        Debug.LogWarning($"Unknown tile type at ({x}, {y}): {wallTile.name}");
                        _tileTypeGrid[x, y] = TileType.Empty;
                    }
                }
                else _tileTypeGrid[x, y] = TileType.Empty;
            }
        }
    }
    #endregion

    #region Interface
    public Vector2Int WorldToGridPos(Vector3 worldPosition)
    {
        return new Vector2Int(Mathf.RoundToInt(worldPosition.x - _bounds.x),
            Mathf.RoundToInt(worldPosition.y - _bounds.y));
    }

    public Vector3 GridToWorldPos(Vector2Int gridPosition)
    {
        return new Vector3(gridPosition.x + _bounds.x,
            gridPosition.y + _bounds.y, 0f);
    }

    public Vector3 SnapToGrid(Vector3 worldPos)
    {
        return GridToWorldPos(WorldToGridPos(worldPos));
    }

    public TileType GetTileTypeAtPos(Vector2Int pos)
    {
        return _tileTypeGrid[pos.x, pos.y];
    }
    public TileType GetTileTypeAtPos(Vector3 pos)
    {
        return _tileTypeGrid[WorldToGridPos(pos).x, WorldToGridPos(pos).y];
    }

    /*public bool IsPosValid(MachineComponent comp, Vector3 worldPos)
    {
        TileType tileType = GetTileTypeAtPos(worldPos);
        return comp.ValidTiles.Contains(tileType);
    }*/
    #endregion

}


public enum TileType
{
    Empty,
    Sand,
    Building,
    Resource0,
    Resource1,
    Resource2,
    Resource3,
    PlantSource,
    PlantConnection,
}

public class Directions
{
    public List<Vector2Int> DirectionsList = new List<Vector2Int>();

    public int Count()
    {
        return DirectionsList.Count;
    }

    public Vector2Int GetRandomDirection()
    {
        if (DirectionsList.Count == 0) return Vector2Int.zero;
        return DirectionsList[Random.Range(0, DirectionsList.Count)];
    }

    public void Filter(Directions other)
    {
        List<Vector2Int> newList = new List<Vector2Int>();
        foreach (Vector2Int dir in DirectionsList)
        {
            if (other.DirectionsList.Contains(dir))
            {
                newList.Add(dir);
            }
        }
        DirectionsList = newList;
    } 
}

using System;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;
using UnityEngine;

public class PathfindingManager : MonoBehaviour
{
    public List<Tile> FindPath(Tile a, Tile b)
    {
        SimplePriorityQueue<Tile> queue = new();
        Dictionary<Tile, int> costSoFar = new();
        Dictionary<Tile, Tile> cameFrom = new();
        queue.Enqueue(a, 0);
        costSoFar.Add(a, 0);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current == b)
                break;

            print($"Looking for neighbours of {current.Position}");
            foreach (var next in _tileManager.GetNeighbours(current.Position))
            {
                print($"Neighbour {next.Position}");
                int newCost = costSoFar[current] + 1;
                if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                {
                    if (costSoFar.ContainsKey(next))
                        costSoFar[next] = newCost;
                    else
                        costSoFar.Add(next, newCost);
                    
                    int heuristic = Mathf.Abs(b.Position.x - next.Position.x) +
                                    Mathf.Abs(b.Position.y - next.Position.y);
                    int priority = newCost + heuristic;
                    queue.Enqueue(next, priority);
                    
                    if (cameFrom.ContainsKey(next))
                        cameFrom[next] = current;
                    else
                        cameFrom.Add(next, current);
                }
            }
        }

        List<Tile> path = new();
        if (cameFrom.ContainsKey(b))
        {
            Tile current = b;
            while (cameFrom.ContainsKey(current))
            {
                path.Add(current);
                current = cameFrom[current];
            }
            path.Add(a);
        }
        else
        {
            print("Path not found!");
        }

        path.Reverse();
        return path;
    }

    [SerializeField]
    private TileManager _tileManager;
}

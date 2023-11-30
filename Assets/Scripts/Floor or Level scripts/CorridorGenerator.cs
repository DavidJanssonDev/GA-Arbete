using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CorridorGenerator : MonoBehaviour
{
    private GridManager gridManager;
    public List<Transform> roomTransforms;

    public Tilemap groundTilemap;
    public Tilemap wallTilemap;

    public Tile groundTile;
    public RuleTile wallRuleTile;

    public Sprite desiredSprite;  // Specify the desired sprite in the Unity Editor

    void Start()
    {
        // Get room transforms from the FloorValueScript component
        roomTransforms = GetComponent<FloorValueScript>().RoomGameObjects;
        gridManager = new GridManager(roomTransforms);

        // Choose a specific room as the starting point
        Transform startRoom = roomTransforms[0]; // Change this to select a specific room

        // Initialize the grid manager with the selected start room and generate corridors
        GenerateCorridors(startRoom);
    }

    void GenerateCorridors(Transform startRoom)
    {
        Debug.Log($"Generating corridors from room: {startRoom.name}");

        // Get the starting node based on the selected room
        Node startNode = gridManager.GetClosestNode(startRoom.position);

        // Find the goal room based on distance and the desired sprite
        Transform goalRoom = GetClosestRoom(startRoom, roomTransforms, desiredSprite);

        if (goalRoom != null)
        {
            Debug.Log($"Processing goal room: {goalRoom.name}");

            // Get the goal node for the current room
            Node goalNode = gridManager.GetClosestNode(goalRoom.position);

            // Find a path and generate corridor tiles
            List<Node> path = gridManager.FindPath(startNode, goalNode);

            if (path != null)
            {
                GenerateCorridorTiles(path);
            }
        }
    }

    Transform GetClosestRoom(Transform startRoom, List<Transform> candidateRooms, Sprite desiredSprite)
    {
        // Find the closest room with the desired sprite based on distance from the starting room
        Transform closestRoom = null;
        float minDistance = float.MaxValue;

        foreach (Transform roomTransform in candidateRooms)
        {
            if (roomTransform != startRoom)
            {
                // Check if the room has the desired sprite
                SpriteRenderer spriteRenderer = roomTransform.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null && spriteRenderer.sprite == desiredSprite)
                {
                    float distance = Vector3.Distance(startRoom.position, roomTransform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestRoom = roomTransform;
                    }
                }
            }
        }

        return closestRoom;
    }

    void GenerateCorridorTiles(List<Node> path)
    {
        foreach (Node node in path)
        {
            Vector3Int tilePosition = groundTilemap.WorldToCell(node.Position);
            groundTilemap.SetTile(tilePosition, groundTile);
            GenerateWallTiles(tilePosition);
        }

        Debug.Log("Created Ground Tiles");
    }

    void GenerateWallTiles(Vector3Int groundTilePosition)
    {
        // Adjust this logic based on your corridor width and wall configuration.
        for (int i = -2; i <= 2; i++)
        {
            Vector3Int wallTilePosition = groundTilePosition + new Vector3Int(i, 0, 0);
            wallTilemap.SetTile(wallTilePosition, wallRuleTile);
        }

        Debug.Log("Created Wall Tiles");
    }
}
public class GridManager
{
    private List<Node> nodes;

    public GridManager(List<Transform> roomTransforms) {
       
        nodes = new List<Node>();
        foreach (Transform roomTransform in roomTransforms) {

            Node node = new() { Position = roomTransform.position };
            nodes.Add(node);
        }
        ConnectNodes();
    }

    private void ConnectNodes() {
        Debug.Log("Connecting nodes...");
        foreach (Node nodeA in nodes) {

            foreach (Node nodeB in nodes){

                if (nodeA != nodeB) {

                    nodeA.Neighbors.Add(nodeB);
                }
            }
        }
    }

    public Node GetClosestNode(Vector3 position) {

        Node closestNode = null;
        float minDistance = float.MaxValue;

        foreach (Node node in nodes) {

            float distance = Vector3.Distance(position, node.Position);
            if (distance < minDistance){

                minDistance = distance;
                closestNode = node;
            }
        }

        return closestNode;
    }

    public Node GetRandomNode() {

        return nodes[Random.Range(0, nodes.Count)];
    }

    public List<Node> FindPath(Node startNode, Node goalNode) {
        
        List<Node> openList = new() { startNode };
        List<Node> closedList = new();

        // Inside the FindPath method in the GridManager class
        while (openList.Count > 0)
        {
            Node currentNode = openList[0];
            openList.RemoveAt(0);
            closedList.Add(currentNode);

            if (currentNode == goalNode)
            {
                Debug.Log("Path found!");
                return ReconstructPath(startNode, goalNode);
            }

            foreach (Node neighbor in currentNode.Neighbors)
            {
                if (closedList.Contains(neighbor))
                {
                    continue;
                }

                float tentativeGScore = currentNode.GScore + Distance(currentNode, neighbor);
                if (!openList.Contains(neighbor) || tentativeGScore < neighbor.GScore)
                {
                    neighbor.Parent = currentNode;
                    neighbor.GScore = tentativeGScore;
                    neighbor.HScore = HeuristicCostEstimate(neighbor, goalNode);

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                        openList.Sort((n1, n2) => n1.FScore.CompareTo(n2.FScore));
                    }
                }
            }
        }

        Debug.Log("No path found!");
        return null;
    }

    private List<Node> ReconstructPath(Node startNode, Node goalNode) {
        
        List<Node> path = new();
        Node currentNode = goalNode;

        while (currentNode != startNode) {

            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        path.Reverse();
        return path;
    }

    private float Distance(Node nodeA, Node nodeB) {
        
        return Vector3.Distance(nodeA.Position, nodeB.Position);
    }

    private float HeuristicCostEstimate(Node node, Node goalNode) {
        return Vector3.Distance(node.Position, goalNode.Position);
    }
}

public class Node {
    public Vector3 Position;
    public List<Node> Neighbors = new List<Node>();
    public float GScore;
    public float HScore;
    public float FScore => GScore + HScore;
    public Node Parent;
}

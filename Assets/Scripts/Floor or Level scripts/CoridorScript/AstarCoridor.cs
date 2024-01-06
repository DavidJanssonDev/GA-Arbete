using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using RoomStuff;

namespace Astar {

    public class Node {

        public List<Node> grannar = new();
        
        public GameObject tileGameObject;
        public bool traversable; // if the node is a obsecal or not (true it is, false it is not)
        public Node parentNodeToStart = null; 

        public Node(GameObject tileGObject, Sprite tileSprite) {
            tileGameObject = tileGObject;
            
            if (tileSprite != null)
            {
                traversable = false; 
            } 
            else 
            {
                traversable = true;
            }
            
      
        }

        public void GetNabour()
        {

        }


    }

    public class AstarCoridor : MonoBehaviour
    {
        public List<Node> NodesListOpen = new();
        public List<Node> NodesListClose = new();
        
        public Tilemap mapOverAllTiles;



        public void StartAlgorithm() {
            CreateTileMapOverAllTiles();
            CreateAllNodes();
        }
        
        public void CreateTileMapOverAllTiles() {

            Tilemap groundTilemap = GameObject.FindGameObjectWithTag("Main ground tilemap").GetComponent<Tilemap>();
            Tilemap wallTilemap = GameObject.FindGameObjectWithTag("Main wall tilemap").GetComponent<Tilemap>();

            TilemapScript.CopyTileMapToTilemap(mapOverAllTiles, wallTilemap);
            TilemapScript.CopyTileMapToTilemap(mapOverAllTiles, groundTilemap);
        }

        public void CreateAllNodes() {

            var bounds = mapOverAllTiles.cellBounds;

            for (int tileX = bounds.x; tileX < bounds.x + bounds.size.x; tileX++) {

                for (int tileY = bounds.y; tileY < bounds.y + bounds.size.y; tileY++) {

                    var cellPosition = new Vector3Int(tileX, tileY, 0);
                    var sourceTile = mapOverAllTiles.GetTile(cellPosition);

                    Tile currentTile = (sourceTile as Tile);

                    var newNodeTile = new Node(currentTile.gameObject,currentTile.sprite);

                    NodesListOpen.Add(newNodeTile);
                }
            }
        }
    }

}



using JigsawGame.Board;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JigsawGame.Tile
{
    public class TileController
    {
        private TileView tileView;
        private BoardService boardService;

        private Vector2 correctPosition;
        private Vector2 size;

     

        public int ID { get; set; }
        public int CurrentIndex { get; set; }
        public bool IsSelected{get;set;}
        public TileController(TileView tileViewPrefab)
        {
            tileView = UnityEngine.Object.Instantiate(tileViewPrefab);
            tileView.SetController(this);
        }
        public void Init(BoardService boardService)
        {
            this.boardService = boardService;
        }
        public void SetPosition(Vector3 spawnPosition) 
        { 
            tileView.transform.position = spawnPosition;
           
        }

        public void SetSprite(Sprite spriteToSet)
        {
            tileView.SetSpriteRenderer(spriteToSet);
        }

        public void SetSize(Vector2 sizeToSet) 
        { 
            size = sizeToSet;
            tileView.SetColliderSize(size);
            tileView.SetColliderOffset(new Vector2(sizeToSet.x/2, sizeToSet.y/2));
        }
      
        public void SetCorrectPosition(Vector2 positionToSet) => correctPosition = positionToSet;

        public void OnTileClickDown()
        {
            if (tileView.ValidateClickAction())
            {
                IsSelected = true;
               
            }
        }

        public void OnTileClickUp()
        {

            if (tileView.ValidateClickAction())
            {
                IsSelected = false;
                Debug.Log("Swap Valid position begin");
                if (boardService.ValidateTilePosition(tileView.transform.position))
                {
                    Debug.Log("Swap Valid position ");

                    TileController dropTile = boardService.GetTileControllerByPosition(tileView.transform.position);
                    if (dropTile != null)
                    {
                        boardService.SwapTilePosition(this, dropTile);
                        Debug.Log("Swap Valid position " + dropTile.CurrentIndex + " " + CurrentIndex);
                    }
                    else
                    {
                        tileView.SetPreviousPosition();
                    }
                }
                else
                {
                    //tileView.SetPreviousPosition();
                    boardService.ResetTilePosition(this);
                    Debug.Log("Invalid position");
                }
                //Debug.Log(ID);
            }
            else { boardService.ResetTilePosition(this); }
            
        }

        
    }

}
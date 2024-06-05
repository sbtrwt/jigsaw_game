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

        private Vector3 positionOffset = new Vector3(0.0f, 0.0f, 0.0f);

        public int ID { get; set; }
        public bool IsSelected{get;set;}
        public TileController(TileView tileViewPrefab)
        {
            tileView = UnityEngine.Object.Instantiate(tileViewPrefab);
            tileView.SetController(this);
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
                Debug.Log(ID);
            }
        }

        public void OnTileClickUp()
        {
            if (tileView.ValidateClickAction())
            {
                IsSelected = false;
                Debug.Log(ID);
            }
        }

        public void SetPositionOffset(Vector2 posToSet)
        {
            positionOffset = posToSet;
        }
    }

}
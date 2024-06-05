using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JigsawGame.Tile
{
    public class TileController
    {
        private TileView tileView;

        public TileController(TileView tileViewPrefab)
        {
            tileView = Object.Instantiate(tileViewPrefab);
            tileView.SetController(this);
        }
        public void SetPosition(Vector3 spawnPosition) => tileView.transform.position = spawnPosition;


    }

}
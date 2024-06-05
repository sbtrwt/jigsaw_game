using JigsawGame.Events;
using JigsawGame.Tile;
using JigsawGame.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JigsawGame.Board
{
    public class BoardService
    {
        private EventService eventService;
        private BoardSO boardSO;
        private List<TileController> allTiles;

        private int tileHeight;
        private int tileWidth;
        private int selectedImageIndex;
        public BoardService(BoardSO boardSO)
        {
            this.boardSO = boardSO;
            allTiles = new List<TileController>();
            tileHeight = boardSO.HeightInPixel / boardSO.RowCount;
            tileWidth = boardSO.WidthInPixel / boardSO.ColumnCount;
            selectedImageIndex = 0;
        }

        public void Init(EventService eventService)
        {
            this.eventService = eventService;
            SubscriveEvents();
        }

        private void SubscriveEvents()
        {
            eventService.OnGameStart.AddListener(OnGameStart);
        }
        public void OnGameStart(int level)
        {
            ConfigureTiles();
            SetCameraPosition();
        }

        private void ConfigureTiles()
        {
            Texture2D boarderTexture = SpriteHandler.LoadTexture(boardSO.ImagesPath[selectedImageIndex]);
            
            int minScale = (boardSO.RowCount < boardSO.ColumnCount) ? boardSO.RowCount : boardSO.ColumnCount;
            int totalTileCount = minScale * minScale;
            TileController tempTileController;
            Sprite tempSprite;
            Vector2 tempPosition;
            

            for (int i = 0; i < totalTileCount; i++)
            {
                tempTileController = new TileController(boardSO.TilePrefab)
                {
                    ID = i + 1
                };
               
                tempPosition = new Vector2((i % minScale) * tileWidth, (i / minScale) * tileHeight);
                tempTileController.SetPosition(tempPosition);
                tempTileController.SetCorrectPosition(tempPosition);
                tempTileController.SetSize(new Vector2(tileWidth, tileHeight));
                tempSprite = SpriteHandler.CreateSpriteFromTexture2D(boarderTexture,
                                                                    (int)tempPosition.x,
                                                                     (int)tempPosition.y,
                                                                    tileWidth,
                                                                    tileHeight);

                tempTileController.SetSprite(tempSprite);
                allTiles.Add(tempTileController);
            }
        }

        void SetCameraPosition()
        {
            Camera.main.transform.position = new Vector3(boardSO.WidthInPixel / 2, boardSO.HeightInPixel / 2, -10.0f);

            int smaller_value = Mathf.Min(boardSO.WidthInPixel, boardSO.HeightInPixel);
            Camera.main.orthographicSize = smaller_value * 0.9f;
        }
    }

}
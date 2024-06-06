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
        private Transform boardContainer;

        private int tileHeight;
        private int tileWidth;
        private int selectedImageIndex;
        private int minScale;
        public TileSorting TileSorting { get; set; } 
        public BoardService(BoardSO boardSO, Transform boardContainer)
        {
            this.boardSO = boardSO;
            this.boardContainer = boardContainer;
            allTiles = new List<TileController>();
            tileHeight = boardSO.HeightInPixel / boardSO.RowCount;
            tileWidth = boardSO.WidthInPixel / boardSO.ColumnCount;
            selectedImageIndex = 0;
            minScale = (boardSO.RowCount < boardSO.ColumnCount) ? boardSO.RowCount : boardSO.ColumnCount;
        }

        public void Init(EventService eventService)
        {
            this.eventService = eventService;
            SubscriveEvents();
        }

        private void SubscriveEvents()
        {
            eventService.OnGameStart.AddListener(OnGameStart);
            eventService.OnTileSuffle.AddListener(OnTileSuffle);
        }
        public void OnGameStart(int level)
        {
            ConfigureTiles();
            SetCameraPosition();
        }

        private void ConfigureTiles()
        {
            Texture2D boarderTexture = SpriteHandler.LoadTexture(boardSO.ImagesPath[selectedImageIndex]);


            int totalTileCount = minScale * minScale;
            TileController tempTileController;
            Sprite tempSprite;
            Vector2 tempPosition;


            for (int i = 0; i < totalTileCount; i++)
            {
                tempTileController = new TileController(boardSO.TilePrefab, boardContainer)
                {
                    ID = i + 1,
                    CurrentIndex = i
                };
                tempTileController.Init(this);

                tempPosition = GetPositionByIndex(i);
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


        private void SetCameraPosition()
        {
            Camera.main.transform.position = new Vector3(boardSO.WidthInPixel / 2, boardSO.HeightInPixel / 2, -10.0f);

            int smaller_value = Mathf.Min(boardSO.WidthInPixel, boardSO.HeightInPixel);
            Camera.main.orthographicSize = smaller_value * 0.9f;
        }

        public bool ValidateTilePosition(Vector2 posToValidate)
        {
            float xOffset = minScale * tileWidth;
            float yOffset = minScale * tileHeight;

            return (posToValidate.x >= 0 && posToValidate.x <= xOffset) && (posToValidate.y >= 0 && posToValidate.y <= yOffset);
        }

        public void SwapTilePosition(TileController tileController1, TileController tileController2)
        {
            int tempIndex = tileController1.CurrentIndex;
            tileController1.CurrentIndex = tileController2.CurrentIndex;
            tileController2.CurrentIndex = tempIndex;
            tileController1.SetPosition(GetPositionByIndex(tileController1.CurrentIndex));
            tileController2.SetPosition(GetPositionByIndex(tileController2.CurrentIndex));
        }
        private Vector3 GetPositionByIndex(int index)
        {
            return new Vector3((index % minScale) * tileWidth, (index / minScale) * tileHeight, 3);
        }

        public TileController GetTileControllerByPosition(Vector2 positionToFind)
        {
            Vector2 tempPostion;
            TileController resultTile = null;
            foreach (var tile in allTiles)
            {
                tempPostion = GetPositionByIndex(tile.CurrentIndex);
                //if((positionToFind - tempPostion).sqrMagnitude < tileWidth * tileWidth)
                if ((positionToFind.x > (tempPostion.x - tileWidth / 1.3f)
                    && positionToFind.x < (tempPostion.x + tileWidth / 1.3f))
                    && (positionToFind.y >= (tempPostion.y - tileHeight / 1.3f)
                    && positionToFind.y <= (tempPostion.y + tileHeight / 1.3f)))
                {
                    resultTile = tile;
                    break;
                }

            }
            return resultTile;
        }

        public void ResetTilePosition(TileController tileController)
        {
            tileController.SetPosition(GetPositionByIndex(tileController.CurrentIndex));
        }

        public void OnTileSuffle(bool isSuffle)
        {
            int totalTileCount = minScale * minScale;
            int tileIndex1, tileIndex2;
            for (int i = 0; i < totalTileCount; i++)
            {
                tileIndex1 = Random.Range(0, totalTileCount - 1);
                tileIndex2 = Random.Range(0, totalTileCount - 1);
                if(tileIndex1 != tileIndex2)
                {
                    SwapTilePosition(allTiles[tileIndex1], allTiles[tileIndex2]);
                }
            }
        }

        public bool IsValidCorrectPosition()
        {
            bool isCorrect = false;
            foreach(var tile in allTiles)
            {
                isCorrect = tile.IsCorrectPosition;
                if (!tile.IsCorrectPosition) break;
            }
            return isCorrect;
        }

        public void ValidateGameOver()
        {
            if (IsValidCorrectPosition())
            {
                eventService.OnGameOver.InvokeEvent(true);
            }
        }
    }

}
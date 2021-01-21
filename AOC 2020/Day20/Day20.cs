using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC_2020
{
    public class Day20
    {
        MainProgram mainProgram = new MainProgram();

        public List<Tile> Tiles = new List<Tile>();
        public int[,] PlacingOfPieces;
        public char[,] FullPicture;

        private int PictureSize;

        public Day20(string path)
        {
            var paragraphs = mainProgram.ConvertFileToParagraphs(path);

            foreach (var paragraph in paragraphs)
            {
                var tileAndId = mainProgram.SplitParagraphByLineEndings(paragraph);
                string id = tileAndId[0].Split(' ')[1].Replace(':', new char());
                tileAndId.Remove(tileAndId[0]);
                var tile = mainProgram.ConvertLinesToTwoDimensionalCharArray(tileAndId);
                Tiles.Add(new Tile { Index = int.Parse(id), Picture = tile });
            }
            var sizePictures = (int)Math.Sqrt(Tiles.Count);
            PlacingOfPieces = new int[sizePictures, sizePictures];

            PictureSize = Tiles.First().Picture.GetLength(0) - 2;
        }

        public void RunDay20Part1()
        {
            var answer = GetCornerMultiplication();

            Console.WriteLine($"Ids of corner tiles multiplied leads to {answer}");
        }

        public long GetCornerMultiplication()
        {
            long multiplication = 0;
            FillNeighbors();
            var cornerTiles = GetCornerTiles();

            if (cornerTiles.Count != 4)
            {
                throw new ArgumentException("Not the right amount of corner tiles");
            }
            foreach (var tile in cornerTiles)
            {
                if (multiplication == 0)
                    multiplication = tile.Index;
                else
                {
                    multiplication = multiplication * tile.Index;
                }
            }
            return multiplication;
        }

        private List<Tile> GetCornerTiles()
        {
            List<Tile> cornerTiles = new List<Tile>();
            foreach (var tile in Tiles)
            {
                if ((tile.PossibleBottomNeighbors.Count == 1 || tile.PossibleTopNeighbors.Count == 1)
                    && (tile.PossibleLeftNeighbors.Count == 1 || tile.PossibleRightNeighbors.Count == 1))
                    cornerTiles.Add(tile);
            }

            return cornerTiles;
        }

        private void FillNeighbors()
        {
            foreach (var referenceTile in Tiles)
            {
                var test = Tiles.Where(t => t.Index == 3593).Count();
                var referenceTileSides = referenceTile.GetSidesWithoutReverse();

                foreach (var compareTile in Tiles)
                {
                    var compareSides = compareTile.GetSides();
                    foreach (var side in referenceTileSides)
                    {
                        if (compareSides.Values.Contains(side.Value))
                        {
                            switch (side.Key)
                            {
                                case Side.LeftNormal:
                                    referenceTile.PossibleLeftNeighbors.Add(compareTile.Index, compareSides.Where(x => x.Value == side.Value).Single().Key);
                                    break;
                                case Side.RightNormal:
                                    referenceTile.PossibleRightNeighbors.Add(compareTile.Index, compareSides.Where(x => x.Value == side.Value).Single().Key);
                                    break;
                                case Side.TopNormal:
                                    referenceTile.PossibleTopNeighbors.Add(compareTile.Index, compareSides.Where(x => x.Value == side.Value).Single().Key);
                                    break;
                                case Side.BottomNormal:
                                    referenceTile.PossibleBottomNeighbors.Add(compareTile.Index, compareSides.Where(x => x.Value == side.Value).Single().Key);
                                    break;
                            }
                        }
                    }
                }
            }
            var sides = 0;
            var corners = 0;

            foreach(var tile in Tiles)
            {
                if (tile.PossibleRightNeighbors.Count > 2)
                    throw new ArgumentException();
                if (tile.PossibleTopNeighbors.Count > 2)
                    throw new ArgumentException();
                if (tile.PossibleLeftNeighbors.Count > 2)
                    throw new ArgumentException();
                if (tile.PossibleBottomNeighbors.Count > 2)
                    throw new ArgumentException();
            }
            foreach(var tile in Tiles)
            {
                var connections = 0;
                if (tile.PossibleRightNeighbors.Count == 2)
                    connections++;
                if (tile.PossibleTopNeighbors.Count == 2)
                    connections++;
                if (tile.PossibleLeftNeighbors.Count == 2)
                    connections++;
                if (tile.PossibleBottomNeighbors.Count == 2)
                    connections++;
                if(connections == 3)
                {
                    sides++;
                }
                if(connections == 2)
                {
                    corners++;
                }
                if (connections == 1 || connections == 0)
                    throw new ArgumentException();


            }

            List<Tile> test3 = Tiles.Where(x => x.PossibleBottomNeighbors.Keys.Contains(3449) || x.PossibleLeftNeighbors.Keys.Contains(3449) ||
            x.PossibleRightNeighbors.Keys.Contains(3449) ||
            x.PossibleTopNeighbors.Keys.Contains(3449)).ToList();
        }

        public void RunDay20Part2()
        {
            var notSeaMonster = GetNotSeaMonster();

            Console.WriteLine($"There are {notSeaMonster} waves in the water");
        }

        public int GetNotSeaMonster()
        {
            CreateFullPicture();

            var seaMonsters = 0;
            var originalArray = FullPicture;
            var flippedArray = FlipHorizontally(FullPicture);
            var countTurns = 0;

            while(seaMonsters == 0 && countTurns < 4)
            {
                var seaMonstersOriginal = FindSeaMonsters(originalArray);
                var seaMonstersFlipped = FindSeaMonsters(flippedArray);

                if (seaMonstersOriginal != 0)
                    seaMonsters = seaMonstersOriginal;
                if (seaMonstersFlipped != 0)
                    seaMonsters = seaMonstersFlipped;

                originalArray = RotateArray(originalArray);
                flippedArray = RotateArray(flippedArray);

                countTurns++;
            }

            var countWaves = 0;

            for (var x = 0; x < FullPicture.GetLength(0); x++)
                for (var y = 0; y < FullPicture.GetLength(0); y++)
                {
                    if (FullPicture[x, y] == '#')
                        countWaves++;
                }
            
            


            return countWaves - (seaMonsters*15);
        }

        private int FindSeaMonsters(char[,] array)
        {
            var countSeaMonsters = 0;

            var maxIndex = array.GetLength(0) - 1;

            for (int w = 0; w < maxIndex; w++)
                for (int h = 0; h < maxIndex; h++)
                {
                    if (h + 2 < maxIndex && w + 20 < maxIndex)
                    {
                        var searchField = new char[20, 3];
                        for (int x = 0; x < 20; x++)
                            for (int y = 0; y < 3; y++)
                            {
                                searchField[x, y] = array[x + w, y + h];
                            }
                        var necessaryFields = new List<char> { searchField[0,1], searchField[1,0],searchField[4,0], searchField[5,1],
                                    searchField[6,1], searchField[7,0], searchField[10,0], searchField[11,1], searchField[12,1], searchField[13,0],
                                    searchField[16,0], searchField[17,1], searchField[18,1], searchField[18,2], searchField[19,1] };
                        if (necessaryFields.Where(f => f == '#').Count() == 15)
                            countSeaMonsters++;
                    }

                }

            return countSeaMonsters;
        }

        public char[,] CreateFullPicture()
        {
            var size = (int)Math.Sqrt(Tiles.Count);
            var tileSize = Tiles.First().Picture.GetLength(0) - 2;

            FullPicture = new char[size * tileSize, size * tileSize];

            FillNeighbors();

            PlaceFirstCornerTile();

            var test = Tiles.Where(x => x.Index == 3191).ToList();
            var test2 = Tiles.Where(x => x.Index == 2309).ToList();

            for (int w = 0; w < size; w++)
            {
                for (int h = 0; h < size; h++)
                {
                    PlaceNeighbors(w, h);
                }
            }

            return FullPicture;
        }

        private void PlaceNeighbors(int w, int h)
        {
            var index = PlacingOfPieces[w, h];
            var highestIndex = PlacingOfPieces.GetLength(0) - 1;
            
            var tile = Tiles.Where(t => t.Index == index).Single();
            var tileFlipped = (tile.HorizontallyFlipped || tile.VerticallyFlipped);
            for (int i = tile.Turned; i > 0; i = i - 90)
            {
                var leftNeighbor = tile.PossibleLeftNeighbors;
                var rightNeighbor = tile.PossibleRightNeighbors;
                var topNeighbor = tile.PossibleTopNeighbors;
                var bottomNeighbor = tile.PossibleBottomNeighbors;

                tile.PossibleLeftNeighbors = bottomNeighbor;
                tile.PossibleRightNeighbors = topNeighbor;
                tile.PossibleTopNeighbors = leftNeighbor;
                tile.PossibleBottomNeighbors = rightNeighbor;
            }
            if (tile.HorizontallyFlipped)
            {
                var topNeighbor = tile.PossibleTopNeighbors;
                var bottomNeighbor = tile.PossibleBottomNeighbors;

                tile.PossibleTopNeighbors = bottomNeighbor;
                tile.PossibleBottomNeighbors = topNeighbor;
            }
            if (tile.VerticallyFlipped)
            {
                var leftNeighbor = tile.PossibleLeftNeighbors;
                var rightNeighbor = tile.PossibleRightNeighbors;

                tile.PossibleLeftNeighbors = rightNeighbor;
                tile.PossibleRightNeighbors = leftNeighbor;
            }

            if (tile.PossibleRightNeighbors.Count != 1 && w < highestIndex)
            {
                var rightNeighborWithSide = tile.PossibleRightNeighbors.Where(x => x.Key != tile.Index).Single();
                var rightNeighborTile = Tiles.Where(t => t.Index == rightNeighborWithSide.Key).SingleOrDefault();
                if(rightNeighborTile == null)
                {
                    return;
                }
                switch (rightNeighborWithSide.Value)
                {
                    case Side.LeftReverse:
                        rightNeighborTile.HorizontallyFlipped = tileFlipped;
                        break;
                    case Side.BottomReverse:
                        rightNeighborTile.HorizontallyFlipped = tileFlipped;
                        rightNeighborTile.Turned = 90;
                        break;
                    case Side.RightReverse:
                        rightNeighborTile.HorizontallyFlipped = tileFlipped;
                        rightNeighborTile.Turned = 180;
                        break;
                    case Side.TopReverse:
                        rightNeighborTile.HorizontallyFlipped = tileFlipped;
                        rightNeighborTile.Turned = 270;
                        break;

                    case Side.LeftNormal:
                        rightNeighborTile.HorizontallyFlipped = !tileFlipped;
                        break;
                    case Side.BottomNormal:
                        rightNeighborTile.HorizontallyFlipped = !tileFlipped;
                        rightNeighborTile.Turned = 90;
                        break;
                    case Side.RightNormal:
                        rightNeighborTile.HorizontallyFlipped = !tileFlipped;
                        rightNeighborTile.Turned = 180;
                        break;                   
                    case Side.TopNormal:
                        rightNeighborTile.HorizontallyFlipped = !tileFlipped;
                        rightNeighborTile.Turned = 270;
                        break;
                    
                    default:
                        throw new ArgumentException("Something went wrong");
                }

                if (PlacingOfPieces[w + 1, h] == 0)
                {
                    PlacingOfPieces[w + 1, h] = rightNeighborTile.Index;

                    PlaceTileIn2DGrid(rightNeighborTile, ((w + 1) * PictureSize), (h * PictureSize));
                }
            }
            if (tile.PossibleTopNeighbors.Count != 1 && h < highestIndex)
            {
                var topNeighborWithSide = tile.PossibleTopNeighbors.Where(x => x.Key != tile.Index).Single();
                var topNeighborTile = Tiles.Where(t => t.Index == topNeighborWithSide.Key).SingleOrDefault();
                if (topNeighborTile == null)
                {
                    return;
                }
                switch (topNeighborWithSide.Value)
                {
                    case Side.BottomReverse:
                        topNeighborTile.VerticallyFlipped = tileFlipped;
                        break;
                    case Side.RightReverse:
                        topNeighborTile.Turned = 90;
                        topNeighborTile.VerticallyFlipped = tileFlipped;
                        break;
                    case Side.TopReverse:
                        topNeighborTile.VerticallyFlipped = tileFlipped;
                        topNeighborTile.Turned = 180;
                        break;
                    case Side.LeftReverse:
                        topNeighborTile.VerticallyFlipped = tileFlipped;
                        topNeighborTile.Turned = 270;
                        break;

                    case Side.BottomNormal:
                        topNeighborTile.VerticallyFlipped = !tileFlipped;
                        break;
                    case Side.RightNormal:
                        topNeighborTile.Turned = 90;
                        topNeighborTile.VerticallyFlipped = !tileFlipped;
                        break;
                    case Side.TopNormal:
                        topNeighborTile.VerticallyFlipped = !tileFlipped;
                        topNeighborTile.Turned = 180;
                        break;
                    case Side.LeftNormal:
                        topNeighborTile.VerticallyFlipped = !tileFlipped;
                        topNeighborTile.Turned = 270;
                        break;                    
                }

                if (PlacingOfPieces[w, h + 1] == 0)
                {
                    PlacingOfPieces[w, h + 1] = topNeighborTile.Index;

                    PlaceTileIn2DGrid(topNeighborTile, (w * PictureSize), ((h + 1) * PictureSize));
                }

                if (tile.PossibleBottomNeighbors.Count > 2 || tile.PossibleLeftNeighbors.Count > 2)
                    throw new ArgumentException();

                Tiles.Remove(tile);
            }
        }

        private void PlaceFirstCornerTile()
        {
            var cornerTiles = GetCornerTiles();
            var leftBottom = cornerTiles.Where(x => x.PossibleLeftNeighbors.Count == 1 && x.PossibleBottomNeighbors.Count == 1).First();
            PlacingOfPieces[0, 0] = leftBottom.Index;

            PlaceTileIn2DGrid(leftBottom, 0, 0);
        }

        private void PlaceTileIn2DGrid(Tile tile, int startingWidth, int startingHeighth)
        {
            if (startingWidth < 0)
                startingWidth = 0;
            if (startingHeighth < 0)
                startingHeighth = 0;

            for(int i = tile.Turned; i > 0; i = i-90)
            {
                tile.Picture = RotateArray(tile.Picture);
            }
            if (tile.HorizontallyFlipped)
            {
                tile.Picture = FlipHorizontally(tile.Picture);
            }
            if (tile.VerticallyFlipped)
            {
                tile.Picture = FlipVertically(tile.Picture);
            }

            for (int w = 1; w < tile.Picture.GetLength(0) - 1; w++)
                for (int h = 1; h < tile.Picture.GetLength(1) - 1; h++)
                {
                    FullPicture[w - 1 + startingWidth, h - 1 + startingHeighth] = tile.Picture[w, h];
                }
        }

        public char[,] FlipHorizontally(char[,] originalArray)
        {
            var length = originalArray.GetLength(0);

            var flippedArray = new char[length, length];

            for (int w = 0; w < length; w++)
                for (int h = 0; h < length; h++)
                {
                    flippedArray[w, length - 1 - h] = originalArray[w, h];
                }

            return flippedArray;
        }

        private char[,] FlipVertically(char[,] originalArray)
        {
            var length = originalArray.GetLength(0);

            var flippedArray = new char[length, length];

            for (int w = 0; w < length; w++)
                for (int h = 0; h < length; h++)
                {
                    flippedArray[length - w - 1, h] = originalArray[w, h];
                }

            return flippedArray;
        }

        private char[,] RotateArray(char[,] originalArray)
        {
            var length = originalArray.GetLength(0);

            var rotatedArray = new char[length, length];

            for(int w =0; w< length; w++)
                for (int h = 0; h < length; h++)
                {
                    rotatedArray[h, length - 1 - w] = originalArray[w, h];
                }

            return rotatedArray;
        }
    }
}


public class Tile
{
    public Tile()
    {
        PossibleBottomNeighbors = new Dictionary<int, Side>();
        PossibleLeftNeighbors = new Dictionary<int, Side>();
        PossibleRightNeighbors = new Dictionary<int, Side>();
        PossibleTopNeighbors = new Dictionary<int, Side>();
    }

    private Dictionary<Side, string> _sides = new Dictionary<Side, string>();
    private Dictionary<Side, string> _reverseSides = new Dictionary<Side, string>();

    public int Index { get; set; }
    public char[,] Picture { get; set; }
    public bool HorizontallyFlipped { get; set; }
    public bool VerticallyFlipped { get; set; }
    public int Turned { get; set; }

    public Dictionary<Side, string> GetSides()
    {
        var line = string.Empty;

        if (_sides.Count == 0)
        {
            _sides.Add(Side.LeftNormal, CreateLeftSide());
            _sides.Add(Side.RightNormal, CreateRightSide());
            _sides.Add(Side.TopNormal, CreateTopSide());
            _sides.Add(Side.BottomNormal, CreateBottomSide());
        }
        if (_reverseSides.Count == 0)
        {
            _reverseSides.Add(Side.LeftReverse, Reverse(_sides[Side.LeftNormal]));
            _reverseSides.Add(Side.RightReverse, Reverse(_sides[Side.RightNormal]));
            _reverseSides.Add(Side.TopReverse, Reverse(_sides[Side.TopNormal]));
            _reverseSides.Add(Side.BottomReverse, Reverse(_sides[Side.BottomNormal]));
        }

        return _sides.Concat(_reverseSides).ToDictionary(x => x.Key, x => x.Value);
    }

    public Dictionary<Side, string> GetSidesWithoutReverse()
    {
        var line = string.Empty;

        if (_sides.Count == 0)
        {
            _sides.Add(Side.LeftNormal, CreateLeftSide());
            _sides.Add(Side.RightNormal, CreateRightSide());
            _sides.Add(Side.TopNormal, CreateTopSide());
            _sides.Add(Side.BottomNormal, CreateBottomSide());
        }

        return _sides;
    }

    private string CreateTopSide()
    {
        string side = string.Empty;
        for (var i = 0; i < Picture.GetLength(0); i++)
        {
            side = side.Insert(side.Length, Picture[i, Picture.GetLength(1) - 1].ToString());
        }
        return side;
    }

    private string CreateBottomSide()
    {
        string side = string.Empty;
        for (var i = Picture.GetLength(0) - 1; i >= 0; i--)
        {
            side = side.Insert(side.Length, Picture[i, 0].ToString());
        }
        return side;
    }

    private string CreateLeftSide()
    {
        string side = string.Empty;
        for (var i = 0; i < Picture.GetLength(1); i++)
        {
            side = side.Insert(side.Length, Picture[0, i].ToString());
        }
        return side;
    }

    private string CreateRightSide()
    {
        string side = string.Empty;
        for (var i = Picture.GetLength(1) - 1; i >= 0; i--)
        {
            side = side.Insert(side.Length, Picture[Picture.GetLength(1) - 1, i].ToString());
        }
        return side;
    }

    private static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    public Dictionary<int, Side> PossibleLeftNeighbors { get; set; }
    public Dictionary<int, Side> PossibleRightNeighbors { get; set; }
    public Dictionary<int, Side> PossibleTopNeighbors { get; set; }
    public Dictionary<int, Side> PossibleBottomNeighbors { get; set; }

}

        public enum Side
        {
            LeftNormal,
            LeftReverse,
            TopNormal,
            TopReverse,
            RightNormal,
            RightReverse,
            BottomNormal,
            BottomReverse
        }


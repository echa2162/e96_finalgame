using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheGrid : MonoBehaviour
{
    // Start is called before the first frame update


	public int columns = 0;
	public int rows = 0;
public float squaresGap = 0.1f;
public GameObject gridSquare;
public Vector2 startPosition = new Vector2(0.0f, 0.0f);
public float squareScale = 0.5f;
public float everySquareOffset = 0.0f;

private Vector2 _offset = new Vector2(0.0f, 0.0f);
private List<GameObject> _gridSquares = new List<GameObject>();
    void Start()
    {
        CreateGrid();
    }

    // Update is called once per frame
    private void CreateGrid()
    {
        SpawnGridSquares();
		SetGridSquarePositions();
    }

    private void SpawnGridSquares()
    {
        int square_index = 0;
        for (var row = 0; row < rows; ++row)
        {
            for (var column = 0; column < columns; ++column)
            {
                _gridSquares.Add(Instantiate(gridSquare) as GameObject);
                _gridSquares[_gridSquares.Count -1].transform.SetParent(this.transform);
                _gridSquares[_gridSquares.Count - 1].transform.localScale =
                    new Vector3(squareScale, squareScale, squareScale);
                _gridSquares[_gridSquares.Count - 1].GetComponent<GridSquare>().SetImage(square_index % 2 == 0);
                square_index++;
            }
        }
    }
    private void SetGridSquarePositions()
    {
        int col_num = 0;
        int row_num = 0;
        Vector2 square_gap_number = new Vector2(0.0f, 0.0f);
        bool row_moved = false;
        var square_rect = _gridSquares[0].GetComponent<RectTransform>();
        _offset.x = square_rect.rect.width * square_rect.transform.localScale.x + everySquareOffset;
        _offset.y = square_rect.rect.height * square_rect.transform.localScale.y + everySquareOffset;
        foreach (GameObject square in _gridSquares)
        {
            if (col_num + 1 > columns)
            {
                square_gap_number.x = 0;
                col_num = 0;
                row_num++;
                row_moved = false;
            }

            var pos_x_offset = _offset.x * col_num + (square_gap_number.x * squaresGap);
            var pos_y_offset = _offset.y * row_num + (square_gap_number.y * squaresGap);
            if (col_num > 0 && col_num % 3 == 0)
            {
                square_gap_number.x++;
                pos_x_offset += squaresGap;
            }
            if (row_num > 0 && row_num % 3 == 0 && row_moved == false)
            {
                row_moved = true;
                square_gap_number.y++;
                pos_y_offset += squaresGap;
                
            }

            square.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(startPosition.x + pos_x_offset, startPosition.y - pos_y_offset);
            square.GetComponent<RectTransform>().localPosition = new Vector3(startPosition.x + pos_x_offset,
                startPosition.y - pos_y_offset, 0.0f);
        }
    }
}

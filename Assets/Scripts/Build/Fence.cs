using UnityEngine;

public class Fence : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private GameObject _boardPrefab;
    [SerializeField] private float _boardWidth = 0.1f;

    private GameObject _board;
    private Fence _otherFence;
    private Vector3 _boardStartPosition;
    private Vector3 _boardEndPosition;
    private bool _isDrawingBoard;

    private void Update()
    {
        if (!_isDrawingBoard)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

            foreach (Collider collider in colliders)
            {
                Fence otherFence = collider.GetComponent<Fence>();
                if (otherFence != null && otherFence != this)
                {
                    _otherFence = otherFence;
                    DrawBoard();
                    break;
                }
            }
        }
        else
        {
            UpdateBoard();
        }
    }

    private void DrawBoard()
    {
        _boardStartPosition = transform.position;
        _boardEndPosition = _otherFence.transform.position;
        Vector3 boardScale = new Vector3(_boardWidth, Vector3.Distance(_boardStartPosition, _boardEndPosition), 1f);

        _board = Instantiate(_boardPrefab, _boardStartPosition, Quaternion.identity);
        _board.transform.up = _boardEndPosition - _boardStartPosition;
        _board.transform.localScale = boardScale;

        _isDrawingBoard = true;
    }

    private void UpdateBoard()
    {
        if (_otherFence == null)
        {
            DestroyBoard();
            return;
        }

        _boardEndPosition = _otherFence.transform.position;
        Vector3 boardScale = new Vector3(_boardWidth, Vector3.Distance(_boardStartPosition, _boardEndPosition), 1f);
        _board.transform.up = _boardEndPosition - _boardStartPosition;
        _board.transform.localScale = boardScale;
    }

    private void DestroyBoard()
    {
        Destroy(_board);
        _isDrawingBoard = false;
    }
}



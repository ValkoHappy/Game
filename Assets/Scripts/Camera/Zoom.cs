public class Zoom
{
    private int _zoomCount = 0;
    private int _minZoom = 0;
    private int _maxZoom = 2;
    private int _zOffset = 50;
    private int _xOffset = 25;
    private float _left;
    private float _right;
    private float _top;
    private float _bottom;

    public int ZoomCount => _zoomCount;
    public int MinZoom => _minZoom;
    public int MaxZoom => _maxZoom;
    public float Top => _top;
    public float Bottom => _bottom;
    public float Left => _left;
    public float Right => _right;

    public Zoom(float leftBound, float rightBound, float topBound, float bottomBound)
    {
        _left = leftBound;
        _right = rightBound;
        _top = topBound;
        _bottom = bottomBound;
    }

    public void ZoomIn()
    {
        _zoomCount++;
        _top += _zOffset;
        _right += _xOffset;
        _left -= _xOffset;
    }

    public void ZoomOut()
    {
        _zoomCount--;
        _top -= _zOffset;
        _right -= _xOffset;
        _left += _xOffset;
    }
}
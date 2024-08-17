namespace Scripts.Camera
{
    public class Zoom
    {
        private float _left;
        private float _right;
        private float _top;
        private float _bottom;

        public Zoom(float leftBound, float rightBound, float topBound, float bottomBound)
        {
            _left = leftBound;
            _right = rightBound;
            _top = topBound;
            _bottom = bottomBound;
        }

        public float Top => _top;
        public float Bottom => _bottom;
        public float Left => _left;
        public float Right => _right;
    }
}
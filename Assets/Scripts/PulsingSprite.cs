using UnityEngine;
using UnityEngine.UI;

namespace HaveYouGotAMoment
{
    [RequireComponent (typeof (SpriteRenderer))]
    public class PulsingSprite : MonoBehaviour
    {
        public float Duration = 1.0f;
		[Range(0.0f, 1.0f)]
		public float AlphaVariance = .5f;

        private SpriteRenderer _spriteRenderer;
        private float _startingAlpha;
        private bool _fadeOut;

		// Start is called before the first frame update
		void Start()
        {
			_spriteRenderer = GetComponent<SpriteRenderer>();

            _startingAlpha = _spriteRenderer.color.a;
            _fadeOut = true;
		}

        // Update is called once per frame
        void Update()
        {
            float newImageAlpha = _spriteRenderer.color.a;

			if (_fadeOut)
			{
                if (_spriteRenderer.color.a > (_startingAlpha - (_startingAlpha * AlphaVariance)))
                {
                    newImageAlpha -= (Time.deltaTime / Duration);
                }
                else
                {
                    _fadeOut = false;
                }
			}
            else
			{
				if (_spriteRenderer.color.a < _startingAlpha)
				{
					newImageAlpha += (Time.deltaTime / Duration);
				}
				else
				{
					_fadeOut = true;
				}
			}


			_spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, newImageAlpha);
		}
    }
}

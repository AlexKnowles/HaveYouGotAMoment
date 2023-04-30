using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaveYouGotAMoment.Managers
{
    public class DayManager : MonoBehaviour
    {
        public int GameHoursInRealSeconds = 30;
		public int PlayerShiftStartGameHour = 7;
        public int PlayerShiftEndGameHour = 18;
        public int GameDayLengthInHours = 24;

        public float CurrentGameTime = 0;
        public int CurrentGameDay = 0;

        public float FadeTimeInGameHours = 0.10f;

        public GameObject BlackoutSprite;
        public GameObject StartNewDayButton;
        private SpriteRenderer _blackoutSpriteRenderer;
        private Color _baseColor;

        private bool _timeRunning = true;
        
		// Start is called before the first frame update
		void Start()
        {
            if (_blackoutSpriteRenderer == null)
            {
                _blackoutSpriteRenderer = BlackoutSprite.GetComponent<SpriteRenderer>();
                _baseColor = _blackoutSpriteRenderer.color;
                _blackoutSpriteRenderer.color = new Color(_baseColor.r, _baseColor.g, _baseColor.b, 1.0f);
            }
            StartNewDay();
		}

        // Update is called once per frame
        void Update()
        {
            if (_timeRunning)
            {
                CurrentGameTime += (Time.deltaTime / GameHoursInRealSeconds);
            }
            var (inFadeTime, opacity) = InStartOrEndOfShift();
            if (inFadeTime)
            {
                _blackoutSpriteRenderer.color = new Color(_baseColor.r, _baseColor.g, _baseColor.b, opacity);
            }
            if (!inFadeTime && _blackoutSpriteRenderer.color.a > 0f)
            {
                _blackoutSpriteRenderer.color = new Color(_baseColor.r, _baseColor.g, _baseColor.b, 0f);
            }

            if(CurrentGameTime >= PlayerShiftEndGameHour)
            {
                StartNewDayButton.SetActive(true);
                EndDay();
			}
        }

        private (bool, float) InStartOrEndOfShift()
        {
            if (CurrentGameTime > PlayerShiftEndGameHour - FadeTimeInGameHours)
            {
                var diff = (CurrentGameTime - PlayerShiftEndGameHour + FadeTimeInGameHours) / FadeTimeInGameHours;
                return (true, diff);
            }
            if (CurrentGameTime < PlayerShiftStartGameHour + FadeTimeInGameHours)
            {
                var diff =  (FadeTimeInGameHours - (CurrentGameTime - PlayerShiftStartGameHour)) / FadeTimeInGameHours;
                return (true, diff);
            }
            return (false, 0f);
        }

        public void StartNewDay()
		{
			CurrentGameDay++;
			CurrentGameTime = PlayerShiftStartGameHour;
            StartNewDayButton.SetActive(false);
            _timeRunning = true;
		}

        private void EndDay()
        {
            _timeRunning = false;
		}
    }
}
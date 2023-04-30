using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaveYouGotAMoment
{
    public class Window : MonoBehaviour
    {
        private Managers.DayManager _dayManager;
        private SpriteRenderer _skyRenderer;

        public Color[] _skyColors = new Color[]
        {
            new Color( 0.05420079f, 0.08420169f, 0.1320755f, 1f ),
            new Color( 0.9622642f, 0.9070576f, 0.503827f, 1f ),
            new Color( 0.5019608f, 0.700487f, 0.9607843f, 1f ),
            new Color( 0.9716981f, 0.6380719f, 0.2245906f, 1f )
        };

        public GameObject Sky;
        // Start is called before the first frame update
        void Start()
        {
            if (_dayManager == null)
            {
                _dayManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<Managers.DayManager>();
            }


            if (_skyRenderer == null)
            {
                _skyRenderer = Sky.GetComponent<SpriteRenderer>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            float currentHour = _dayManager.CurrentGameTime;
            int colorIndex = Mathf.FloorToInt(currentHour / (24f / _skyColors.Length));
            int secondColorIndex = colorIndex + 1;
            if (secondColorIndex > (_skyColors.Length - 1))
            {
                secondColorIndex = 0;
            }
            float t = (currentHour % (24f / _skyColors.Length))/ (24f / _skyColors.Length);
            _skyRenderer.color = Color.Lerp(_skyColors[colorIndex], _skyColors[secondColorIndex], t);
        }
    }
}

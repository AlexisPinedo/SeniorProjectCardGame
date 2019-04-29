using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SA
{
    public class CardDisplay : MonoBehaviour
    {
        public Card card;

        public CardDisplayProperties[] properties;

        //[SerializeField] private Text nameText;
        //[SerializeField] private Text descriptionText;
        //[SerializeField] private Text effectText;

        //[SerializeField] private Text costText;
        //[SerializeField] private Text currencyText;
        //[SerializeField] private Text attackText;

        //[SerializeField] private Image artImage;
        //[SerializeField] private Image elementImage;


        void Start()
        {
            if (card == null)
                return;

            for (int i = 0; i < properties.Length; i++)
            {
                CardProperties cp = card.properties[i];

                CardDisplayProperties p = GetProperty(cp.element);

                if (p == null)
                    continue;

                if (cp.element is ElementInt)
                {
                    p.text.text = cp.intValue.ToString();
                }
                else if (cp.element is ElementText)
                {
                    p.text.text = cp.stringValue;
                }
                else if (cp.element is ElementImage)
                {
                    p.img.sprite = cp.sprite;
                }
            }

        }

        public CardDisplayProperties GetProperty(Element e)
        {
            CardDisplayProperties result = null;

            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].element == e)
                {
                    result = properties[i];
                    break;
                }
            }

            return result;
        }
    }
}

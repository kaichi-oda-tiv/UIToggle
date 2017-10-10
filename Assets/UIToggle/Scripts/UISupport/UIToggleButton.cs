using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UISupport
{

    public class UIToggleButton : MonoBehaviour
    {
        [SerializeField]
        Button[] defaultSelectButtons;

        List<Button> toggleButtons = new List<Button>();

        Dictionary<Button, ColorBlock> buttonColorBlocks = new Dictionary<Button, ColorBlock>();

        // Button.Colors
        // normal : 何もしていない
        // highlight : onmouseover
        // pressed : onmousedown
        // disable : intaractive == false;

        private void OnEnable()
        {
            Initialize();
        }

        private void OnDisable()
        {
            RemoveClickListener();
        }

        private void OnDestroy()
        {
            RemoveClickListener();
        }

        void RemoveClickListener()
        {
            toggleButtons.Clear();
        }

        void Initialize(params Button[] enabledList)
        {
            toggleButtons.Clear();
            var buttons = gameObject.GetComponentsInChildren<Button>().ToList();
            List<Button> enabled = enabledList.ToList();

            buttons.ForEach(b =>
            {
                b.onClick.AddListener(() => OnClickEventListener(b));
                ColorBlock cb = new ColorBlock();
                cb = b.colors;
                buttonColorBlocks.Add(b,cb);

                if( enabled.Contains(b) )
                {
                    SetSelectableColor(b);
                }
                else
                {
                    SetUnselectableColor(b);
                }
            });

            toggleButtons = buttons;
        }


        void OnClickEventListener(Button b)
        {
            SetSelectableColor(b);
        }

        void SetSelectableColor(Button b)
        {
            buttonColorBlocks.Keys.Where(kb => kb.colors.normalColor == buttonColorBlocks[kb].normalColor).ToList().ForEach(db => SetUnselectableColor(db));

            // TODO: tintして欲しい
            var cb = b.colors;
            cb.normalColor = buttonColorBlocks[b].normalColor;
            b.colors = cb;
        }

        void SetUnselectableColor(Button b)
        {
            // TODO: tintして欲しい
            var cb = b.colors;
            cb.normalColor = buttonColorBlocks[b].disabledColor;
            b.colors = cb;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

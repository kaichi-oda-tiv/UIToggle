using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UISupport
{
    public class UIToggle : MonoBehaviour
    {
        [SerializeField]
        Toggle defaultSelect;

        List<Toggle> toggleList = new List<Toggle>();
        Dictionary<Toggle, bool> localState = new Dictionary<Toggle, bool>();

        private void OnEnable()
        {
            Initialize(defaultSelect);
        }

        private void OnDisable()
        {
        }

        private void OnDestroy()
        {
            
        }

        void Initialize(Toggle selected = null)
        {
            toggleList.Clear();

            var tl = GetComponentsInChildren<Toggle>().ToList();

            tl.ForEach(toggle =>
            {
                
                toggle.isOn = selected == toggle;
                toggle.onValueChanged.AddListener((state) => OnValueChangeEvent(toggle, state)); // isOnで値を入れられてから設定
                localState.Add(toggle, toggle.isOn);
            });

            toggleList = tl;
        }

        void OnValueChangeEvent(Toggle t,bool state)
        {
//            toggleList.Where(lt => (lt != t)).ToList().ForEach(lt => lt.isOn = false); // これ最後に選択したToggle保持してそいつをfalseにすれば良いんじゃないかなぁ...
            
            t.isOn = localState[t] | state;
            Debug.LogFormat("{0} => {1}({2},{3})",t.name,state,t.isOn,localState[t]);
            localState[t] = t.isOn;
        }

    }
}

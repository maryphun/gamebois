using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiAnimation : MonoBehaviour
{
    //マウスがのった時
    public void Event()
    {
        // Textコンポーネントを取得
        Text text = this.GetComponent<Text>();

        // 色を指定(黒)
        text.color = Color.black;
        
    }

    //マウスが離れたとき
    public void Undo()
    {
        // Textコンポーネントを取得
        Text text = this.GetComponent<Text>();

        // 色を指定(白)
        text.color = Color.white;
    }
}

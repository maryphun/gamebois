using UnityEngine;
using System.Collections;
using UnityEngine.UI; 
public class WhiteAnimation : MonoBehaviour
{
    private bool _gage;
    public float _speed = 4;

    Slider _slider;
    GameObject _imageObject;
    public Image _fill;

    float whiteGage = 0;
    float imageAlpha  = 0;

    void Start()
    {
        // スライダーを取得する
        _slider = this.GetComponent<Slider>();
        //_imageObject = transform.Find("Fill").gameObject;
        _fill = _imageObject.GetComponent<Image>();
    }

    public void Update()
    {
        if (_gage)
        {
            if (whiteGage <= 1)
            {
                // 上昇
                whiteGage += 1/_speed;

                if (whiteGage > 1) whiteGage = 1;

                // ゲージに値を設定
                _slider.value = whiteGage;
            }
        }
        else
        {
            if (imageAlpha >= 0.0f)
            {
                // 減少
                imageAlpha -= 1 / _speed;

                var tempColor = _fill.color;
                tempColor.a = imageAlpha;
                _fill.color = tempColor;

                if (imageAlpha <= 0.0f)
                {
                    whiteGage = 0.0f;
                    _slider.value = whiteGage;
                    tempColor.a = 1.0f;
                    _fill.color = tempColor;
                }
            }
        }
    }
    
    //マウスカーソルが乗ったら白くする
    public void RiseGage()
    {
        _gage = true;
        whiteGage = 0.5f;
        var tempColor = _fill.color;
        tempColor.a = 1.0f;
        _fill.color = tempColor;
    }

    //マウスカーソルが離れたら黒に戻す
    public void DecreaseGage()
    {
        _gage = false;
        whiteGage = 1.0f;
        imageAlpha = 1.0f;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenTransition : MonoBehaviour
{
    //表示させたいオブジェクト
    public GameObject TransitionObject;
    

    //画面を遷移
    public void TransitionObjectScreen()
    {
        //非アクティブのオブジェクトを表示
        TransitionObject.SetActive(true);
    }


    //戻る
    public void BackScreen()
    {
        //表示されている画面を非表示にする
        TransitionObject.SetActive(false);
    }













    /* 参考　
    GameObject MenuScreen;

    private void Start()
    {
        //MenuScreenオブジェクトを取得
        MenuScreen = GameObject.Find("MenuScreen");
    }

    //設定画面に遷移
    public void StartOptionScreen()
    {
        //MenuScreenオブジェクトをfalse
        MenuScreen.SetActive(false);
    }

    //メニュー画面に戻る
    public void BackScreen()
    {
        //MenuScreenオブジェクトをtrue
        MenuScreen.SetActive(true);
    }

    //マルチプレイを開始する
    public void StartMultiPlay()
    {
        //あとで同じシーンにまとめる
        SceneManager.LoadScene("Menu");
    }

    */
}



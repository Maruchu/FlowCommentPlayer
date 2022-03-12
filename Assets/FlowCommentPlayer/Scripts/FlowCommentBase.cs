/*
 * ニコニコ動画風コメント再生スクリプト
 * FlowCommentPlayer
 * 
 * Copyright(C) 2022 ㊥Maruchu
 * 
 * This software is released under the MIT License.
 * http://opensource.org/licenses/mit-license.php
 */


using UnityEngine;
using System.Collections.Generic;



/// <summary>
/// 流れるコメント
/// </summary>
public class FlowCommentBase : MonoBehaviour
{
    public List<TextMesh> textMeshList = new List<TextMesh>();                      //テキストのリスト
    private Vector3 currentPosition = Vector3.zero;                                 //表示位置
    private float moveSpeedX = 0;                                                   //移動速度
    private float textWidth = 16f;                                                  //テキスト幅

    private static readonly Vector3 screenMax = new Vector3(256f, 256f, 16f);       //画面端情報
    private static readonly Vector2 colorRange = new Vector2(0.9f, 1.0f);           //文字の色のレンジ
    private static readonly Vector2 moveSpeedRange = new Vector2(150f, 200f);       //移動速度のレンジ

    private FlowCommentPlayer parentPlayer = null;                                  //コメントの取得先
    public static readonly float rareColorPercent = 0.05f;                          //レア色の出る確率 (0.0～1.0)
    private static readonly List<Color> rareColorList = new List<Color>() {         //レア色のリスト
        Color.red,
        Color.green,
        Color.blue,
        Color.cyan,
        Color.magenta,
        Color.yellow,
        Color.black,
    };

    /// <summary>
    /// 開始時
    /// </summary>
    private void Start()
    {
        //テキスト取得
        if (textMeshList.Count < 0)
        {
            //なければ無視
            Destroy(gameObject);
        }
        //リセット
        Reset();
    }
    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        //位置更新
        currentPosition.x += (moveSpeedX * Time.deltaTime);
        transform.position = currentPosition;
        //消えきった？
        if (currentPosition.x < -(screenMax.x * 2))
        {
            //位置を戻す
            Reset();
        }
    }


    /// <summary>
    /// プレーヤー設定
    /// </summary>
    public void SetPlayer(FlowCommentPlayer tempPlayer)
    {
        parentPlayer = tempPlayer;
    }
    /// <summary>
    /// コメントを差し替え
    /// </summary>
    private void SetText(string commentText)
    {
        //テキスト差し替え
        foreach (var tempText in textMeshList)
        {
            tempText.text = commentText;
        }
    }
    /// <summary>
    /// リセット
    /// </summary>
    private void Reset()
    {
        //文字と位置を設定
        SetText(parentPlayer.commentTextList[Random.Range(0, parentPlayer.commentTextList.Length)]);
        //位置をバラす
        currentPosition = new Vector3((screenMax.x + (Random.value * screenMax.x * 3)), (Random.Range(-screenMax.y, screenMax.y)), (Random.value * screenMax.z));
        //速度をバラす
        moveSpeedX = -Random.Range(moveSpeedRange.x, moveSpeedRange.y);
        //色をバラす
        float alphaVal = Random.Range(colorRange.x, colorRange.y);
        Color tempColor = (Color.white * alphaVal);
        if (Random.value < rareColorPercent)
        {
            ///レア色
            tempColor = rareColorList[Random.Range(0, rareColorList.Count)];
            currentPosition.z = 0;
        }
        textMeshList[0].color = tempColor;
        //幅を取得
        var tempRenderer = textMeshList[0].gameObject.GetComponent<MeshRenderer>();
        textWidth = tempRenderer.bounds.extents.x;
    }
}

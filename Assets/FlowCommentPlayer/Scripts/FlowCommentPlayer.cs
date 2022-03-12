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


/// <summary>
/// ニコニコ動画風コメント再生スクリプト
/// </summary>
public class FlowCommentPlayer : MonoBehaviour
{
    public GameObject flowCommentPrefab = null;                 //コメントプレハブ
    public int createFlowCommentCount = 128;                    //流すコメント数

    public string[] commentTextList = new string[]{             //流すコメントの文章
        "wwwwwwwwww",
        "wwwwww",
        "www",
        "888888888",
        "888888",
        "8888",
    };


    /// <summary>
    /// 起動時
    /// </summary>
    private void Awake()
    {
        //コメントを生成
        for (int i = 0; i < createFlowCommentCount; i++)
        {
            var tempObj = Instantiate(flowCommentPrefab);
            tempObj.transform.parent = transform;
            var tempComment = tempObj.GetComponent<FlowCommentBase>();
            tempComment.SetPlayer(this);
        }
    }
}

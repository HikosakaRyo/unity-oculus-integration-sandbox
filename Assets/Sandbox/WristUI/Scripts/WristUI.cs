using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;
using Oculus.Interaction.HandGrab.Visuals;

/// <summary>
/// 手首周辺に表示するUI
/// </summary>
public class WristUI : MonoBehaviour
{
    public OVRCameraRigRef ovrCameraRigRef;
    public bool isLeftHand;
    public List<HandVisual> handVisuals;

    // Editモードでanchorの位置を調整するために利用する手のグラフィックス（左）
    // Playmodeでは非表示になる。
    [SerializeField]
    [HideInInspector]
    private HandGhost _handGhostLeft;

    // Editモードでanchorの位置を調整するために利用する手のグラフィックス（右）
    [SerializeField]
    [HideInInspector]
    private HandGhost _handGhostRight;

    [SerializeField]
    [HideInInspector]
    private Transform _anchor;

    private Transform _centerEyeAnchor;
    /// <summary>
    /// 現在有効なHandVisual
    /// handVisualsから有効なものが選択される（複数ある場合は最初の要素優先）
    /// </summary>
    private HandVisual _currentHandVisual;

    void Start()
    {
        this._centerEyeAnchor = ovrCameraRigRef.CameraRig.centerEyeAnchor;
        _handGhostLeft.gameObject.SetActive(false);
        _handGhostRight.gameObject.SetActive(false);
    }

    void Update()
    {
        // ハンドトラッキング <=> コントローラ切り替わりのタイミングで参照先のOculusHandを変更する
        if (_currentHandVisual == null || !_currentHandVisual.Hand.IsHighConfidence)
        {
            UpdateCurrentHandVisual();
            UpdateParentToCurrentHandVisual();
        }
        // 有効なHandが存在しない場合は隠す
        if (_currentHandVisual == null)
        {
            _anchor.gameObject.SetActive(false);
            return;
        }
        // UIがカメラ方向を向いていないときは隠す
        var angle = Vector3.Angle(
            _centerEyeAnchor.forward,
            _anchor.forward);
        bool hide = angle >= -90 && angle <= 90;
        _anchor.gameObject.SetActive(hide);
    }

    /// <summary>
    /// _currentHandVisualを更新
    /// </summary>
    void UpdateCurrentHandVisual()
    {
        _currentHandVisual = null;
        foreach (var v in handVisuals)
        {
            if (v == null)
            {
                continue;
            }
            if (v.Hand.IsHighConfidence)
            {
                _currentHandVisual = v;
                return;
            }
        }
    }

    /// <summary>
    /// UIのルートをcurrentHandVisualの位置に移動
    /// </summary>
    void UpdateParentToCurrentHandVisual()
    {
        if (_currentHandVisual == null)
        {
            return;
        }
        var handRootTr = _currentHandVisual.GetTransformByHandJointId(HandJointId.HandWristRoot).parent;
        var rotZ = isLeftHand ? 0 : 180;
        var tr = this.transform;
        tr.parent = handRootTr;
        tr.localRotation = Quaternion.Euler(0, 0, rotZ);
        tr.localPosition = new Vector3(0, 0, 0);
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            return;
        }
        UnityEditor.EditorApplication.delayCall += _OnValidate;
    }

    private void _OnValidate()
    {
        // 位置調整用のghostHandの左右切り替え
        UnityEditor.EditorApplication.delayCall -= _OnValidate;
        if (this == null) return;
        if (isLeftHand)
        {
            _handGhostLeft.gameObject.SetActive(true);
            _handGhostRight.gameObject.SetActive(false);
        }
        else
        {
            _handGhostLeft.gameObject.SetActive(false);
            _handGhostRight.gameObject.SetActive(true);
        }
    }
#endif

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;
using Oculus.Interaction.HandGrab.Visuals;

/// <summary>
/// �����ӂɕ\������UI
/// </summary>
public class WristUI : MonoBehaviour
{
    public OVRCameraRigRef ovrCameraRigRef;
    public bool isLeftHand;
    public List<HandVisual> handVisuals;

    // Edit���[�h��anchor�̈ʒu�𒲐����邽�߂ɗ��p�����̃O���t�B�b�N�X�i���j
    // Playmode�ł͔�\���ɂȂ�B
    [SerializeField]
    [HideInInspector]
    private HandGhost _handGhostLeft;

    // Edit���[�h��anchor�̈ʒu�𒲐����邽�߂ɗ��p�����̃O���t�B�b�N�X�i�E�j
    [SerializeField]
    [HideInInspector]
    private HandGhost _handGhostRight;

    [SerializeField]
    [HideInInspector]
    private Transform _anchor;

    private Transform _centerEyeAnchor;
    /// <summary>
    /// ���ݗL����HandVisual
    /// handVisuals����L���Ȃ��̂��I�������i��������ꍇ�͍ŏ��̗v�f�D��j
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
        // �n���h�g���b�L���O <=> �R���g���[���؂�ւ��̃^�C�~���O�ŎQ�Ɛ��OculusHand��ύX����
        if (_currentHandVisual == null || !_currentHandVisual.Hand.IsHighConfidence)
        {
            UpdateCurrentHandVisual();
            UpdateParentToCurrentHandVisual();
        }
        // �L����Hand�����݂��Ȃ��ꍇ�͉B��
        if (_currentHandVisual == null)
        {
            _anchor.gameObject.SetActive(false);
            return;
        }
        // UI���J���������������Ă��Ȃ��Ƃ��͉B��
        var angle = Vector3.Angle(
            _centerEyeAnchor.forward,
            _anchor.forward);
        bool hide = angle >= -90 && angle <= 90;
        _anchor.gameObject.SetActive(hide);
    }

    /// <summary>
    /// _currentHandVisual���X�V
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
    /// UI�̃��[�g��currentHandVisual�̈ʒu�Ɉړ�
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
        // �ʒu�����p��ghostHand�̍��E�؂�ւ�
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

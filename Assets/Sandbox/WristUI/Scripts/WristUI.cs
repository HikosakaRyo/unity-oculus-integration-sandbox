using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;
using Oculus.Interaction.HandGrab.Visuals;

#if UNITY_EDITOR
using UnityEditor;
#endif


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
        _centerEyeAnchor = ovrCameraRigRef.CameraRig.centerEyeAnchor;
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

    [MenuItem("GameObject/WristUI")]
    public static void CreateWristUIObject(MenuCommand menuCommand)
    {
        GameObject newObj = new GameObject("WristUI");
        GameObject anchor = new GameObject("anchor");
        anchor.transform.parent = newObj.transform;
        anchor.transform.localPosition = new Vector3(-0.05f, 0.06f, 0);
        anchor.transform.localRotation = Quaternion.Euler(90, 90, 0);

        var wristUI = newObj.AddComponent<WristUI>();
        var leftHandPrefab = AssetDatabase.LoadAssetAtPath<HandGhost>(
            "Assets/Oculus/Interaction/Runtime/Prefabs/HandGrab/Ghost-LeftHand.prefab");
        var rightHandPrefab = AssetDatabase.LoadAssetAtPath<HandGhost>(
            "Assets/Oculus/Interaction/Runtime/Prefabs/HandGrab/Ghost-RightHand.prefab");
        var leftHand = Instantiate(leftHandPrefab, newObj.transform);
        var rightHand = Instantiate(rightHandPrefab, newObj.transform);

        leftHand.transform.localPosition = new Vector3(0, 0, 0);
        leftHand.transform.localRotation = Quaternion.Euler(0, 0, 0);
        rightHand.transform.localPosition = new Vector3(0, 0, 0);
        rightHand.transform.localRotation = Quaternion.Euler(0, 0, 180);

        wristUI._handGhostLeft = leftHand.GetComponent<HandGhost>();
        wristUI._handGhostRight = rightHand.GetComponent<HandGhost>();
        wristUI._anchor = anchor.transform;
        wristUI.isLeftHand = true;

        wristUI.ovrCameraRigRef = FindObjectOfType<OVRCameraRigRef>();

        GameObjectUtility.SetParentAndAlign(newObj, menuCommand.context as GameObject);
        Undo.RegisterCreatedObjectUndo(newObj, "WristUI");
        Selection.activeObject = newObj;
    }

    private void OnValidate()
    {
        if (EditorApplication.isPlaying)
        {
            return;
        }
        EditorApplication.delayCall += _OnValidate;
    }

    private void _OnValidate()
    {
        // �ʒu�����p��ghostHand�̍��E�؂�ւ�
        EditorApplication.delayCall -= _OnValidate;
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

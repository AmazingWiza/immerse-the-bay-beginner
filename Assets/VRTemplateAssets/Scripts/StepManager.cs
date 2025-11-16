using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace Unity.VRTemplate
{
    /// <summary>
    /// Controls the steps in the in coaching card.
    /// </summary>
    public class StepManager : MonoBehaviour
    {
        [Serializable]
        class Step
        {
            [SerializeField]
            public GameObject stepObject;

            [SerializeField]
            public string buttonText;
        }

        [SerializeField]
        public TextMeshProUGUI m_StepButtonTextField;

        [SerializeField]
        List<Step> m_StepList = new List<Step>();

        int m_CurrentStepIndex = 0;

        [ContextMenu("Next Step")]
        public void Next()
        {
            if (m_StepList == null || m_StepList.Count == 0)
                return;

            if (m_CurrentStepIndex < 0 || m_CurrentStepIndex >= m_StepList.Count)
                m_CurrentStepIndex = 0;

            var current = m_StepList[m_CurrentStepIndex];
            if (current != null && current.stepObject != null)
                current.stepObject.SetActive(false);

            m_CurrentStepIndex = (m_CurrentStepIndex + 1) % m_StepList.Count;

            var next = m_StepList[m_CurrentStepIndex];
            if (next != null && next.stepObject != null)
                next.stepObject.SetActive(true);

            if (m_StepButtonTextField != null)
                m_StepButtonTextField.text = m_StepList[m_CurrentStepIndex].buttonText;

#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            EditorSceneManager.MarkSceneDirty(gameObject.scene);
#endif
        }

        [ContextMenu("Previous Step")]
        public void Previous()
        {
            if (m_StepList == null || m_StepList.Count == 0)
                return;

            if (m_CurrentStepIndex < 0 || m_CurrentStepIndex >= m_StepList.Count)
                m_CurrentStepIndex = 0;

            var current = m_StepList[m_CurrentStepIndex];
            if (current != null && current.stepObject != null)
                current.stepObject.SetActive(false);

            m_CurrentStepIndex = (m_CurrentStepIndex - 1 + m_StepList.Count) % m_StepList.Count;

            var prev = m_StepList[m_CurrentStepIndex];
            if (prev != null && prev.stepObject != null)
                prev.stepObject.SetActive(true);

            if (m_StepButtonTextField != null)
                m_StepButtonTextField.text = m_StepList[m_CurrentStepIndex].buttonText;

#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            EditorSceneManager.MarkSceneDirty(gameObject.scene);
#endif
        }
    }
}

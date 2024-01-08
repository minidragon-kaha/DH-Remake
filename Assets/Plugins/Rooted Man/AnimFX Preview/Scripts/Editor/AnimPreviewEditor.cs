// 10:11 AMFeast

using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using ReorderableList = CustomList.ReorderableList;

namespace AnimPreview
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(AnimPreview))]
    public partial class AnimPreviewEditor : Editor
    {
        private Color GreenColor = new Color(0f, 1f, 0 ) ;
        private float SpaceSection = 5f;
        private AnimPreview Target;

        private SerializedProperty PSPreviewField;
        private SerializedProperty AnimationDelayField;
        private SerializedProperty PlaybackSpeed;
        private SerializedProperty AnimationSpeed;

        private ReorderableList List;

        protected void OnEnable()
        {
            Target = (AnimPreview) target;

            PSPreviewField = serializedObject.FindProperty("PsPreview");
            List = new ReorderableList(PSPreviewField);
            List.onAddCallback += OnAddPSPreview;
            
            AnimationDelayField = serializedObject.FindProperty("AnimationDelay");
            PlaybackSpeed = serializedObject.FindProperty("PlaybackSpeed");
            AnimationSpeed = serializedObject.FindProperty("AnimationSpeed");
        }

        private void OnAddPSPreview(ReorderableList list)
        {
            InitializeItem(list.AddItem());
        }
        
        private void InitializeItem(SerializedProperty particlePreview) 
        {
            particlePreview.FindPropertyRelative("Ps").objectReferenceValue = null;
            particlePreview.FindPropertyRelative("Delay").floatValue = 0f;
            particlePreview.FindPropertyRelative("SimulationSpeed").floatValue = 1;
            particlePreview.FindPropertyRelative("PreviewChildren").boolValue = true;
        }

        private void OnDisable()
        {
            List.onAddCallback -= OnAddPSPreview;
            if (AnimPreview.AutoStopOnDisable)
                Target.StopButton();
        }
        
        public static void UpdatePrefab()
        {
            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage != null)
            {
                EditorSceneManager.MarkSceneDirty(prefabStage.scene);
            }
        }

        void DoBaseColor()
        {
            var simuColor = Target.IsSimulating ? GreenColor : Color.white;
            GUI.backgroundColor = simuColor;
        }

        public static bool IsPrefabInProjectView(GameObject gObject) {
            return gObject.scene.IsValid() == false;
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            GUI.color = Color.white;

            DoBaseColor();

            GeneralInspector();

            AnimatedInspector();

            ParticleSystemInspector();

            if (!IsPrefabInProjectView(Target.gameObject))
            {
                GUILayout.Space(SpaceSection);
                TimelineInspector();
            }
            else
            {
                EditorGUILayout.HelpBox(Style.ProjectViewMsg, MessageType.Info);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void ParticleSystemInspector()
        {
            List.DoLayoutList();
            
            // EditorGUILayout.PropertyField(PSPreviewField, Style.ParticleSystems);

            // foreach (var p in Target.PsPreview)
            // {
            //     // If it's not a scene gameobject, we do not allow it.
            //     if (p.Ps != null && p.Ps.gameObject.scene.IsValid() == false)
            //     {
            //         // TODO: We need to make sure that we're not saving a prefab. For now we remove this.
            //         Debug.LogWarning($"{p.Ps.name}" + Style.WARNING_PS_IS_NOT_SCENE_OBJECT);
            //         p.Ps = null;
            //     }
            // }
        }

        private void GeneralInspector()
        {
            var height = EditorGUIUtility.singleLineHeight;
            Rect headerRect = EditorGUI.IndentedRect(EditorGUILayout.GetControlRect(false, height));
            headerRect.height = height;

            DrawHeader(headerRect, new GUIContent(""));

            EditorGUI.indentLevel = 1;

            Target.GeneralDropdown = EditorGUI.Foldout(headerRect, Target.GeneralDropdown, "General", true);
            if (Target.GeneralDropdown)
            {
                GUILayout.BeginHorizontal();

                EditorGUI.BeginChangeCheck();

                var theHeight = 2f;
                Rect buttonRect = EditorGUI.IndentedRect(EditorGUILayout.GetControlRect(false, 38, Style.LockButtonOptions));
                buttonRect.position += Vector2.up * theHeight;
                buttonRect.size = new Vector2(38, 38);
                var helpBoxRect = EditorGUI.IndentedRect(EditorGUILayout.GetControlRect(false, 38));
                helpBoxRect.position += Vector2.up * theHeight;
                var move = 10f;
                // helpBoxRect.width += buttonRect.width + move;
                helpBoxRect.position += new Vector2(- move, 0f);;


                // style.overflow = new RectOffset(-3,-3,-3,-3);
                // style.margin = new RectOffset(5,5,5,5);
                
                if (AnimPreview.AutoStopOnDisable)
                {
                    if (GUI.Button(buttonRect, Style.LockButtonContent, Style.LockButtonStyle))
                    {
                        AnimPreview.AutoStopOnDisable = false;
                    }

                    // GUILayout.Label(Style.LockMsg);
                    
                    EditorGUI.HelpBox(helpBoxRect, Style.LockMsg, MessageType.Info);
                }
                else
                {
                    if (GUI.Button(buttonRect, Style.UnlockButtonContent, Style.LockButtonStyle))
                    {
                        AnimPreview.AutoStopOnDisable = true;
                    }
                    
                    EditorGUI.HelpBox(helpBoxRect, Style.UnlockMsg, MessageType.Info);

                    // GUILayout.Label(Style.UnlockMsg);
                }

                GUILayout.EndHorizontal();
           
                // var speed = EditorGUILayout.FloatField(Style.PlaybackSpeedContent, Target.PlaybackSpeed);

                EditorGUILayout.PropertyField(PlaybackSpeed, Style.PlaybackSpeedContent);  
                Target.PlaybackSpeed = Mathf.Clamp(Target.PlaybackSpeed, 0f, float.MaxValue);

                if (EditorGUI.EndChangeCheck())
                {
                    UpdatePrefab();
                }
            }

            EditorGUI.indentLevel = 0;
        }


        private void DrawHeader(Rect rect, GUIContent label)
        {
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            if (Event.current.type == EventType.Repaint)
            {
                Style.HeaderBackground.Draw(rect, false, false, false, false);
            }

            Rect titleRect = rect;
            titleRect.xMin += 6f;
            titleRect.xMax -= 95f;

            GUI.Label(titleRect, label, EditorStyles.label);
            GUILayout.Space(3f);
        }

        private void AnimatedInspector()
        {
            var height = EditorGUIUtility.singleLineHeight;
            Rect headerRect = EditorGUI.IndentedRect(EditorGUILayout.GetControlRect(false, height));
            headerRect.height = height;

            DrawHeader(headerRect, new GUIContent(""));

            EditorGUI.indentLevel = 1;
            
            EditorGUI.BeginChangeCheck();

            Target.AnimatedDropdown = EditorGUI.Foldout(headerRect, Target.AnimatedDropdown, "Animation", true);
            if (Target.AnimatedDropdown)
            {
                var draggedAnimatedObject = EditorGUILayout.ObjectField(
                    Style.AnimatedContent,
                    Target.Animated,
                    typeof(GameObject),
                    true) as GameObject;
                
                // on drag a new object.
                if (draggedAnimatedObject != Target.Animated)
                {
                    // If it's not a scene gameobject, we do not allow it.
                    if (draggedAnimatedObject == null || draggedAnimatedObject.scene.IsValid())
                    {
                        // make sure the other gameObject is reset properly.
                        if (Target.Animated != null)
                        {
                            // Debug.Log("reset obj");
                            Target.ResetAnimation(Target.Animated);
                        }
                        
                        Target.Animated = draggedAnimatedObject;
                        Target.GetMainPreviewClip();
                        Target.SelectedAnimationClipIndex = 1;
                    }
                    else
                    {
                        Debug.LogWarning(draggedAnimatedObject.name + Style.WARNING_ANIMATED_IS_NOT_SCENE_OBJECT);
                    }
                }

                Target.GetMainPreviewClip();

                if (Target.Clips == null || Target.Clips.Length == 0)
                {
                    var names = new string[1];
                    if (Target.Animated != null)
                    {
                        names[0] = $"NO ANIM FOUND: {Target.Animated.name}";
                    }
                    else
                    {
                        names[0] = $"NONE";
                    }
                    EditorGUILayout.Popup(
                        Style.AnimationContent,
                        0, names);
                }
                else
                {
                    var offset = AnimPreview.Offset;
                    var max = Target.Clips.Length + offset;
                    Target.SelectedAnimationClipIndex = Mathf.Clamp(Target.SelectedAnimationClipIndex, 0, max);
                    var names = new string[max];
                    names[0] = "STOP";
                    for (int i = offset; i < (Target.Clips.Length + offset); i++)
                    {
                        names[i] = Target.Clips[i - offset].name;
                    }

                    EditorGUILayout.BeginHorizontal();
                    Target.SelectedAnimationClipIndex = EditorGUILayout.Popup(
                        Style.AnimationContent,
                        Target.SelectedAnimationClipIndex, names);

                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.PropertyField(AnimationDelayField, Style.DelayContent);  

                // Target.AnimationDelay = EditorGUILayout.FloatField(
                //     Style.DelayContent,
                //     Target.AnimationDelay);

                EditorGUILayout.PropertyField(AnimationSpeed, Style.AnimationSpeedContent);  
                Target.AnimationSpeed = Mathf.Abs(Target.AnimationSpeed);
            }

            EditorGUI.indentLevel = 0;
            
            if (EditorGUI.EndChangeCheck())
            {
                UpdatePrefab();
            }
        }


        private void TimelineInspector()
        {
            GUILayout.BeginHorizontal();

            if (Target.IsSimulating)
            {
                var second = Mathf.FloorToInt(Target.PlaybackValue);
                var frame = ((Target.PlaybackValue % 1) * 60);
                string timeString;
                if (Target.CurrentClip != null)
                    timeString= ($"{second:f0}:{frame:00}ms (Animation {(Target.AnimationPlaybackPercent * 100f):f0} %)");
                else 
                    timeString= ($"{second:f0}:{frame:00}ms");
                GUILayout.Label($"Playback {timeString}", Style.TitleStyle);
            }
            else
            {
                GUILayout.Label($"", Style.TitleStyle);
            }

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

            var simuColor = Target.IsSimulating ? Color.red : Color.white;
            GUI.backgroundColor = simuColor;

            if (GUILayout.Button(Style.StopButtonContent, Style.TimelineButtonStyle, Style.ButtonOptions))
            {
                Target.StopButton();
            }

            DoBaseColor();

            if (GUILayout.Button(Target.IsPaused ? Style.PlayButtonContent : Style.PauseButtonContent,
                Style.ButtonOptions))
            {
                if (Target.IsPaused)
                {
                    Target.PlayButton();
                }
                else
                {
                    Target.PauseButton();
                }
            }

            var rectHeight = 20f;
            var floatFieldWidth = 30f;
            var spacing = 2f;
            var heightOffset = 2f;

            Rect rect = EditorGUI.IndentedRect(EditorGUILayout.GetControlRect(false, rectHeight));
            var baseWidth = rect.width;
            rect.width = floatFieldWidth;
            rect.position += Vector2.up * heightOffset + Vector2.right * spacing;
            
            EditorGUI.BeginChangeCheck();
            var start = EditorGUI.FloatField(rect, Target.PlaybackStart);
            Target.PlaybackStart = Mathf.Clamp(start, 0f, Target.PlaybackEnd);
            if (EditorGUI.EndChangeCheck())
            {
                UpdatePrefab();
            }
            
            rect.width = baseWidth - ((floatFieldWidth + spacing) * 2f);
            rect.position = rect.position + Vector2.right * (floatFieldWidth + spacing * 2f);

            Target.PlaybackValue = Mathf.Clamp(Target.PlaybackValue, Target.PlaybackStart, Target.PlaybackEnd);
            var val = EditorGUI.Slider(rect, Target.PlaybackValue, Target.PlaybackStart, Target.PlaybackEnd);
            if (val != Target.PlaybackValue)
            {
                Target.PlaybackValue = val;
                if (!Target.IsSimulating)
                {
                    Target.PlayButton();
                }
                
                // Target.PauseButton(); // If you want to pause when you scrub the playback instead of letting it play, uncomment this line.
                Target.IsScrubbing = true;
            }
            else
            {
                if (Target.IsScrubbing && !Target.IsPaused)
                {
                    Target.PlayButton();
                    Target.IsScrubbing = false;
                }
            }

            // rect = EditorGUILayout.GetControlRect(false, rectHeight);

            rect.position = new Vector2(rect.max.x + spacing, rect.position.y);
            rect.width = floatFieldWidth - spacing;
            
            EditorGUI.BeginChangeCheck();
            var end = EditorGUI.FloatField(rect, Target.PlaybackEnd);
            Target.PlaybackEnd = Mathf.Clamp(end, 0.01f, float.MaxValue);
            if (EditorGUI.EndChangeCheck())
            {
                UpdatePrefab();
            }

            GUILayout.EndHorizontal();
        }
    }
}
// 1:50 PMFeast

using UnityEditor;
using UnityEngine;

namespace AnimPreview
{
    public partial class AnimPreviewEditor
    {
        public static class Style
        {
            public static readonly GUIStyle HeaderBackground;

            public static readonly GUIContent PlayButtonContent;
            public static readonly GUIContent PauseButtonContent;
            public static readonly GUIContent StopButtonContent;
            public static readonly GUIContent LockButtonContent;
            public static readonly GUIContent UnlockButtonContent;

            public static readonly GUIStyle TimelineButtonStyle;
            public static readonly GUIStyle LockButtonStyle;

            public static readonly GUILayoutOption[] ButtonOptions;
            public static readonly GUILayoutOption[] LockButtonOptions;
            public static readonly GUILayoutOption PlaybackField;
            // public static readonly GUIContent TimelineStart;
            // public static readonly GUIContent TimelineEnd;

            public static readonly GUIStyle TitleStyle;

            public static readonly GUIContent AnimationSpeedContent;
            public static readonly GUIContent DelayContent;
            public static readonly GUIContent AnimationContent;
            public static readonly GUIContent AnimatedContent;

            public static readonly GUIContent PlaybackSpeedContent;
            public static readonly GUIContent ParticleSystems;

            public static readonly string WARNING_ANIMATED_IS_NOT_SCENE_OBJECT = $" is not a SCENE gameObject. Add to {nameof(AnimPreview)} a gameObject from a SCENE with an Animator/Animation component (and not the project window).";
            public static readonly string WARNING_PS_IS_NOT_SCENE_OBJECT = $" is not a SCENE ParticleSystem. Add to {nameof(AnimPreview)} a ParticleSystem from a SCENE (not the project window).";
            public static readonly string LockMsg = "Stops previewing on previewer deselected";
            public static readonly string UnlockMsg = "Keeps previewing on previewer deselected";
            public static readonly string ProjectViewMsg = "Playback not available from the Project view.";
               
            static Style()
            {
                ParticleSystems = new GUIContent("Particle Systems");
                PlaybackSpeedContent = new GUIContent("Playback Speed", "Adjust the speed of the playback.\nThis means the Particles and the Animation simulates at a new speed based on this value.\nDefault = 1.");
                
                // TimelineStart = new GUIContent("", "The start time of the timeline");
                // TimelineEnd = new GUIContent("", "The end time of the timeline");
                
                AnimatedContent = new GUIContent("Animated", "A SCENE gameObject.\nIt must have an Animator/Animation component.");
                AnimationContent = new GUIContent("Animation", "What animation should we preview?\nThe animations are read from an Animation/Animator component.");
                DelayContent = new GUIContent("Delay", "Delays the animation of the Animated object.\nDefault = 0");
                AnimationSpeedContent = new GUIContent("Speed", "The Animated object's animation speed.\nDefault = 1");

                TitleStyle = new GUIStyle("BoldLabel") {alignment = TextAnchor.MiddleCenter};
                
                ButtonOptions = new GUILayoutOption[] {GUILayout.MinWidth(25), GUILayout.MaxWidth(40), GUILayout.Height(25)};
                PlaybackField = GUILayout.Width(30);

                PlayButtonContent = EditorGUIUtility.IconContent("PlayButton", "Play");
                PlayButtonContent.tooltip = "PLAY";
                PauseButtonContent = EditorGUIUtility.IconContent("PauseButton", "Pause");
                PauseButtonContent.tooltip = "PAUSE";
                
                TimelineButtonStyle = new GUIStyle("Button");
                
#if UNITY_2019
                StopButtonContent = EditorGUIUtility.IconContent("PopupWindowOff", "Preview");
                TimelineButtonStyle.contentOffset = Vector2.up * 1.5f;
#else
                StopButtonContent = EditorGUIUtility.IconContent("ol_minus", "Preview");

#endif 
                StopButtonContent.tooltip = "STOP";

                
                LockButtonOptions = new GUILayoutOption[] {GUILayout.MinWidth(40), GUILayout.MaxWidth(60)};

                LockButtonStyle = new GUIStyle("Button");
                
#if UNITY_2019  
                LockButtonContent = EditorGUIUtility.IconContent("IN LockButton@2x", "Lock");
                LockButtonStyle.contentOffset = Vector2.left * 3f;
#else
                LockButtonContent = EditorGUIUtility.IconContent("IN LockButton@2x", "Lock");
                // LockButtonContent = EditorGUIUtility.IconContent("LockIcon-On", "Lock");
#endif
                LockButtonContent.tooltip =
                    $"The animation will play even when this gameObject is deselected\n\nTIP: This is useful if you want to edit objects while previewing your animations.";

                UnlockButtonContent = EditorGUIUtility.IconContent("IN LockButton on@2x", "Unlock");
                UnlockButtonContent.tooltip = $"When you deselect this gameObject, STOP is called automatically and the preview stops";

                HeaderBackground = new GUIStyle("RL Header");

                HeaderBackground = new GUIStyle("RL Header");
            }
        }
    }
}
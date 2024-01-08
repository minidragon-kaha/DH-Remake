
using System.Collections.Generic;

namespace AnimPreview
{
#if UNITY_EDITOR
    using UnityEditor;
    
#endif
    using System;
    using CustomList;
    using UnityEngine;

    [AddComponentMenu("Preview/Anim | FX Preview")]
    [ExecuteAlways]
    public class AnimPreview : MonoBehaviour
    {
#if UNITY_EDITOR
        // General
        public bool GeneralDropdown = false;
        public static bool AutoStopOnDisable = false;

        // Animated
        public bool AnimatedDropdown = true;
        public GameObject Animated;
        public float AnimationDelay;
        public Animator Animator;
        public Animation Animation;
        public int SelectedAnimationClipIndex = 0;
        public AnimationClip[] Clips;
        public const int Offset = 1;
        public AnimationClip CurrentClip;

        // Particles
        public FxList PsPreview;

        // Playback
        public bool IsSimulating { get; private set; } = false;
        public float PlaybackStart = 0f;
        public float PlaybackEnd = 3f;
        public float PlaybackValue;
        public bool IsPaused = true;
        private float TotalAnimationLength;
        private double StartTime;
        public float AnimationPlaybackPercent { get; private set; }
        public float TotalPlaybackPercent { get; private set; }
        public bool IsScrubbing;

        // Playback speed
        public float ParticlesSpeed = 1f;
        public float PlaybackSpeed = 1f;
        public float AnimationSpeed = 1f;

        // Save autoRandomSeed
        public static bool AUTO_SAVE_SEED_ON_SIMULATE = true;
        
        void OnEnable()
        {
            EditorApplication.update += SimulateUpdate;
            UnityEditor.SceneManagement.PrefabStage.prefabStageOpened += PrefabStage;
            UnityEditor.SceneManagement.PrefabStage.prefabStageClosing += PrefabStage;
        }

        void OnDestroy()
        {
            EditorApplication.update -= SimulateUpdate;
            UnityEditor.SceneManagement.PrefabStage.prefabStageOpened -= PrefabStage;
            UnityEditor.SceneManagement.PrefabStage.prefabStageClosing -= PrefabStage;
        }
        
        private void PrefabStage(UnityEditor.SceneManagement.PrefabStage obj)
        {
            StopButton();
        }

        public void Reset()
        {
            IsSimulating = false;
            IsPaused = true;
        }

        public void OpenAnimationWindow()
        {
            EditorApplication.ExecuteMenuItem("Window/Animation/Animation");
        }

        public void RestartSimulation()
        {
            StartSimulate(true, true);
        }

        public void PlayButton()
        {
            IsPaused = false;
            StartSimulate(true, false);
        }

        public void PauseButton()
        {
            IsPaused = true;
            StartSimulate(true, false);
        }


        public void StopButton()
        {
            IsPaused = true;
            StartSimulate(false, true);
        }

        private void Save()
        {
            if (AUTO_SAVE_SEED_ON_SIMULATE)
            {
                foreach (var p in PsPreview)
                {
                    p.SaveSeed();
                }
            }
        }

        private void Load()
        {
            if (AUTO_SAVE_SEED_ON_SIMULATE)
            {
                foreach (var p in PsPreview)
                {
                    p.LoadSeed();
                }
            }
        }

        public void StartSimulate(bool _simulate, bool _resetToBeginning)
        {
            //Application.IsPlaying(gameObject) == false && 

            bool isChanged = IsSimulating != _simulate;

            if (_resetToBeginning)
            {
                PlaybackValue = PlaybackStart;

                AnimationPlaybackPercent = PlaybackValue / TotalAnimationLength;
                TotalPlaybackPercent = PlaybackValue / PlaybackEnd;
            }
            else
            {
                PlaybackValue = (PlaybackValue) % (PlaybackEnd);
            }

            StartTime = EditorApplication.timeSinceStartup - (PlaybackValue / PlaybackSpeed);

            IsSimulating = _simulate;

            if (PsPreview != null)
            {
                foreach (var psp in PsPreview)
                {
                    if (psp.Ps == null) continue;
                    psp.Ps.Stop(psp.PreviewChildren, ParticleSystemStopBehavior.StopEmittingAndClear);
                    psp.Ps.time = 0f;
                }
            }

            if (Animated != null)
            {
                Animator = Animated.GetComponent<Animator>();
                Animation = Animated.GetComponent<Animation>();

                var clip = GetMainPreviewClip();

                if (clip != null)
                {
                    if (Animation != null)
                    {
                        clip.SampleAnimation(this.gameObject, 0f);
                        Animation.Rewind();
                    }

                    if (Animator != null)
                    {
                        clip.SampleAnimation(Animator.gameObject, 0f);
                    }
                }
            }

            if (isChanged)
            {
                if (_simulate) Save();
            }

            if (isChanged)
            {
                if (!_simulate) Load();
            }
        }

        public AnimationClip GetMainPreviewClip()
        {
            if (Animated == null)
            {
                Clips = new AnimationClip[0];
                return null;
            }

            Clips = AnimationUtility.GetAnimationClips(Animated);
            if (SelectedAnimationClipIndex < Offset) return null;
            var result = SelectedAnimationClipIndex - Offset;
            if (Clips.Length < SelectedAnimationClipIndex)
            {
                SelectedAnimationClipIndex = 0;
                return null;
            }

            return Clips[result];
        }

        public void ResetAnimation(GameObject _object)
        {
            var animClip = new AnimationClip();
            animClip.SampleAnimation(_object, 0f);
            var animation = _object.GetComponent<Animation>();
            if(animation != null)
                animation.Rewind();
        }

        
        void SimulateUpdate()
        {
            if (IsSimulating)
            {
                Save();

                var clip = GetMainPreviewClip();
                CurrentClip = clip;
                TotalAnimationLength = clip == null ? 1f : clip.length;

                if (IsPaused || IsScrubbing)
                {
                    AnimationPlaybackPercent = PlaybackValue / TotalAnimationLength;
                    TotalPlaybackPercent = PlaybackValue / PlaybackEnd;
                }
                else
                {
                    double dt = (EditorApplication.timeSinceStartup - StartTime) * PlaybackSpeed;
                    PlaybackValue = (float) (dt) % PlaybackEnd;

                    AnimationPlaybackPercent = PlaybackValue / TotalAnimationLength;
                    TotalPlaybackPercent = PlaybackValue / PlaybackEnd;

                    if (dt >= PlaybackEnd)
                    {
                        RestartSimulation();
                        return;
                    }
                }

                if (clip != null)
                {
                    clip.SampleAnimation(Animated, (PlaybackValue - AnimationDelay) * AnimationSpeed);
                }

                for (var i = 0; i < PsPreview.Count; i++)
                {
                    var preview = PsPreview[i];
                    if (preview == null) continue;
                    if (preview.Ps == null) continue;

                    var playtime = (PlaybackValue - preview.Delay) * preview.SimulationSpeed;
                    preview.Ps.Simulate(playtime, preview.PreviewChildren, true);
                    preview.Ps.time = playtime;
                }
            }
        }
#endif
    }

    
#if UNITY_EDITOR

    [Serializable]
    public class ParticlePreview
    {
        public static string ERROR_CANT_LOAD = $"Can't load particles's autoRandomSeed! The particles you were previewing might have their autoRandomSeed set as false. This might not be what you want.";
        
        [Tooltip("The particle system to preview. It should be a SCENE object.")]
        public ParticleSystem Ps;

        [Tooltip("Delay the particle system's activation in the preview.\nDefault = 0.\n\nTIP: The ParticleSystem component has a Delay value you can modify so the preview and playmode can look the same.")]
        public float Delay;

        [Tooltip("How fast the particle system simulates.\nDefault = 1.\n\nTIP: The ParticleSystem component has a Simulation Speed value you can modify so the preview and playmode can look the same.")]
        public float SimulationSpeed = 1f;

        [Tooltip("Should the children particle systems also preview?")]
        public bool PreviewChildren = true;

        [HideInInspector, SerializeField] 
        public List<SavedSeed> SavedSeeds;

        public void SaveSeed()
        {
            if (Ps == null) return;

            var childrenPs = Ps.GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < childrenPs.Length; i++)
            {
                bool found = false;
                foreach (var s in SavedSeeds) // check no duplicate
                {
                    if (s.Ps == childrenPs[i])
                    {
                        found = true;
                        if (!s.IsSaved)
                        {
                            s.Save();
                            // Debug.Log("saved");
                        }
                    }
                }

                if (!found)
                {
                    // Debug.Log("saved");
                    SavedSeeds.Add(new SavedSeed(childrenPs[i]));
                }
            }
        }

        public void LoadSeed()
        {
            foreach (var s in SavedSeeds)
            {
                if (s.Ps == null)
                {
                    Debug.LogError(ERROR_CANT_LOAD);
                    continue;
                }
                else
                {
                    if (s.IsSaved)
                    {
                        if (s.Ps.useAutoRandomSeed != s.SavedUseRandomSeed)
                            s.Ps.useAutoRandomSeed = s.SavedUseRandomSeed;
                        
                        // Debug.Log("loaded");
                        s.IsSaved = false;
                    }
                }
            }

            SavedSeeds.Clear();
        }
    }

    [Serializable]
    public class SavedSeed
    {
        public SavedSeed(ParticleSystem _ps)
        {
            IsSaved = true;
            Ps = _ps;
            Save();
        }

        public void Save()
        {
            SavedUseRandomSeed = Ps.useAutoRandomSeed;
            if (Ps.useAutoRandomSeed)
            {
                // we have to stop the particle to make sure it's properly saved.
                Ps.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
                Ps.useAutoRandomSeed = false;
            }
        }

        public ParticleSystem Ps;
        public bool IsSaved;
        [HideInInspector] public bool SavedUseRandomSeed;
    }

    [Serializable]
    public class FxList : ReorderableArray<ParticlePreview>
    {
        
    }
#endif
}
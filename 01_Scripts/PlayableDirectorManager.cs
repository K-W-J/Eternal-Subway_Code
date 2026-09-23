using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace _01_Scripts
{
    public class PlayableDirectorManager : MonoSingleton<PlayableDirectorManager>
    {
        [SerializeField] private PlayableDirector _playableDirector;
        [SerializeField] private TimelineAsset[] _timelineAssets;

        private Dictionary<string, TimelineAsset> _timelines = new Dictionary<string, TimelineAsset>();
        
        private void Awake()
        {
            if (_timelineAssets.Length > 0)
            {
                foreach (var timelineClip in _timelineAssets)
                {
                    _timelines[timelineClip.name] = timelineClip;
                }
            }
        }

        public void PlayTimeline(string timelineName)
        {
            _playableDirector.playableAsset = _timelines[timelineName];
            _playableDirector.Play();
        }
    }
}
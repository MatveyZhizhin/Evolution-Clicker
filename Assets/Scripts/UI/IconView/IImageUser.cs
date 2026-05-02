using System;
using UnityEngine;

namespace UI.IconView
{
    public interface IImageUser
    {
        public event Action<Sprite> SpriteChanged;
    }
}


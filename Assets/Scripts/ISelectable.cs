using UnityEngine;

namespace Assets.Scripts
{
    public interface ISelectable
    {
        void Select();
        void Deselect();
        GameObject GameObject { get; }
    }
}

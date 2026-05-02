using System;

namespace UI.ProgressBar
{
    public interface IProgressBarUser
    {
        public event Action<int, int> OnValueChanged;
    }
}

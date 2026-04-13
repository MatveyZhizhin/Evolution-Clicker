using System;

namespace UI
{
    public interface ITextUser
    {
        public event Action<string, string> OnDataChanged;

        public void InitializeTextView(TextView view);
    }
}


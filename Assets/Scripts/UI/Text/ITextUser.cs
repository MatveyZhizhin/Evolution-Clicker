using System;

namespace UI.Text
{
    public interface ITextUser
    {
        public event Action<string, string> OnDataChanged;
    }
}


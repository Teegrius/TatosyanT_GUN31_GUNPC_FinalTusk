using System;

namespace FinalTask.Casino
{
    public abstract class CasinoGameBase
    {
        public event Action? OnWin;
        public event Action? OnLoose;
        public event Action? OnDraw;

        public abstract void PlayGame();

        protected void OnWinInvoke() => OnWin?.Invoke();
        protected void OnLooseInvoke() => OnLoose?.Invoke();
        protected void OnDrawInvoke() => OnDraw?.Invoke();

        protected abstract void FactoryMethod();
    }
}

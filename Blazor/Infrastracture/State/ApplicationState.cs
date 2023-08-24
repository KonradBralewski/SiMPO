namespace Blazor.Infrastracture.State
{
    /// <summary>
    /// Singleton injected service to pass shared and relevent informations between components.
    /// </summary>
    public class ApplicationState
    {
        private bool _isWaitingForResponse;
        public bool IsWaitingForResponse 
        {
            get => _isWaitingForResponse;
            set
            {
                if(value != _isWaitingForResponse)
                {
                    _isWaitingForResponse = value;
                    NotifyStateChanged();
                }
            }
        }

        public Action? StateChanged;

        private void NotifyStateChanged()
            => this.StateChanged?.Invoke();

        /// <summary>
        /// Method that will be used to determine if application is waiting for something.
        /// </summary>
        /// <returns>Returns true if application waits for something or false if not.</returns>
        public bool IsApplicationWaiting()
        {
            if(_isWaitingForResponse)
            {
                return true;
            }

            return false;
        }
    }
}

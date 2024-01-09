using Humanizer;

namespace AuthenticationDemo.Models
{
    /// <summary>
    /// cung cap kha nang cho viewmodel helper add error message
    /// Message dc dich phia Api
    /// </summary>
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            ErrorMessage = new Dictionary<string, List<string>>();
        }
        public void AddError(string fieldName, string message)
        {
            if (ErrorMessage.ContainsKey(fieldName))
            {
                ErrorMessage[fieldName].Add(message);
            }
            else
            {
                ErrorMessage.Add(fieldName, new List<string>() { message });
            }
        }
        private  Dictionary<string, List<string>> ErrorMessage { get; set; }

        public Dictionary<string, List<string>> Errors { get => ErrorMessage; }
    }
}

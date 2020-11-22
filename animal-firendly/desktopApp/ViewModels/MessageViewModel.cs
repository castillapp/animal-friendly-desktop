using DesktopApp.ConstantsData;
using DesktopApp.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopApp.ViewModels
{
    public class MessageViewModel : ObservableObject
    {
        private string message;
        private TipusMessage tipusMessage;

        public string Message
        {
            get { return message; }
            private set
            {
                message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public TipusMessage TipusMessage
        {
            get => tipusMessage;
            private set
            {
                tipusMessage = value;
                OnPropertyChanged(nameof(TipusMessage));
            }
        }

        public MessageViewModel()
        {
            HideMessage();
        }

        public void HideMessage()
        {
            Message = null;
            TipusMessage = TipusMessage.None;
        }

        public void DisplayMessage(string message, TipusMessage tipusMissatge = TipusMessage.Info)
        {
            Message = message;
            TipusMessage = tipusMissatge;
        }

        public void DisplayErrorMessage(string message, TipusMessage tipusMissatge = TipusMessage.Error)
        {
            Message = message;
            TipusMessage = tipusMissatge;
        }

        public void DisplayErrorMessage(Exception e, TipusMessage tipusMissatge = TipusMessage.Error)
        {
            Message = e.Message;
            TipusMessage = tipusMissatge;
        }
    }
}

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Client
{
    class ViewModel : INotifyPropertyChanged
    {
        private string sendedMessage;
        private string userNamed = Environment.MachineName;
        public ObservableCollection<MessageModel> AllMessages { get; set; }
        private RequestModel request;
        public DateModel dateFilter { get; set; }
        public ViewModel()
        {
            request = new RequestModel("https://localhost:44313/");
            AllMessages = new ObservableCollection<MessageModel>(request.GetRequest());
            dateFilter = new DateModel();
        }
        private RelayCommand sendCommand;
        public RelayCommand SendCommand
        {
            get
            {
                return sendCommand ??
                  (sendCommand = new RelayCommand(obj =>
                  {
                      MessageModel message = new MessageModel
                      {
                          userName = userNamed,
                          messageSendTime = DateTime.Now.ToString(),
                          message = SendedMessage
                      };
                      if (!String.IsNullOrEmpty(message.message))
                      {
                          request.PostRequest(message);
                          AllMessages.Insert(0, message);
                      }
                      sendedMessage = String.Empty;
                      OnPropertyChanged("SendedMessage");

                  }));
            }
        }
        private RelayCommand changeNameCommand;
        public RelayCommand ChangeNameCommand
        {
            get
            {
                return changeNameCommand ??
                    (changeNameCommand = new RelayCommand(obj =>
                     {
                         if (obj.ToString() == "SystemName")
                         {
                             userNamed = Environment.MachineName;
                             OnPropertyChanged("UserName");
                         }
                     }));
            }
        }
        private RelayCommand sortingMessageByDate;
        public RelayCommand SortingMessageByDate
        {
            get
            {
                return sortingMessageByDate ??
                    (sortingMessageByDate = new RelayCommand(obj =>
                     {
                         ObservableCollection<MessageModel> messageInMemory = AllMessages;
                         AllMessages = new ObservableCollection<MessageModel>
                         (AllMessages.Where(message => DateTime.Parse(message.messageSendTime) >= dateFilter.FromDate && DateTime.Parse(message.messageSendTime) <= dateFilter.ToDate));
                         OnPropertyChanged("AllMessages");
                         AllMessages = messageInMemory;
                     }));
            }
        }
        private RelayCommand resetSort;
        public RelayCommand ResetSort
        {
            get
            {
                return resetSort ??
                    (resetSort = new RelayCommand(obj =>
                    {
                        OnPropertyChanged("AllMessages");
                    }));
            }
        }

        public string UserName
        {
            get { return userNamed; }
            set { userNamed = value; OnPropertyChanged("UserName"); }
        }
        public string SendedMessage
        {
            get { return sendedMessage; }
            set { sendedMessage = value; OnPropertyChanged("SendedMessage"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

}

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MobileDev_Projekt.Annotations;

namespace MobileDev_Projekt.Models
{
    class PublishProgramPageModel : INotifyPropertyChanged
    {
        private PublishModel _publishModel;

        public PublishProgramPageModel()
        {
            publishModel = new PublishModel
            {
                Name = "Program Navn"
            };
        }

        public PublishModel publishModel
        {
            get => _publishModel;
            set
            {
                if (Equals(value, _publishModel)) return;
                _publishModel = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class PublishModel : INotifyPropertyChanged
        {
            private string _name;
            private string _reciever;

            public string Name
            {
                get => _name;
                set
                {
                    if (value == _name) return;
                    _name = value;
                    OnPropertyChanged();
                }
            }

            public string reciever
            {
                get => _reciever;
                set
                {
                    if (value == _reciever) return;
                    _reciever = value;
                    OnPropertyChanged();
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

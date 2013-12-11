using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace DRPApp2
{
    public class TwitterSearchResult : INotifyPropertyChanged
    {
        private string _author;
        public string Author { get
        {
            return _author;
        }
            set
            {
                if (_author != value)
                {
                    _author = value;
                    NotifyPropertyChanged("Author");
                }
            }
        }

        private string _tweet;
        public string Tweet
        {
            get
            {
                return _tweet;
            }
            set
            {
                if (_tweet != value)
                {
                    _tweet = value;
                    NotifyPropertyChanged("Tweet");
                }
            }
        }

        private DateTime _publishDate;
        public DateTime PublishDate
        {
            get
            { return _publishDate; }
            set
            {
                if (_publishDate != value)
                {
                    _publishDate = value;
                    NotifyPropertyChanged("PublishDate");
                }
            }
        }

        private string _Id;
        public string ID
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }

        private string _avatarUrl;
        public string AvatarUrl
        {
            get { return _avatarUrl; }
            set
            {
                if (_avatarUrl != value)
                { _avatarUrl = value;
                NotifyPropertyChanged("AvatarUrl");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

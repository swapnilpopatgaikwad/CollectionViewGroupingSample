using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CollectionViewGroupingSample.Model
{
    public class ChildItem : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        private bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                if (isVisible != value)
                {
                    isVisible = value;
                    OnPropertyChanged(nameof(IsVisible));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class GroupModel : ObservableCollection<ChildItem>, INotifyPropertyChanged
    {
        private int groupId;
        public int GroupId
        {
            get { return groupId; }
            set
            {
                if (groupId != value)
                {
                    groupId = value;
                    OnPropertyChanged(nameof(GroupId));
                }
            }
        }

        private string groupName;
        public string GroupName
        {
            get { return groupName; }
            set
            {
                if (groupName != value)
                {
                    groupName = value;
                    OnPropertyChanged(nameof(GroupName));
                }
            }
        }

        public GroupModel(int groupId, string groupName, ObservableCollection<ChildItem> childItems) : base(childItems)
        {
            this.GroupId = groupId;
            this.GroupName = groupName;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

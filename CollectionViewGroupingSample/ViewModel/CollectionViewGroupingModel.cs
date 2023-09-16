using CollectionViewGroupingSample.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace CollectionViewGroupingSample.ViewModel
{
    public class CollectionViewGroupingModel : BaseViewModel
    {

        private ObservableCollection<GroupModel> groups;
        public ObservableCollection<GroupModel> Groups
        {
            get { return groups; }
            set
            {
                if (groups != value)
                {
                    groups = value;
                    OnPropertyChanged(nameof(Groups));
                }
            }
        }

        public List<GroupModel> MainGroups = new List<GroupModel>();

        private int remainingItemsThreshold;
        public int RemainingItemsThreshold
        {
            get { return remainingItemsThreshold; }
            set
            {
                remainingItemsThreshold = value;
                OnPropertyChanged(nameof(RemainingItemsThreshold));
            }
        }

        public ICommand ChangeStateChild { get; set; }
        public ICommand RemainingItemsThresholdReachedCommand { get; set; }

        public int PageNo { get; set; }
        public int RemainingChildItemsThreshold { get; set; } = 25;
        public int ChildPageNo { get; set; }
        public int LastGroupId { get; set; }
        public CollectionViewGroupingModel()
        {
            Groups = new ObservableCollection<GroupModel>();
            RemainingItemsThreshold = 1;
            ChangeStateChild = new Command<ChildItem>(ChangeChildStateMethod);
            RemainingItemsThresholdReachedCommand = new Command<object>(ExecuteThresholdReachedCommand);
            MainGroups = CreateStaticGroups(1, 6000);
            ChildPageNo = 0;
            PageNo = 0;
            LoadInitialItems();
        }

        // Method to load initial items
        public void LoadInitialItems()
        {
            if (MainGroups.Count > 0)
            {
                var _group = MainGroups.FirstOrDefault();
                if (_group != null)
                {
                    var childs = _group.Skip(ChildPageNo * RemainingChildItemsThreshold).Take(RemainingChildItemsThreshold);

                    Groups.Add(new GroupModel(_group.GroupId, _group.GroupName, new ObservableCollection<ChildItem>()));

                    childs.ForEach(child =>
                    {
                        Groups.FirstOrDefault().Add(child);
                    });

                    LastGroupId = _group.GroupId;
                    ChildPageNo++;
                }
            }
        }

        // Method to load more items
        public void LoadMoreItems()
        {

            if (MainGroups.Count > 0 && MainGroups.Count != Groups.Count)
            {
                if (Groups.FirstOrDefault(x => x.GroupId == LastGroupId).Count == MainGroups.FirstOrDefault(x => x.GroupId == LastGroupId).Count)
                {
                    ChildPageNo = 0;
                    PageNo++;
                }

                GroupModel _group = MainGroups.Skip(PageNo * RemainingItemsThreshold).Take(RemainingItemsThreshold).FirstOrDefault();
                if (_group != null)
                {
                    var uiGroup = Groups.Skip(PageNo * RemainingItemsThreshold).Take(RemainingItemsThreshold).FirstOrDefault();
                    if (uiGroup != null && uiGroup.GroupId == _group.GroupId)
                    {
                        var childs = _group.Skip(ChildPageNo * RemainingChildItemsThreshold).Take(RemainingChildItemsThreshold);


                        childs.ForEach(child =>
                        {
                            uiGroup.Add(child);
                        });

                        ChildPageNo++;
                    }
                    else
                    {
                        ChildPageNo = 0;
                        LastGroupId = _group.GroupId;
                        Groups.Add(new GroupModel(_group.GroupId, _group.GroupName, new ObservableCollection<ChildItem>()));

                        uiGroup = Groups.Skip(PageNo * RemainingItemsThreshold).Take(RemainingItemsThreshold).FirstOrDefault();

                        var childs = _group.Skip(ChildPageNo * RemainingChildItemsThreshold).Take(RemainingChildItemsThreshold);

                        childs.ForEach(child =>
                        {
                            uiGroup.Add(child);
                        });

                        ChildPageNo++;
                    }
                }
            }
            else
            {
                GroupModel _group = MainGroups.LastOrDefault();
                var uiGroup = Groups.LastOrDefault();

                if (_group != null && uiGroup != null && _group.Count != uiGroup.Count)
                {
                    var childs = _group.Skip(ChildPageNo * RemainingChildItemsThreshold).Take(RemainingChildItemsThreshold);

                    childs.ForEach(child =>
                    {
                        uiGroup.Add(child);
                    });

                    ChildPageNo++;
                }
            }
        }

        private void ExecuteThresholdReachedCommand(object obj)
        {
            LoadMoreItems();
        }

        private void ChangeChildStateMethod(ChildItem obj)
        {
            if (obj.Id == 3 || obj.Id == 5)
            {
                Groups.ForEach(x =>
                {
                    x.ForEach(y =>
                    {
                        if (values.Contains(y.Id))
                        {
                            if (y.IsVisible)
                            {
                                y.IsVisible = false;
                            }
                            else
                            {
                                y.IsVisible = true;
                            }
                        }
                    });
                });
            }
        }

        public List<GroupModel> CreateStaticGroups(int numberOfGroups, int numberOfChildforEach)
        {
            var groups = new List<GroupModel>();

            ///Build child element 
            var childItems = new ObservableCollection<ChildItem>();
            bool isVisible = false;


            for (int i = 1; i <= numberOfChildforEach; i++)
            {
                isVisible = CheckIsVisible(i);
                var childItem = new ChildItem
                {
                    Id = i,
                    Name = $"Child {i}",
                    Description = $"Description {i}",
                    IsVisible = isVisible
                };

                childItems.Add(childItem);
            }


            for (int i = 1; i <= numberOfGroups; i++)
            {
                var group = new GroupModel(i, $"Static Group {i}", childItems);
                groups.Add(group);
            }

            return groups;
        }

        public List<int> values = new List<int>() { 2, 4, 6 };
        public bool CheckIsVisible(int value)
        {
            if (values.Contains(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;
using MyWmp.Converters;
using MyWmp.Models;

namespace MyWmp.ViewModel
{
    class LibraryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ListCollectionView LibraryMusics { get; private set; }
        public ListCollectionView LibraryVideos { get; private set; }
        public ArrayList LibraryPictures { get; private set; }

        private readonly Library library_;

        public LibraryViewModel()
        {
            library_ = Library.Instance;
            library_.Load();
            LibraryMusics = new ListCollectionView(library_.Sounds.ToArray());
            LibraryVideos = new ListCollectionView(library_.Videos.ToArray());
            LibraryPictures = new ArrayList(library_.Pictures.ToArray());

            this.FilterCommand = new ActionCommand(OnFilter);
            this.GroupCommand = new ActionCommand(OnGroup);
        }

        private void OnFilter(object o)
        {
            var param = o as LibraryConverterParam;
            if (param != null && param.Filter)
                this.LibraryMusics.Filter =
                    a => ((string)a.GetType().GetProperty(param.Sender).GetValue(a, null)).Contains(param.Value);
            else if (param != null && param.Filter == false)
                this.LibraryMusics.Filter = null;
        }

        private void OnGroup(object o)
        {
            var param = o as LibraryConverterParam;
            if (param != null && param.Group && this.LibraryMusics.GroupDescriptions != null)
                this.LibraryMusics.GroupDescriptions.Add(new PropertyGroupDescription(param.Sender));
            else if (param != null && param.Group == false && this.LibraryMusics.GroupDescriptions != null)
                this.LibraryMusics.GroupDescriptions.Remove(
                    this.LibraryMusics.GroupDescriptions.First(a => a.ToString() == (new PropertyGroupDescription(param.Sender)).ToString()));
        }

        public ICommand FilterCommand { get; private set; }
        public ICommand GroupCommand { get; private set; }
    }
}

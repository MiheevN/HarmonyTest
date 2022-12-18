using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfHarmony.Model;

namespace WpfHarmony.ViewModel
{
    public class TestViewModel : INotifyPropertyChanged
    {
        private TestModel Model { get; set; }

        private string findname;
        private string findvalue;

        public event PropertyChangedEventHandler PropertyChanged;

        public string FindName
        {
            get => findname;
            set
            {
                findname = value;
                if (string.IsNullOrEmpty(value) == false)
                {
                    Search();
                }
                Console.WriteLine("Find Name Edits");
                OnPropertyChanged();
            }
        }
        public string FindValue
        {
            get => findvalue;
            set
            {
                findvalue = value;
                Console.WriteLine("Find Value Edits");
                OnPropertyChanged();
            }
        }
        public string EditValue
        {
            get => Model.Value;
            set
            {
                Model.Value = value;
                Console.WriteLine("Edit Value Edits");
                OnPropertyChanged();
            }
        }
        public string EditName
        {
            get => Model.Name;
            set
            {
                Model.Name = value;
                Console.WriteLine("Edit Name Edits");
                OnPropertyChanged();
            }
        }

        public TestViewModel()
        {
            Model = new TestModel();

            UpdateClick = new ButtonCommand(Update);
            AddClick = new ButtonCommand(Add);
            DeleteClick = new ButtonCommand(Delete);
        }

        /// <summary>
        /// Raises OnPropertychangedEvent when property changes
        /// </summary>
        /// <param name="name">String representing the property name</param>
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ICommand UpdateClick { get; set; }
        public ICommand AddClick { get; set; }
        public ICommand DeleteClick { get; set; }
        private async Task Search()
        {
            FindValue = await Model.Search(findname);
        }

        private void Update()
        {
            EditName = FindName;
            EditValue = FindValue;
        }

        private void Add()
        {
            Model.Add();
        }

        private void Delete()
        {
            Model.Delete(FindName);
        }
    }
}

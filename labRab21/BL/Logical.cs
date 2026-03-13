using GalaSoft.MvvmLight.Command;
using labRab21.Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace labRab21.BL
{
    internal class Logical : INotifyPropertyChanged
    {
        public ObservableCollection<Discipline> Disciplines { get; set; } = new ObservableCollection<Discipline>();

        private List<Discipline> _allDisciplines = new List<Discipline>();

        public List<Status> Statuses { get; set; } = new List<Status>()
        {
            new Status(){Name = "Сдано"},
            new Status(){Name = "Не сдано"}
        };

        public Discipline SelectedDiscipline { get; set; }

        private Discipline _newDiscipline;
        public Discipline NewDiscipline
        {
            get { return _newDiscipline; }
            set
            {
                _newDiscipline = value;
                OnPropertyChanged("NewDiscipline");
            }
        }

        public bool IsFilterAll { get; set; } = true;
        public bool IsFilterPassed { get; set; }
        public bool IsFilterFailed { get; set; }

        public RelayCommand AddCommand { get; set; }
        public RelayCommand ChangeStatusCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand FilterCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }

        public Logical()
        {
            _allDisciplines.Add(new Discipline("История", Statuses[1]));
            _allDisciplines.Add(new Discipline("Физ-ра", Statuses[0]));
            _allDisciplines.Add(new Discipline("Физика", Statuses[1]));
            _allDisciplines.Add(new Discipline("Философия", Statuses[1]));

            foreach (var d in _allDisciplines)
            {
                Disciplines.Add(d);
            }

            NewDiscipline = new Discipline() { Status = Statuses[1] };

            AddCommand = new RelayCommand(AddDiscipline);
            ChangeStatusCommand = new RelayCommand(ChangeStatus);
            DeleteCommand = new RelayCommand(Delete);
            FilterCommand = new RelayCommand(ApplyFilter);
            SaveCommand = new RelayCommand(SaveToFile);
        }

        private void AddDiscipline()
        {
            if (string.IsNullOrWhiteSpace(NewDiscipline.Name)) return;

            var d = new Discipline(NewDiscipline.Name, NewDiscipline.Status);
            _allDisciplines.Add(d);
            ApplyFilter();

            NewDiscipline = new Discipline() { Status = Statuses[1] };
        }

        private void ChangeStatus()
        {
            if (SelectedDiscipline != null)
            {
                string targetName = SelectedDiscipline.Name;
                Status targetStatus = SelectedDiscipline.Status.Name == "Сдано" ? Statuses[1] : Statuses[0];

                int index = Disciplines.IndexOf(SelectedDiscipline);
                if (index >= 0)
                {
                    var updatedDiscipline = new Discipline(targetName, targetStatus);
                    Disciplines[index] = updatedDiscipline;

                    int allIndex = _allDisciplines.FindIndex(x => x.Name == targetName);
                    if (allIndex >= 0)
                    {
                        _allDisciplines[allIndex] = updatedDiscipline;
                    }

                    SelectedDiscipline = updatedDiscipline;
                }
            }
        }

        private void Delete()
        {
            if (SelectedDiscipline != null)
            {
                var itemToRemove = _allDisciplines.FirstOrDefault(x => x.Name == SelectedDiscipline.Name);
                if (itemToRemove != null)
                {
                    _allDisciplines.Remove(itemToRemove);
                    ApplyFilter();
                }
            }
        }

        private void ApplyFilter()
        {
            Disciplines.Clear();
            IEnumerable<Discipline> filtered = _allDisciplines;

            if (IsFilterPassed)
                filtered = _allDisciplines.Where(x => x.Status.Name == "Сдано");
            else if (IsFilterFailed)
                filtered = _allDisciplines.Where(x => x.Status.Name == "Не сдано");

            foreach (var item in filtered)
            {
                Disciplines.Add(item);
            }
        }

        private void SaveToFile()
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== Список дисциплин ===");
            foreach (var d in _allDisciplines)
            {
                sb.AppendLine($"{d.Name} — {d.Status.Name}");
            }
            File.WriteAllText("Disciplines.txt", sb.ToString());
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace labRab21.Domain
{
    internal class Institute : INotifyPropertyChanged
    {
        public string Name 
        {
            get { return _name; }
            set
            { 
                _name = value;
                NotifyPropertyChanged();
            }
        } 


        private string _name;



        public event PropertyChangedEventHandler PropertyChanged;


        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

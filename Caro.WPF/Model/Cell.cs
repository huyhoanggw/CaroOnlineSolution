using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caro.WPF.Model
{
    public class Cell : OnPropertyChangedProvider
    {
        private Status _value { get; set; }
        public string Content { get => _content; set { _content = value; OnPropertyChanged(nameof(Content)); } }
        private string _content { get; set; }
        public Status Value
        {
            get => _value;
            set
            {
                if (_value == value) return;
                _value = value;
                OnPropertyChanged(nameof(Value));
                Content = _value == Status.None ? "" : _value == Status.X ? "X" : "O";
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
    public enum Status
    {
        None,
        X,
        O
    }
}

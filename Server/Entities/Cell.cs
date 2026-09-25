using Server.Enum;

namespace Server.Entities
{
    public class Cell
    {
        private Status _value { get; set; }
        public string Content { get => _content; set { _content = value; } }
        private string _content { get; set; }
        public Status Value
        {
            get => _value;
            set
            {
                if (_value == value) return;
                _value = value;
                Content = _value == Status.None ? "" : _value == Status.X ? "X" : "O";
            }
        }

    }
}

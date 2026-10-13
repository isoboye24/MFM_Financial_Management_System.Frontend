using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace MFMFMSF.UI.Features.Dashboard.ViewModels
{
    public class IncomeVM : INotifyPropertyChanged
    {
        private string _name = string.Empty;
        private decimal _amount;
        private double _percentage;
        private Brush _color = Brushes.Gray;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public decimal Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }

        public double Percentage
        {
            get => _percentage;
            set
            {
                _percentage = value;
                OnPropertyChanged();
            }
        }

        public Brush Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
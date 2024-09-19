using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ISW_TP_6.Entities {
    public class StarDrawer : INotifyPropertyChanged {
        private int _rating;
        public int Rating {
            get => _rating;
            set {
                // Notify that the Rating property has changed
                // Notify that all the star sources might have changed
                if (_rating != value) {
                    _rating = value;
                    OnPropertyChanged(nameof(Rating));          
                    OnPropertyChanged(nameof(Star1Source));     
                    OnPropertyChanged(nameof(Star2Source));
                    OnPropertyChanged(nameof(Star3Source));
                    OnPropertyChanged(nameof(Star4Source));
                    OnPropertyChanged(nameof(Star5Source));
                }
            }
        }
        // Alt path: //pack://application:,,,/Resources/Images/filled_star.png :
        public string Star1Source => Rating >= 1 ? "/Resources/Images/FilledStar.png" : "/Resources/Images/EmptyStar.png";
        public string Star2Source => Rating >= 2 ? "/Resources/Images/FilledStar.png" : "/Resources/Images/EmptyStar.png";
        public string Star3Source => Rating >= 3 ? "/Resources/Images/FilledStar.png" : "/Resources/Images/EmptyStar.png";
        public string Star4Source => Rating >= 4 ? "/Resources/Images/FilledStar.png" : "/Resources/Images/EmptyStar.png";
        public string Star5Source => Rating >= 5 ? "/Resources/Images/FilledStar.png" : "/Resources/Images/EmptyStar.png";

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

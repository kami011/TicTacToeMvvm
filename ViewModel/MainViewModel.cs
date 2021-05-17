using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TicTacToe.ViewModel
{
    using BaseClass;
    using System.Windows.Input;

    class MainViewModel : ViewModel
    {
        string _firstname;
        string _secondname;
        string currentPlayer { get; set; }
        string player1;
        string player2;
        string lblContent;
        ObservableCollection<bool> isEnabled;
        ObservableCollection<string> fields;
        ObservableCollection<string> btnbackground;
        int counter;


        public MainViewModel()
        {
            NewGame();
        }

        public string FirstName
        {
            get { return _firstname; }
            set
            {
                _firstname = value;
                onPropertyChange(nameof(FirstName));
            }
        }

        public string SecondName
        {
            get { return _secondname; }
            set
            {
                _secondname = value;
                onPropertyChange(nameof(SecondName));
            }
        }

        public string Player1
        {
            get { return player1; }
            set
            {
                player1 = value;
                onPropertyChange(nameof(Player1));
            }
        }

        public string Player2
        {
            get { return player2; }
            set
            {
                player2 = value;
                onPropertyChange(nameof(Player2));
            }
        }

        public string LblContent
        {
            get { return lblContent; }
            set
            {
                lblContent = value;
                onPropertyChange(nameof(lblContent));
            }
        }

        public ObservableCollection<bool> IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                onPropertyChange(nameof(IsEnabled));
            }
        }

        public ObservableCollection<string> Fields
        {
            get { return fields; }
            set
            {
                fields = value;
                onPropertyChange(nameof(Fields));
            }
        }

        public ObservableCollection<string> BtnBackground
        {
            get { return btnbackground; }
            set
            {
                btnbackground = value;
                onPropertyChange(nameof(BtnBackground));
            }
        }

        private void NewGame() //ustawia wszystko do nowej gry
        {
            IsEnabled = new ObservableCollection<bool> { false, false, false, false, false, false, false, false, false };
            Fields = new ObservableCollection<string> { "", "", "", "", "", "", "", "", "" };
            BtnBackground = new ObservableCollection<string> { "White", "White", "White", "White", "White", "White", "White", "White", "White" };
            counter = 0;
            LblContent = "ROZPOCZNIJ GRĘ";
            FirstName = "";
            SecondName = "";
            Player1 = "";
            Player2 = "";

        }

        private async void checkWin() //sprawdza czy ktoś wygrał
        {

            if (Fields[0].Equals(Fields[8]) && Fields[4].Equals(Fields[8]) && !Fields[0].Equals(""))
            {
                LblContent = "Wygrał gracz " + currentPlayer;
                IsEnabled = new ObservableCollection<bool> { false, false, false, false, false, false, false, false, false };
                await Task.Delay(3000); //zatrzymuje na chwile napis kto wygrał/remis
                
                NewGame();
                return;
            }
            
            if (Fields[2].Equals(Fields[6]) && Fields[4].Equals(Fields[6]) && !Fields[2].Equals(""))
            {
                LblContent = "Wygrał gracz " + currentPlayer;
                IsEnabled = new ObservableCollection<bool> { false, false, false, false, false, false, false, false, false };
                await Task.Delay(3000);

                NewGame();
                return;
            }

            for (int i = 0; i <= 6; i++)
            {
                if ((i == 0 || i == 1 || i == 2) && (Fields[i].Equals(Fields[i + 3]) && Fields[i + 3].Equals(Fields[i + 6]) && !Fields[i].Equals("")))
                {
                    LblContent = "Wygrał gracz " + currentPlayer;
                    IsEnabled = new ObservableCollection<bool> { false, false, false, false, false, false, false, false, false };
                    await Task.Delay(3000);

                    NewGame();
                    System.Threading.Thread.Sleep(3000);
                    return;
                }
                if ((i == 0 || i == 3 || i == 6) && (Fields[i].Equals(Fields[i + 1]) && Fields[i + 1].Equals(Fields[i + 2]) && !Fields[i].Equals("")))
                {
                    LblContent = "Wygrał gracz " + currentPlayer;
                    IsEnabled = new ObservableCollection<bool> { false, false, false, false, false, false, false, false, false };
                    await Task.Delay(3000);

                    NewGame();
                    System.Threading.Thread.Sleep(3000);
                    return;
                }
            }

            if (counter == 8)
            {
                LblContent = "REMIS";
                await Task.Delay(3000);

                NewGame();
                System.Threading.Thread.Sleep(3000);
                return;
            }
        }

        private ICommand setnames;
        public ICommand SetNames //start, przypisuje imiona graczom
        {
            get
            {
                return setnames ?? (setnames = new RelayCommand(passNames, emptyNames));
            }
        }

        private ICommand click;
        public ICommand Click //klik na planszy
        {
            get
            {
                return click ?? (click = new RelayCommand(onClick, tru));
            }
        }


        private void onClick(object param) //co dzieje się przy kliknięciu
        {
            int parameter = (int)param;
            IsEnabled[parameter] = false;
            Fields[parameter] = currentPlayer;
            BtnBackground[parameter] = changeClr();
            checkWin();
            counter++;
            changeCurrent();
        }

        private string changeClr() //zmienia kolor fontu na planszy
        {
            if (currentPlayer == Player1 && Player1 == FirstName) return "PaleVioletRed";
            else return "SkyBlue";
        }

        private void changeCurrent() //zmiana aktualnego gracza
        {
            if (counter % 2 == 0) currentPlayer = Player1;
            else currentPlayer = Player2;
        }

        private bool tru(object param)
        {
            return true;
        }

        private void passNames(object param) //przypisuje imiona graczom i rozpoczyna gre
        {
            player1 = FirstName;
            player2 = SecondName;
            IsEnabled = new ObservableCollection<bool> { true, true, true, true, true, true, true, true, true };
            changeCurrent();
        }

        private bool emptyNames(object param) //sprawdza czy imiona nie są puste lub takie same, nie dopuszcza do rozpoczęcia gry
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(SecondName) || SecondName == FirstName) return false;
            else if (counter > 0) return false;
            else return true;
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Game.Model;
using Game.View;

namespace Game.ViewModel
{
    public class Login : INotifyPropertyChanged
    {
        private string _name;
        private string _ip;
        private int _port;
        private TcpConnact connact;
        public event PropertyChangedEventHandler PropertyChanged;

        public Login()
        {
            connact = new TcpConnact();
            _name = "Player";
            _ip = "127.0.0.1";
            _port = 1300;
        }

        private void Notify(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                Notify("Name");
            }
        }
        public string Ip
        {
            get { return _ip; }
            set
            {
                _ip = value;
                Notify("Ip");
            }
        }
        public int Port
        {
            get { return _port; }
            set
            {
                _port = value;
                Notify("Port");
            }
        }
        public ButtonCommand AddClientClick
        {
            get
            {
                return new ButtonCommand(new Action<object>((obj) =>
                {
                    if(connact.Connacted(Ip, Port))
                    {
                        User user = User.GetObject(Name, connact.client);
                        IOClient client = IOClient.GetObject();
                        MainWindow loginWindow = (MainWindow)obj;
                        client.SendMessage(Name);
                        GameWindow gameWindow = new GameWindow();
                        gameWindow.Show();
                        loginWindow.Close();
                    }
                    else
                    {
                        MessageBox.Show("No connacted");
                    }
                }), new Func<bool>(() =>
                {
                if (Name != null && Ip != null && (_port.ToString()).Length >= 4)
                    {
                        return true;
                    }
                    return false;
                })
                );
            }
        }
    }
}

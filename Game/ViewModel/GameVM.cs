using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Game.Model;

namespace Game.ViewModel
{
    public class GameVM : INotifyPropertyChanged
    {
        private string _name;
        private string _my_path_photo;
        private string _enemy_path_photo;
        private string _labl_win;
        private string _item_name;
        private bool _can_click;
        private IOClient _ioclient;
        private Dispatcher _dispatcher;
        public event PropertyChangedEventHandler PropertyChanged;

        public GameVM()
        {
            _name = User.GetObject().Name;
            _can_click = true;
            _ioclient = IOClient.GetObject();
            _dispatcher = Dispatcher.CurrentDispatcher;
        }
        private async void ListenServerWin()
        {
            Task<string> taskListen = new Task<string>(_ioclient.ListenMessage);
            taskListen.Start();
            string message = await taskListen;
            if (message.Remove(message.IndexOf('|')) == "stone")
                ImageFonEnemy = "../image/stone.png";
            else if (message.Remove(message.IndexOf('|')) == "scissors")
                ImageFonEnemy = "../image/scissors.png";
            else if (message.Remove(message.IndexOf('|')) == "paper")
                ImageFonEnemy = "../image/paper.png";
            LableWin = message.Remove(0, message.IndexOf('|') + 1);
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
                Notify("Name_u");
            }
        }
        public string LableWin
        {
            get { return _labl_win; }
            set
            {
                _labl_win = value;
                Notify("LableWin");
            }
        }
        public string ImageFon
        {
            get { return _my_path_photo; }
            set
            {
                _my_path_photo = value;
                Notify("ImageFon");
            }
        }
        public string ImageFonEnemy
        {
            get { return _enemy_path_photo; }
            set
            {
                _enemy_path_photo = value;
                Notify("ImageFonEnemy");
            }
        }

        public ButtonCommandNoParam EnterClick
        {
            get
            {
                return new ButtonCommandNoParam(new Action(()=>
                {
                    _can_click = false;
                    _ioclient.SendMessage(_item_name);
                    ListenServerWin();
                }),
                new Func<bool>(() =>
                {
                    if (ImageFon != null && _can_click)
                        return true;
                    return false;
                }));
            }
        }
        public ButtonCommand FormClike
        {
            get
            {
                return new ButtonCommand(new Action<object>((obj) =>
                {
                    _item_name = (string)obj;

                    if(_item_name == "stone")
                    {
                        ImageFon = "../image/stone.png";
                    }
                    else if(_item_name == "scissors")
                    {
                        ImageFon = "../image/scissors.png";
                    }
                    else if(_item_name == "paper")
                    {
                        ImageFon = "../image/paper.png";
                    }
                }),
                new Func<bool>(() =>
                {
                    return _can_click;
                }));
            }
        }
    }
}
